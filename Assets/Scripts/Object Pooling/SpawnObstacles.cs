using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO
// Change the frequencies of how often the obstacles shall spawn
// Based on the player's performance
// The longer the player goes without colliding, the faster they spawn
// Reset the frequency on collision
public class SpawnObstacles : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstacles = new List<GameObject>();
    [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();

    [SerializeField] private float defaultObjectPoolTimer = 5f;

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

    // Start is called before the first frame update
    private void Start()
    {
        player = FindObjectOfType<Player>();
        objectPooling = FindObjectOfType<ObjectPooling>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (allowSpawn)
            SpawnRandomObstacle();

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

        Mathf.Clamp(objectPoolTimer, 0.2f, defaultObjectPoolTimer);

        if (totalRuntime >= 5f && objectPoolTimer >= 0.2f)
        {
            objectPoolTimer -= 0.5f;
            totalRuntime = 0;
        }
        else if (player.isPlayerHit)
        {
            player.isPlayerHit = false;
            totalRuntime = 0;
            objectPoolTimer += 0.4f;
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