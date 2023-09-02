using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour
{
    ///<summary>
    /// This monobehavior is supposed to delete ANY spawned objects behind this gameobject to improve performance and reduce clutter
    /// it should only delete spawned objects at a certain distance AND the objects have to be behind this object.
    ///</summary>
    
    public static TrashManager Instance;

    public float deletionRange;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    bool IsBehind(Transform other)
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toOther = other.position - transform.position;

        if (Vector3.Dot(forward, toOther) < 0)
        {
            return true;
        }
        return false;
    }

    bool IsFar(Transform other)
    {
        if (Vector3.Distance(transform.position, other.position) >= deletionRange)
        {
            return true;
        }
        return false;
    }

    public void Cleanup(Spawnable spawnable)
    {
        if (IsBehind(spawnable.transform) && IsFar(spawnable.transform))
        {
            Destroy(spawnable.gameObject);
        }
    }

}
