using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField]
    Projectile projectilePrefab;
    [SerializeField]
    Projectile missilePrefab;

    [SerializeField]
    Transform[] missilePods;
    bool rightPodTurn = true;

    [SerializeField]
    Reticle reticle;
    [SerializeField]
    float reticleRange = 20f;
    [SerializeField]
    float accuracy = 1f;
    [SerializeField]
    float maxDeviationDegrees = 45f;

    public float timeBetweenShots;
    float timeBeforeNextShot;

    Camera cam;
    public AudioSource sourceExterior;
    public AudioSource sourceInterior;
    public AudioSource missileFire;
    [SerializeField]
    AudioClip shootOne;

    Transform lockedTarget;
    [SerializeField]
    AudioClip lockingSFX;
    [SerializeField]
    AudioClip lockedSFX;
    [SerializeField]
    LayerMask lockOnMask;

    [SerializeField]
    AudioClip missileFireSFX;

    public int gunAmmo;
    public int missileAmmo;
    public LineRenderer debugLine;

    private void Start()
    {
        cam = Camera.main;
        StartCoroutine(LockOn());
    }

    IEnumerator LockOn()
    {
        RaycastHit hit;
        float lockSeconds = 0.5f;
        float lockTimeLeft = lockSeconds;
        Transform currentTarget = null;
        Ship currentShip = null;

        // bool flags so we dont repeat code each frame.
        bool isLocking = false;
        bool isLocked = false;
        while (true)
        {
            if (Physics.SphereCast(Camera.main.transform.position, 1f, Camera.main.transform.forward, out hit, 50f, lockOnMask))
            {
                // IF SOMETHING IS HIT
                // If target is new -- Reset everything for the next Raycast
                if (currentTarget != hit.transform)
                {
                    // reset the old lockon
                    if (currentShip != null && currentShip.hudSignature != null) currentShip.hudSignature.SetNoLockOn();
                    isLocked = false;
                    isLocking = false;
                    lockedTarget = null; // If you lose the target, you start from scratch.
                    currentShip = hit.transform.GetComponent<Ship>();
                    currentTarget = hit.transform;
                    lockTimeLeft = lockSeconds;
                    sourceInterior.Stop();

                }
                // If the target is still the same after the next Raycast, start locking in
                else 
                {
                    // Locking In
                    // --
                    // If the locking is not yet finished, carry on with ONLY the countdown but everything else ONCE

                    if (lockTimeLeft > 0)
                    {
                        if (!isLocking)
                        {
                            sourceInterior.Stop();
                            sourceInterior.clip = lockingSFX;
                            sourceInterior.loop = true;
                            sourceInterior.Play();
                            isLocking = true;
                        } 
                        lockTimeLeft -= Time.deltaTime;
                    }
                    // Otherwise set the lock target and setup everything else you need to only ONCE
                    else
                    {
                        if (!isLocked)
                        {
                            sourceInterior.Stop();
                            sourceInterior.clip = lockedSFX;
                            sourceInterior.loop = true;
                            sourceInterior.PlayOneShot(lockedSFX); // use for one shots ofc
                            // sourceInterior.Play(); // Use for looping sfx
                            lockedTarget = currentTarget;
                            if (currentShip.hudSignature != null && currentShip.hudSignature != null) currentShip.hudSignature.SetLockOn();
                            isLocked = true;
                        }    

                    }
                }
            }
            else // if nothing was hit... still reset
            {
                if (currentShip != null && currentShip.hudSignature != null) currentShip.hudSignature.SetNoLockOn();
                isLocked = false;
                isLocking = false;
                lockedTarget = null; // If you lose the target, you start from scratch.
                currentShip = null;
                currentTarget = null;
                lockTimeLeft = lockSeconds;
                sourceInterior.Stop();
            }
            yield return null;
        }
        //while (true)
        //{
        //    // Debug.DrawRay(transform.position, transform.forward * 25f);
        //    //debugLine.useWorldSpace = false;
        //    //debugLine.positionCount = 2;
        //    //debugLine.SetPositions(new Vector3[] {Camera.main.transform.position, Camera.main.transform.forward * 25f });
        //    if (Physics.SphereCast(Camera.main.transform.position, 1f, Camera.main.transform.forward, out hit, 50f, lockOnMask))
        //    {
        //        currentTarget = hit.transform;
        //        if (lockingHasStarted)
        //        {

        //        }

        //            // If lockingTarget was not replaced (still OLD TARGET)
        //            if (currentTarget == hit.transform)
        //            {
        //                // Continue Locking within the loop
        //                if (lockTime <= 0)
        //                {
        //                    // Target is Locked

        //                    targetLock = currentTarget;         
        //                    sourceInterior.clip = lockedSFX;
        //                    sourceInterior.loop = true;
        //                    if (!sourceInterior.isPlaying)
        //                    {
        //                        HUD.Instance.ModifySignatureRFColor(targetLock.GetComponentInChildren<Renderer>(), Color.red);
        //                        sourceInterior.Stop();
        //                        sourceInterior.Play();
        //                    }
        //            }
        //                else
        //                {
        //                    // Locking in progress
        //                    lockTime -= Time.deltaTime;
        //                    sourceInterior.clip = lockingSFX;
        //                    sourceInterior.loop = true;
        //                    if (!sourceInterior.isPlaying)
        //                    {
        //                        sourceInterior.Play();
        //                    }

        //                }

        //            } 
        //            // TARGET LOST / LOCKING NEW TARGET
        //            else
        //            {
        //                currentTarget = null;
        //                targetLock = null;
        //                lockTime = lockingSeconds;
        //                sourceInterior.Stop();
        //            }
        //        }
        //    else
        //    {
        //        if (targetLock != null) HUD.Instance.ModifySignatureRFColor(targetLock.GetComponentInChildren<Renderer>(), Color.green);
        //        currentTarget = null;
        //        targetLock = null;
        //        lockTime = lockingSeconds;
        //        sourceInterior.Stop();
        //    }

        //    yield return null;
        //}
    }

    void Update()
    {
        if (timeBeforeNextShot > 0)
        {
            timeBeforeNextShot-=Time.deltaTime;
        }

        // Vector3 aimVector = (cam.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        if (Input.GetButton("Fire1"))
        {
            if (timeBeforeNextShot > 0 || gunAmmo <= 0) return;
            sourceExterior.PlayOneShot(shootOne);
            Ray ray = cam.ScreenPointToRay(reticle.transform.position);
            Vector3 targetPosition = ray.origin + ray.direction * reticleRange;
            Vector3 aimVector = (targetPosition - transform.position).normalized;
            float accuracyRollX = (1 - accuracy) * Random.Range(-maxDeviationDegrees, maxDeviationDegrees);
            float accuracyRollY = (1 - accuracy) * Random.Range(-maxDeviationDegrees, maxDeviationDegrees);
            float accuracyRollZ = 0f;
            Quaternion adjustAccuracy = Quaternion.Euler(new Vector3(Random.Range(-accuracyRollX, accuracyRollX), Random.Range(-accuracyRollY, accuracyRollY), accuracyRollZ));
            Quaternion aimRot = Quaternion.LookRotation(adjustAccuracy * aimVector, Vector3.up);


            Instantiate(projectilePrefab, transform.position, aimRot);
            timeBeforeNextShot = timeBetweenShots;
            gunAmmo--;
        }


        if (Input.GetButtonDown("Fire2"))
        {
            if (missileAmmo <= 0) return;
            Missile m = Instantiate(missilePrefab, rightPodTurn ? missilePods[0].position : missilePods[1].position, transform.rotation) as Missile;
            m.target = lockedTarget;
            rightPodTurn = !rightPodTurn;
            missileFire.PlayOneShot(missileFireSFX);
            missileAmmo--;
        }
    }
}
