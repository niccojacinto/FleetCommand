using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Spawnable
{
    [SerializeField]
    Transform target;

    enum Type { IDLE = 0, HOMING = 1 }
    public float health;
    public float speed;
    public int value;
    Type type;


    [SerializeField]
    ParticleSystem deathParticles;

    [SerializeField]
    AudioClip deathSFX;

    private void Start()
    {

        // FactionManager.Instance.AddToFaction(transform, 2);
        Vector3 randForce = Random.rotation.eulerAngles.normalized * 1f;
        //GetComponent<Rigidbody>().AddForce(randForce, ForceMode.VelocityChange);
        GetComponent<Rigidbody>().AddTorque(randForce, ForceMode.VelocityChange);
        type = (Type)Random.Range(0, 2);
        target = FactionManager.Instance.GetRandomUnitFromFaction(0);
    }


    void Update()
    {
        if (type == Type.HOMING)
        {
            if (target) 
            {
                Vector3 towardsTargetDir = (target.transform.position - transform.position).normalized;
                transform.position = transform.position + (towardsTargetDir * speed * Time.deltaTime);
            }

        }
 
        TrashManager.Instance.Cleanup(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {

            Projectile p = other.GetComponent<Projectile>();

            health-=p.damage;
            if (health <= 0) 
            {
                Player.Instance.AddResources(value);
                Instantiate(deathParticles, transform.position, Quaternion.identity, null);
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("UNSA"))
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity, null);
            Destroy(gameObject);
        }
    }
}
