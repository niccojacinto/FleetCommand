using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris : Ship
{

    Transform target;
    Vector3 targetLocation;

    float range = 25f;
    float jumpDistance = 3f;
    

    [SerializeField]
    Projectile projectilePrefab;

    public float fireRate = 1f;
    float lastFireTime;

    public float jumpRate = 1f;
    float lastJumpTime;

    Vector3 velocity = Vector3.zero;
    protected override void Start()
    {
        base.Start();
        target = FactionManager.Instance.GetRandomUnitFromFaction(0);
        hudSignature = HUD.Instance.CreateSignature(this, "IRIS", "ENEMY INTERCEPTOR", Color.red);
        turnSpeed = 120f;
    }


    private void Update()
    {
        if (!target) return;

        transform.position = Vector3.MoveTowards(transform.position, targetLocation, flightSpeed * Time.deltaTime);
        // transform.position = Vector3.SmoothDamp(transform.position, targetLocation, ref velocity, 0.25f, flightSpeed);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target.position - transform.position), turnSpeed * Time.deltaTime);

        float aimAngleToTarget = Vector3.Angle(transform.forward, (target.position - transform.position).normalized);

        if (aimAngleToTarget < 1f)
        {
            Fire();
        }
        if (transform.position.Equals(targetLocation) && lastJumpTime + jumpRate < Time.time)
        {
            Jump();
        }

    }

    void Jump()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget > range)
        {
            targetLocation = transform.position + transform.forward * jumpDistance;

        }
        else
        {
            targetLocation = transform.position + GetRandomVector() * jumpDistance;
        }
        lastJumpTime = Time.time;
    }

    Vector3 GetRandomVector()
    {
        Quaternion rot = Random.rotation;

        // For my old wave design where the cruiser goes in a straight line
        // return new Vector3(rot.x, rot.y, 0).normalized;


        int negX = Random.Range(0, 2) == 0 ? -1 : 1;
        int negY = Random.Range(0, 2) == 0 ? -1 : 1;
        int negZ = Random.Range(0, 2) == 0 ? -1 : 1;
        Vector3 direction = new Vector3(rot.eulerAngles.x * negX, rot.eulerAngles.y * negY, rot.eulerAngles.z * negZ).normalized;
        return direction;

    }


    void Fire()
    {
        if (lastFireTime + fireRate <= Time.time)
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
            lastFireTime = Time.time;
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("UNSA Projectile"))
        {
            Projectile p = other.GetComponent<Projectile>();
            TakeDamage(p.damage);
        }

    }


    IEnumerator Die()
    {
        dying = true;
        float deathTimer = 2.5f;
        // GameObject deathParticle = Instantiate(deathParticlePrefab, transform, false);
        Destroy(hudSignature.gameObject);
        while (deathTimer > 0)
        {

            deathTimer -= Time.deltaTime;
            yield return null;
        }
        //if (deathParticle != null)
        //{
        //    deathParticle.transform.SetParent(null);
        //}
        Destroy(gameObject);
    }

    void TakeDamage(int amount)
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

