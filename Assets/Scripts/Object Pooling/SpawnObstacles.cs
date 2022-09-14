using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstacles = new List<GameObject>();
    [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();

    [Tooltip("Default threshold for spawning obstacles")]
    [SerializeField] private float defaultObjectPoolTimer = 5f;

    [SerializeField] private float runtimeThreshold = 5f;
    [SerializeField] private float minObjectPoolThreshold = 0.2f;
    [SerializeField] private float increaseObjectPoolTimer = 0.5f;
    [SerializeField] private float decreaseObjectPoolTimer = 0.5f;

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

    public void ChangeSpawnInterval()
    {
        totalRuntime += Time.deltaTime;

        objectPoolTimer = Mathf.Clamp(objectPoolTimer, 0.2f, defaultObjectPoolTimer);

        // If total run time exceeds x,
        // decrease the spawning interval by x seconds
        if (totalRuntime >= runtimeThreshold && objectPoolTimer >= minObjectPoolThreshold)
        {
            // Decrease the spawning interval
            objectPoolTimer -= decreaseObjectPoolTimer;
            // Reset timer
            totalRuntime = 0;
        }
        else if (player.isPlayerHit)
        {
            // Reset playerhit
            player.SetPlayerHit(false);
            totalRuntime = 0;

            // Increase the spawning interval
            objectPoolTimer += increaseObjectPoolTimer;
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