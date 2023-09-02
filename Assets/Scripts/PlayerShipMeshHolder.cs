using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

[RequireComponent(typeof(Collider))]
public class PlayerShipMeshHolder : MonoBehaviour
{
    [SerializeField]
    FlightController fc;

    [SerializeField]
    PlayerShip ps;

    [SerializeField]
    GameObject takeDmgEffect;

    private void Awake()
    {
        ps = GetComponentInParent<PlayerShip>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ps.TakeDamage((int)collision.rigidbody.mass);
        Instantiate(takeDmgEffect, collision.contacts[0].point, Quaternion.identity, null);
        // StartCoroutine(fc.ReleaseCam(.7f));
        // CameraShaker.Instance.ShakeOnce(5f, 5f, .35f, .35f);
        CameraShaker.Instance.Shake(CameraShakePresets.Explosion);

        if (collision.collider.CompareTag("Debris"))
        {
            Helpers.Chance(25f, () =>
            {
                HUD.Instance.Say("AWACS Hopper", "Careful!", Dialogues.Instance.DialogueDict["AWACS-Careful_1"], false);
            });

        }
    }

}
