using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float time;
    void Start()
    {
        if (time >= 0) Destroy(gameObject, time);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

}
