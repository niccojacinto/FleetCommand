using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionLight : MonoBehaviour
{
    public AnimationCurve lightIntensity;
    public float lifetime;

    new Light light;
    float startTime;

    void Start()
    {
        light = GetComponent<Light>();
        startTime = Time.time;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        light.intensity = lightIntensity.Evaluate(Time.time - startTime);
    }
}
