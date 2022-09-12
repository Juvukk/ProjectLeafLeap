using UnityEngine;

public class ObjectPoolingReturnGameObject : MonoBehaviour
{
    private ObjectPooling objectPool;
    private bool isobjectPoolNotNull;
    [SerializeField] private float timeToDeactivate = 5f;
    private float timeSinceDeactivation = 0f;

    private void Start()
    {
        isobjectPoolNotNull = objectPool != null;
        objectPool = FindObjectOfType<ObjectPooling>();
    }

    private void Update()
    {
        timeSinceDeactivation += Time.deltaTime;

        if (!isobjectPoolNotNull && !(timeSinceDeactivation >= timeToDeactivate)) return;

        // Add to the queue
        objectPool.ReturnGameObject(this.gameObject);
        // Reset timer
        timeSinceDeactivation = 0f;
    }
}