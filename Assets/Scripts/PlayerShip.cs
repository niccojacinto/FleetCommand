using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerShip : Ship
{
    [Header("Griffin Stats")]
    [SerializeField]
    FlightController flightController;
    [SerializeField]
    ShootingController shootingController;

    [SerializeField]
    GameObject deathEffect;

    public bool IsDead
    {
        get { return (health <= 0); }
    }

    public int Health
    {
        get { return health; }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            health = 0;
            StartCoroutine(KillSelf());
        }
    }

    IEnumerator KillSelf()
    {

        deathEffect.SetActive(true);
        shootingController.enabled = false;
        Vector3 deathPosition = Camera.main.transform.position;
        
        float delay = 2.4f;
        while (delay > 0)
        {
            Camera.main.transform.position = deathPosition;
            Camera.main.transform.LookAt(Player.Instance.transform);
            delay -= Time.deltaTime;
            yield return null;
        }
        flightController.enabled = false;
        Camera.main.transform.position = deathPosition;
        Camera.main.transform.LookAt(Player.Instance.transform);
        CameraShaker.Instance.RestPositionOffset = transform.InverseTransformPoint(deathPosition);
        CameraShaker.Instance.RestRotationOffset = Camera.main.transform.localRotation.eulerAngles;
        EndScreen.Instance.ShowDefeat();

    }

    // MOVED TO CHILD's COLLIDER
    //private void OnCollisionEnter(Collision collision)
    //{
    //    StartCoroutine(flightController.ReleaseCam(.7f));
    //    CameraShaker.Instance.ShakeOnce(5f, 5f, .35f, .35f);
    //}


}
