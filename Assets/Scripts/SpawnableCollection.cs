using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableCollection : MonoBehaviour
{
    Dictionary<string, GameObject> spawnDict;

    public static SpawnableCollection Instance;

    [SerializeField]
    List<GameObject> spawnables;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        spawnDict = new Dictionary<string, GameObject>();
        foreach (GameObject s in spawnables)
        {
            spawnDict.Add(s.name, s);
        }
    }

    public GameObject Get(string spawnableName)
    {
        return spawnDict[spawnableName];
    }
}
