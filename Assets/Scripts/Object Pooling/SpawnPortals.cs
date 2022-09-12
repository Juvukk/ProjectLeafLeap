using UnityEngine;

public class SpawnPortals : MonoBehaviour
{
    [SerializeField] private float timeToSpawn = 5f;
    private float timeSinceSpawn;
    private ObjectPooling objectPool;
    [SerializeField] private GameObject chooknPrefab;
    [SerializeField] private GameObject portalPrefab;

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPooling>();

        if (chooknPrefab == null || portalPrefab == null)
        {
            Debug.LogWarning("Missing chookn and/or portal prefab");
            return;
        } 
    }

    private void Update()
    {
        timeSinceSpawn += Time.deltaTime;

        if (timeSinceSpawn >= timeToSpawn)
        {
            GameObject newChooknPrefab = objectPool.GetObject(chooknPrefab);
            GameObject newPortalPrefab = objectPool.GetObject(portalPrefab);

            int randomX = Random.Range(-300, 300);
            int randomY = Random.Range(0, 100);
            int randomZ = Random.Range(300, 360);

            Quaternion rot = new Quaternion(randomX, randomY, randomY, randomZ);
            Vector3 randomChooknVector = new Vector3(randomX, randomY, randomZ);
            // We want the portal spawn behind the chookn
            Vector3 randomPortalVector = new Vector3(randomX, randomY, randomZ+10f);

            newChooknPrefab.transform.SetPositionAndRotation(randomChooknVector, rot);
            newPortalPrefab.transform.position = randomPortalVector;

            timeSinceSpawn = 0f;
        }
    }
}