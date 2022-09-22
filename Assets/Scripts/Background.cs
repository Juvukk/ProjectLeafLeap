using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private List<GameObject> backgroundObjects = new List<GameObject>();
    [SerializeField] private Transform spawnPoint;

    [Tooltip("When a background object reaches this point, spawn a new background object")]
    [SerializeField] private float zThreshold = -25f;

    private ObjectPooling objectPooling;
    private List<GameObject> spawnedBkgObjects = new List<GameObject>();

    private GameObject bkg;
    private int lastRandomizedBkg;

    private void Awake()
    {
        objectPooling = FindObjectOfType<ObjectPooling>();
    }

    // Update is called once per frame
    private void Update()
    {
        SpawnBkgObjects();
    }

    /// <summary>
    /// Spawn randomized background objects
    /// </summary>
    private void SpawnBkgObjects()
    {
        var randomBkg = Random.Range(0, backgroundObjects.Count);

        if (bkg == null)
        {
            // Spawn a randomized background object
            bkg = objectPooling.GetObject(backgroundObjects[randomBkg]);

            // Set spawned background object to parent
            // for organization purposes
            bkg.transform.parent = spawnPoint;

            // Set the position of background object to the same as the spawn point
            bkg.transform.position = spawnPoint.position;

            spawnedBkgObjects.Add(bkg);
        }
        else if (bkg != null && randomBkg != lastRandomizedBkg)
        {
            lastRandomizedBkg = randomBkg;

            // If an background already exists
            // and is x floats on the Z-Axis from the previously spawned background object
            // spawn a new background object
            if (spawnedBkgObjects[spawnedBkgObjects.Count - 1].transform.position.z >= zThreshold)
            {
                var newbkg = objectPooling.GetObject(backgroundObjects[randomBkg]);

                newbkg.transform.parent = spawnPoint;

                // Clear list as we only want the last spawned background object
                spawnedBkgObjects.Clear();
                spawnedBkgObjects.Add(newbkg);

                // Set the position of background object to the same as the spawn point
                newbkg.transform.position = spawnPoint.position;
            }
        }
        else if (bkg != null && randomBkg == lastRandomizedBkg)
        {
            randomBkg = Random.Range(0, backgroundObjects.Count);
        }
    }
}