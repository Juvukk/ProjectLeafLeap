using UnityEngine;

public class ObjectPoolingReturnGameObject : MonoBehaviour
{
    private ObjectPooling objectPool;
    [SerializeField] private float timeToDeactivate = 5f;

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