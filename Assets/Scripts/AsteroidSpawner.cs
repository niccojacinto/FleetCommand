using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField]
    Asteroid spawnablePrebab;
    public float spawnRadius;
    public float spawnRate;
    float spawnTime;
    public int maxSpawns;

    public Transform testTarget;

    static List<Asteroid> spawnedAsteroids;

    private void Start()
    {
        spawnedAsteroids = new List<Asteroid>();
    }

    private void Update()
    {
        if (spawnTime <= 0)
        {
            if (spawnedAsteroids.Count < maxSpawns)
            {
                Asteroid newAsteroid = Instantiate(spawnablePrebab, transform.position + GetRandomVector(spawnRadius), Quaternion.identity) as Asteroid;
                spawnedAsteroids.Add(newAsteroid);
                spawnTime = spawnRate;
            }
        }
        spawnTime -= Time.deltaTime;
    }

    Vector3 GetRandomVector(float distance)
    {
        Vector3 rv = new Vector3(Random.Range(-1f, 1f),Random.Range(-1f, 1f),Random.Range(-1f, 1f)) * distance;
        // Just return forward vector if its zero, dont know if this is even worth it
        return rv == Vector3.zero ? Vector3.forward : rv;
    }

    public static void RemoveAsteroid(Asteroid asteroid)
    {
        spawnedAsteroids.Remove(asteroid);
    }
}
