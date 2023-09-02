using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpy : Ship
{
    [Header("Harpy Settings")]
    public Transform target;

    public float fireRate;
    float lastFireTime;

    [SerializeField]
    Projectile attack1Prefab;

    [SerializeField]
    GameObject deathParticlePrefab;

    bool dodging;
    protected override void Start()
    {
        base.Start();
        hudSignature = HUD.Instance.CreateSignature(this, "HARPY", "ALLY", Color.red);
        StartCoroutine(Deploy());
    }

    IEnumerator Deploy()
    {
        while (true)
        {
            if (!target)
                target = FactionManager.Instance.GetRandomUnitFromFaction(0);
            yield return new WaitForSeconds(0.1f);
        }

    }

    // Hacky way of dodging since, left/right dodging is too ugly to look at and since IDK any other steering/flocking algorithms
    IEnumerator EvasiveAction()
    {
        dodging = true;
        float dodgeTime = Random.Range(1f, 2f);
        Vector3[] dodgePool = new Vector3[] { transform.up, -transform.up, transform.right, -transform.right };
        Vector3 dodgeDirection = dodgePool[Random.Range(0, dodgePool.Length)];
        while (dodgeTime > 0)
        {
            transform.Rotate(dodgeDirection * turnSpeed * Time.deltaTime);
            dodgeTime -= Time.deltaTime;
            yield return null;
        }
        dodging = false;
    }

    private void Update()
    {
        if (!target) return;
        transform.position += transform.forward * flightSpeed * Time.deltaTime;


        float aimAngleToTarget = Vector3.Angle(transform.forward, (target.position - transform.position).normalized);
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Avoid
        if (!dodging)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, flightSpeed))
            {
                if (!hit.collider.CompareTag("Untagged"))
                {
                    StartCoroutine(EvasiveAction());
                }
            }
            else
            {
                // Seek
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target.position - transform.position), turnSpeed * Time.deltaTime);
            }
        }

        if (aimAngleToTarget < 25f)
        {
            Fire();
        }
    }

    void Fire()
    {
        if (lastFireTime + fireRate <= Time.time)
        {
            Instantiate(attack1Prefab, transform.position, transform.rotation);
            lastFireTime = Time.time;
        }

    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("UNSA Projectile"))
        {
            Projectile p = other.GetComponent<Projectile>();
            TakeDamage(p.damage);
        }

    }


    protected override IEnumerator Die()
    {
        dying = true;
        Instantiate(deathParticlePrefab, transform.position, Quaternion.identity, null);
        if (hudSignature != null) Destroy(hudSignature.gameObject);
        Destroy(gameObject);
        yield return null;
    }

    protected override void TakeDamage(int amount)
    {
        if (dying) return;
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, health);
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }


}
