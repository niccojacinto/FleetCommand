using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour
{
    public string factionName;

    [SerializeField]
    List<Transform> units;

    private void Start()
    {
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void AddUnit(Transform t)
    {
        units.Add(t);
    }

    public void RemoveUnit(Transform t)
    {
        units.Remove(t);
    }

    public Transform GetRandomUnit()
    {
        if (units.Count <= 0) return null;
        return units[Random.Range(0, units.Count)];
    }

    bool ContainsUnit(Transform t)
    {
        return units.Contains(t);
    }

}
