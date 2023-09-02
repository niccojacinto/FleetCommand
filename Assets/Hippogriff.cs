using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hippogriff : Ship
{
    [Header("Hippogriff Settings")]
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
        hudSignature = HUD.Instance.CreateSignature(this, "UNSA HIPPOGRIFF", "ALLY", Color.cyan);
        StartCoroutine(Deploy());
        
    }

    IEnumerator Deploy()
    {
        while (true)
        {
            if (!target)
            target = FactionManager.Instance.GetRandomUnitFromFaction(1);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("UMBRA Projectile"))
        {
            Projectile p = other.GetComponent<Projectile>();
            TakeDamage(p.damage);
        }

    }


    IEnumerator Die()
    {
        dying = true;
        // Helpers.Chance(100f, () => { HUD.Instance.Say("Hippogriff Pilot", "Ahh!", Dialogues.Instance.DialogueDict["cduckett-05-scream"], false); });
        float deathTimer = 2.5f;
        GameObject deathParticle  = Instantiate(deathParticlePrefab, transform, false);
        Destroy(hudSignature.gameObject);
        while (deathTimer > 0)
        {
           
            deathTimer-=Time.deltaTime;
            yield return null;
        }
        if (deathParticle != null)
        {
            deathParticle.transform.SetParent(null);
        }
        Destroy(gameObject);
    }

    void TakeDamage(int amount)
    {
        // Helpers.Chance(10f, () => { HUD.Instance.Say("Hippogriff Pilot", "Taking Fire", Dialogues.Instance.DialogueDict["cduckett-04-taking_fire"], false); });

        if (dying) return;
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, health);
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }


}
