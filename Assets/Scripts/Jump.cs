using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private GameObject LeafPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("end Game");
            LeafPrefab.SetActive(true);
            EventManager.endGame?.Invoke();
        }
    }
}