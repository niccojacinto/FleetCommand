using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocula : Ship
{
    [Header("Ocula Settings")]

    public Transform target;
    [SerializeField]
    ProximitySensor sensor;

    [SerializeField]
    Projectile projectilePrefab;

    public float fireRate;
    float lastFireTime;

    protected override void Start()
    {
        base.Start();
        hudSignature = HUD.Instance.CreateSignature(this, "OCULA ORBITAL CANNON", "!DANGER!", Color.red);
        target = FactionManager.Instance.GetRandomUnitFromFaction(0);
    }

    private void Update()
    {
        transform.position += Vector3.forward * flightSpeed * Time.deltaTime;
        if (!target) return;
        float aimAngleToTarget = Vector3.Angle(transform.forward, (target.position - transform.position).normalized);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target.position - transform.position), turnSpeed * Time.deltaTime);
        if (aimAngleToTarget < 1f)
        {
            Fire();
        }
    }

    void Fire()
    {
        if (Time.time > lastFireTime + fireRate)
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation, null);
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
        float deathTimer = 1f;
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
