using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{

    public HUDSignature hudSignature;

    [Header("Ship Hull Stats")]
    [SerializeField]
    protected int shields;
    [SerializeField]
    protected int armor;
    [SerializeField]
    protected int health;
    public int Health {  get { return health; } }
    protected int currentHealth;
    public int CurrentHealth { get { return currentHealth; } }

    protected bool dying = false;

    protected virtual void Start()
    {
        currentHealth = health;
    }

    [Header("Ship Mobility Stats")]
    [SerializeField]
    public float acceleration;
    public float flightSpeed;
    public float maxFlightSpeed;
    public float turnSpeed;

    protected virtual void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("UNSA Projectile") && this.CompareTag("Umbra") ||
            other.gameObject.layer == LayerMask.NameToLayer("UMBRA Projectile") && this.CompareTag("UNSA"))
        {
            Projectile p = other.GetComponent<Projectile>();
            TakeDamage(p.damage);
        }
    }


    protected virtual IEnumerator Die()
    {
        dying = true;
        float deathTimer = 2.5f;
        //if (deathParticle != null) GameObject deathParticle = Instantiate(deathParticlePrefab, transform, false);
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

    protected virtual void TakeDamage(int amount)
    {
        if (dying) return;
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, health);
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private void OnBecameInvisible()
    {
        if (hudSignature != null) hudSignature.gameObject.SetActive(false);
    }

    private void OnBecameVisible()
    {
        if (hudSignature != null) hudSignature.gameObject.SetActive(true);
    }

}
