using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autoturret : Turret
{
    public GameObject gunTurret;

    [SerializeField]
    GameObject projectilePrefab;

    [SerializeField]
    ParticleSystem muzzleFlash;

    public float timeBetweenShots;
    float timeBeforeNextShot;
    public float reloadTime;
    int clipSize = 3;
    int currentClip;

    AudioSource source;
    [SerializeField]
    AudioClip burstFireSFX;

    [SerializeField]
    bool firing;

    public float accuracy;
    public float maxDeviationDegrees;

    private void Start()
    {
       source = GetComponent<AudioSource>();
       currentClip = clipSize;
       firing = false;  
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
        // gunTurret.transform.LookAt(target.transform);
        Vector3 gunOrientation = gunTurret.transform.forward.normalized;
        Vector3 targetDirection = (target.transform.position - gunTurret.transform.position).normalized;
        float angle = Vector3.Angle(gunOrientation, targetDirection);

 
        
        if (angle < 5f)
        {
            if (!firing && currentClip > 0) 
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, gunTurret.transform.forward, out hit, 2.0f))
                {
                    if (hit.collider.CompareTag("UNSA"))
                    {
                        target = null;
                    } else
                    {
                        StartCoroutine(FireBurst());
                    }
                }
                else
                {
                    StartCoroutine(FireBurst());
                }
            }
        }

        if (timeBeforeNextShot > 0)
        {
            timeBeforeNextShot -= Time.deltaTime;
        }



    }

    void Fire()
    {
        // if (timeBeforeNextShot > 0) return;
        float accuracyRollX = (1 - accuracy) * Random.Range(-maxDeviationDegrees, maxDeviationDegrees);
        float accuracyRollY = (1 - accuracy) * Random.Range(-maxDeviationDegrees, maxDeviationDegrees);
        float accuracyRollZ = 0f;
        Quaternion adjustAccuracy = Quaternion.Euler(new Vector3(Random.Range(-accuracyRollX, accuracyRollX), Random.Range(-accuracyRollY, accuracyRollY), accuracyRollZ));
        Instantiate(projectilePrefab, gunTurret.transform.position, gunTurret.transform.rotation * adjustAccuracy);
        // timeBeforeNextShot = timeBetweenShots;
    }

    IEnumerator FireBurst()
    {
        firing = true;
        muzzleFlash.gameObject.SetActive(true);
        source.PlayOneShot(burstFireSFX);
        while (currentClip > 0)
        {
            Fire();
            currentClip--;
            yield return new WaitForSeconds(timeBetweenShots);
        }
        muzzleFlash.gameObject.SetActive(false);
        firing = false;
        yield return new WaitForSeconds(reloadTime);
        currentClip = clipSize;

    }

}
