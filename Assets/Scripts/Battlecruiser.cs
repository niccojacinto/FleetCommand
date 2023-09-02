using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battlecruiser : Ship
{
    [SerializeField]
    Turret[] turrets;

    [SerializeField]
    ProximitySensor proximitySensor;

    [SerializeField]
    GameObject takeDmgEffect;


    protected override void Start()
    {
        base.Start();
        hudSignature = HUD.Instance.CreateSignature(this, "UNSA AMBTION", "ALLY - FLAGSHIP", Color.yellow);
        AquireTargets();
    }

    private void Update()
    {
        transform.position += transform.forward * flightSpeed * Time.deltaTime;
    }

    private void AquireTargets()
    {
        foreach (Turret t in turrets)
        {
            t.AquireTargets(proximitySensor);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        health -= (int)collision.rigidbody.mass;
        Instantiate(takeDmgEffect, collision.contacts[0].point, Quaternion.identity, null);
    }
}
