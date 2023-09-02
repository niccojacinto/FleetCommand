using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximitySensor : MonoBehaviour
{
    [SerializeField]
    List<Transform> targetsInProximity;

    public List<Transform> TargetsInProximity
    {
        get { return targetsInProximity; }
    }

    private void OnTriggerEnter(Collider other)
    {
        targetsInProximity.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        targetsInProximity.Remove(other.transform);
    }
}
