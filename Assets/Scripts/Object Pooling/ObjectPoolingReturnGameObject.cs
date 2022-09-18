using UnityEngine;

public class ObjectPoolingReturnGameObject : MonoBehaviour
{
    private ObjectPooling objectPool;

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPooling>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destroyer"))
        {
            objectPool.ReturnGameObject(this.gameObject);
        }
    }
}