using UnityEngine;

public class ObjectPoolingReturnGameObject : MonoBehaviour
{
    private ObjectPooling objectPool;
    [SerializeField] private float deactivationTimer;
    private float timer;

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPooling>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer <= deactivationTimer) return;

        timer = 0;

        objectPool.ReturnGameObject(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destroyer"))
        {
            objectPool.ReturnGameObject(this.gameObject);
        }
    }
}