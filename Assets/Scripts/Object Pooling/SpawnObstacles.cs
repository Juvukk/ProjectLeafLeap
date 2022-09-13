using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnObstacles : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstacles = new List<GameObject>();
    [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();

    private ObjectPooling objectPooling;
    private float objectPoolTimer = 3f;
    private float spawnTimer;

    private int lastSpawnPoint;
    private bool allowSpawn = true;

    private void OnEnable()
    {
        EventManager.endGame += StopSpawning;
    }

    private void OnDisable()
    {
        EventManager.endGame -= StopSpawning;
    }

    // Start is called before the first frame update
    private void Start()
    {
        objectPooling = FindObjectOfType<ObjectPooling>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (allowSpawn)
            SpawnRandomObstacle();
    }

    private void SpawnRandomObstacle()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer <= objectPoolTimer) return;

        // Reset timer to prevent infinite spawning
        spawnTimer = 0;

        var randomObstacle = Random.Range(0, obstacles.Count);

        // Create or reuse an existing obstacle
        GameObject obstacle = objectPooling.GetObject(obstacles[randomObstacle]);

        var randomSpawnPoint = Random.Range(0, spawnPoints.Count);

        // If the random spawn point is the same as the last one, randomize again
        if (randomSpawnPoint == lastSpawnPoint)
        {
            randomSpawnPoint = Random.Range(0, spawnPoints.Count);
        }
        else if (randomSpawnPoint != lastSpawnPoint)
        {
            // Set the obstacle's position to be the same as the spawn point
            obstacle.transform.position = spawnPoints[randomSpawnPoint].transform.position;
            lastSpawnPoint = randomSpawnPoint;
        }
    }

    private void StopSpawning()
    {
        allowSpawn = false;
    }
}