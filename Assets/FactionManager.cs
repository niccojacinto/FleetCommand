using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionManager : MonoBehaviour
{
    private static FactionManager m_instance;
    public static FactionManager Instance { get { return m_instance; } }

    [SerializeField]
    Faction[] factions;

    private void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(gameObject);
        } else
        {
            m_instance = this;
        }

        factions = GetComponentsInChildren<Faction>();
    }

    public Transform GetRandomUnitFromFaction(int factionIndex)
    {
        return factions[factionIndex].GetRandomUnit();
    }

    public void AddToFaction(Transform t, int factionIndex)
    {
        factions[factionIndex].AddUnit(t);
    }

    public void RemoveFromFaction(Transform t, int factionIndex)
    {
        factions[factionIndex].RemoveUnit(t);
    }

    private void OnDestroy()
    {
        m_instance = null;
    }


}
