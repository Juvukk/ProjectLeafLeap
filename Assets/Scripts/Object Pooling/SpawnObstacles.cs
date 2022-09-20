using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    [Header("Obstacles")]
    [SerializeField] private List<GameObject> obstacles = new List<GameObject>();
    [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();

    [Header("Object Pooling Settings")]
    [SerializeField,Tooltip("Default threshold for spawning obstacles")] 
    private float defaultOPTimerThreshold = 5f;
    [SerializeField] private float runtimeThreshold = 5f;
    [SerializeField] private float minSpawnIntervals = 0.2f;
    [SerializeField] private float increaseSpawnIntervals = 0.5f;
    [SerializeField] private float decreaseSpawnIntervals = 0.5f;

    private Player player;

    private ObjectPooling objectPooling;
    private float objectPoolTimer = 3f;
    private float spawnTimer;
    private float totalRuntime;

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

    private void Start()
    {
        player = FindObjectOfType<Player>();
        objectPooling = FindObjectOfType<ObjectPooling>();
    }

    private void Update()
    {
        if (allowSpawn) SpawnRandomObstacle();

        ChangeSpawnInterval();
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

    /// <summary>
    /// Change how frequent the spawning occurs
    /// Based on how long the player can go without colliding with an obstacle
    /// </summary>
    public void ChangeSpawnInterval()
    {
        totalRuntime += Time.deltaTime;

        objectPoolTimer = Mathf.Clamp(objectPoolTimer, 0.2f, defaultOPTimerThreshold);

        // If total run time exceeds x,
        // decrease the spawning interval by x seconds
        if (totalRuntime >= runtimeThreshold && objectPoolTimer >= minSpawnIntervals)
        {
            // Decrease the spawning interval
            objectPoolTimer -= decreaseSpawnIntervals;
            // Reset timer
            totalRuntime = 0;
        }
        else if (player.isPlayerHit)
        {
            // Reset playerhit
            player.SetPlayerHit(false);
            totalRuntime = 0;

            // Increase the spawning interval
            objectPoolTimer += increaseSpawnIntervals;
        }
        else if (objectPoolTimer < 0.2f)
        {
            objectPoolTimer = 0.2f;
        }
    }

    private void StopSpawning()
    {
        allowSpawn = false;
    }
}