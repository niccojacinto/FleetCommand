using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaveSpawner : MonoBehaviour
{

    [SerializeField]
    Transform target;

    public static WaveSpawner Instance;

    public float distanceToTarget;
    public float minSpawnRadius;
    public float maxSpawnRadius;
    public float spawnRate;

    float nextSpawnTime;
    Queue<string> spawnPool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        spawnPool = new Queue<string>();
    }

    void Update()
    {
        transform.position = target.position + target.forward * distanceToTarget;
        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnRate;
            SpawnNext();
        }
    }

    
    Vector3 GetRandomVector()
    {
        Quaternion rot = Random.rotation;
        
        // For my old wave design where the cruiser goes in a straight line
        // return new Vector3(rot.x, rot.y, 0).normalized;


        int negX = Random.Range(0, 2) == 0 ? -1 : 1;
        int negY = Random.Range(0, 2) == 0 ? -1 : 1;
        int negZ = Random.Range(0, 2) == 0 ? -1 : 1;
        Vector3 direction = new Vector3(rot.eulerAngles.x * negX, rot.eulerAngles.y * negY, rot.eulerAngles.z * negZ).normalized;
        return direction;
        
    }

    void SpawnNext()
    {
        if (spawnPool.Count > 0)
        {
            Spawn(spawnPool.Dequeue());
        } 

    }

    void Spawn(string spawnableName)
    {
        // Instantiate Spawnable at position * random radius around spawner + with rotation quaternion.identity
        Instantiate(SpawnableCollection.Instance.Get(spawnableName), transform.position + (GetRandomVector() * Random.Range(minSpawnRadius, maxSpawnRadius)), Quaternion.identity);
    }

    public void QueueList(List<string> list)
    {
        foreach (string s in list)
        {
            spawnPool.Enqueue(s);
        }
    }



}
