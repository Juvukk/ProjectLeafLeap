using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private Transform restartPos; private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");
        if (other.CompareTag("Destroyer"))
        {
            Debug.Log("collision with destroyer-reset");

            transform.position = restartPos.position;
            rb.velocity = Vector3.zero;
        }
    }
}