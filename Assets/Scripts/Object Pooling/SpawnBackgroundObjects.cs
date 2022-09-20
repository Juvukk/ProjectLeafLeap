using System.Collections.Generic;
using UnityEngine;

public class SpawnBackgroundObjects : MonoBehaviour
{
    [SerializeField] private List<GameObject> backgroundObjects = new List<GameObject>();
    [SerializeField] private Transform spawnPoint;
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

    private void SpawnBkgObjects()
    {
        var randomBkg = ReturnRandomizedBkg(backgroundObjects);

        if (bkg == null)
        {
            // Spawn a randomized background object
            bkg = objectPooling.GetObject(backgroundObjects[randomBkg]);

            bkg.transform.parent = spawnPoint;

            // Set the position of background object to the same as the spawn point
            bkg.transform.position = spawnPoint.position;

            spawnedBkgObjects.Add(bkg);
        }
        else if (bkg != null)
        {
            // If an background already exists,
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
    }

    private int ReturnRandomizedBkg(List<GameObject> backgroundObjects)
    {
        var rdm = Random.Range(0, backgroundObjects.Count);

        if (rdm != lastRandomizedBkg)
        {
            lastRandomizedBkg = rdm;
            return rdm;
        }
        else if (rdm == lastRandomizedBkg)
        {
            rdm = Random.Range(0, backgroundObjects.Count);
        }

        return rdm;
    }
}