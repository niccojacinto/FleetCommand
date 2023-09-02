using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTurret : Turret
{
    public GameObject gunTurret;

    [SerializeField]
    Missile projectilePrefab;

    public float timeBetweenShots;
    float timeBeforeNextShot;


    AudioSource source;
    [SerializeField]
    AudioClip fireSound;


    private void Start()
    {
        source = GetComponent<AudioSource>();
    }


    private void RotateGunToTarget()
    {
        Vector3 targetDirection = (target.transform.position - gunTurret.transform.position).normalized;
        Quaternion rotDelta = Quaternion.RotateTowards(gunTurret.transform.rotation, Quaternion.LookRotation(targetDirection, Vector3.up), 180f * Time.deltaTime);
        gunTurret.transform.rotation = rotDelta;
    }

    private void Update()
    {

        if (target == null) return;
        RotateGunToTarget();
        Vector3 gunOrientation = gunTurret.transform.forward.normalized;
        Vector3 targetDirection = (target.transform.position - gunTurret.transform.position).normalized;
        float angle = Vector3.Angle(gunOrientation, targetDirection);



        if (angle < 5f)
        {
            Fire();
        }

        if (timeBeforeNextShot > 0)
        {
            timeBeforeNextShot -= Time.deltaTime;
        }

    }

    void Fire()
    {
        if (timeBeforeNextShot > 0) return;
        Missile m = Instantiate(projectilePrefab, gunTurret.transform.position, gunTurret.transform.rotation) as Missile;
        m.target = target;
        source.PlayOneShot(fireSound);
        timeBeforeNextShot = timeBetweenShots;
    }
}
