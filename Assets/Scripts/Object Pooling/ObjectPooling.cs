using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();

    public GameObject GetObject(GameObject gameObject)
    {
        // Find a match
        if (objectPool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
        {
            // No match, create a new object
            if (objectList.Count == 0)
                return CreateNewObject(gameObject);

            // Dequeue
            GameObject _object = objectList.Dequeue();
            // Activate object
            _object.SetActive(true);
            return _object;
        }
        return CreateNewObject(gameObject);
    }

    private GameObject CreateNewObject(GameObject gameObject)
    {
        GameObject newGO = Instantiate(gameObject);
        // Not setting its name will break the object pooling
        newGO.name = gameObject.name;
        return newGO;
    }

    public void ReturnGameObject(GameObject gameObject)
    {
        if (objectPool.TryGetValue(gameObject.name, out Queue<GameObject> objectList))
        {
            // Add object to the queue
            objectList.Enqueue(gameObject);
        }
        else
        {
            // Create a new queue if the object doesn't belong in any existing queue
            Queue<GameObject> newObjectQueue = new Queue<GameObject>();
            newObjectQueue.Enqueue(gameObject);
            objectPool.Add(gameObject.name, newObjectQueue);
        }
        gameObject.SetActive(false);
    }
}