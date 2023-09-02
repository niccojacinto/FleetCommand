using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public Transform target;
    public virtual void AquireTargets(ProximitySensor sensor) 
    {
        StartCoroutine(LookForTargets(sensor));
    }

    public IEnumerator LookForTargets(ProximitySensor sensor)
    {
        Transform temp;
        while (true)
        {
            // If no targets are in range -- do nothing
            if (sensor.TargetsInProximity.Count <= 0) yield return null;
            else if (target)
            {
                // If the target still exists -- do nothing
                if (sensor.TargetsInProximity.Contains(target))
                {
                    yield return null;
                }
                else
                {
                    // Otherwise
                    // Pick a random target within proximity and evaluate
                    while (target == null)
                    {
                        temp = null;
                        if (sensor.TargetsInProximity.Count > 0)
                        {
                            temp = sensor.TargetsInProximity[Random.Range(0, sensor.TargetsInProximity.Count)];
                        }
                        foreach (string opposingFaction in Helpers.Opposing(this.tag))
                        {
                            if (temp && temp.CompareTag(opposingFaction))
                            {
                                target = temp;
                                break;
                            }
                        }
                        // If no suitable targets are found.. try again;
                        yield return null;
                    }

                }
            }
            else
            {
                // Pick a random target within proximity and evaluate
                while (target == null)
                {
                    temp = null;
                    if (sensor.TargetsInProximity.Count > 0)
                    {
                        temp = sensor.TargetsInProximity[Random.Range(0, sensor.TargetsInProximity.Count)];
                    }


                    foreach (string opposingFaction in Helpers.Opposing(this.tag))
                    {
                        if (temp && temp.CompareTag(opposingFaction))
                        {
                            target = temp;
                            break;
                        }
                    }
                    // If no suitable targets are found.. ;
                    yield return  null;
                }
            }
            yield return new WaitForSeconds(1f);
        }


    }
}
