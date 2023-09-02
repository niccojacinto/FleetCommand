using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed;
    public GameObject hitParticle;
    public int damage;
    public float life = 1f;

    private void FixedUpdate()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void Start()
    {
        Destroy(gameObject, life);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hitParticle) Instantiate(hitParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
