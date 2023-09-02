using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpalerMissile : Projectile
{

    public Transform target;

    [SerializeField]
    float turnSpeedDegrees;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (!target) return;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation((target.position - transform.position).normalized), turnSpeedDegrees * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(hitParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}
