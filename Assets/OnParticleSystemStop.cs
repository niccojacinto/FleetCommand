using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnParticleSystemStop : MonoBehaviour
{
    [SerializeField]
    ParticleSystem ps;
    

    void OnParticleSystemStopped()
    {
        Destroy(gameObject);
    }
}
