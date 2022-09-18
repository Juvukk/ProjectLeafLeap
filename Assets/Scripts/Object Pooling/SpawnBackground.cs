using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBackground : MonoBehaviour
{
    [SerializeField] private List<GameObject> backgroundObjects = new List<GameObject>();
    [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();

    [SerializeField] private float spawnTimer;
    [SerializeField] private float timerThreshold = 5f;

    private ObjectPooling objectPool;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    private void Awake()
    {
        objectPool = FindObjectOfType<ObjectPooling>();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnBackgroundObjects();
    }

    private void SpawnBackgroundObjects()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer <= timerThreshold) return;

        spawnTimer = 0;
 
        int randomObj = Random.Range(0, backgroundObjects.Count);

        GameObject backgroundObject = objectPool.GetObject(backgroundObjects[randomObj]);

        spawnedObjects.Add(backgroundObject);

        var length = spawnedObjects[spawnedObjects.Count - 1].GetComponent<Renderer>().bounds.size.z;

        // Spawn at a spawn point
        backgroundObject.transform.position = spawnPoints[0].transform.position;
    }
};

        
