using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareToEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            EventManager.endBegin?.Invoke();
    }
}