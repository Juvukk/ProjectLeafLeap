using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Obstacle : MonoBehaviour
{
    private Rigidbody rb;

    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveObstacle();
    }

    private void MoveObstacle()
    {
        rb.AddForce(Vector3.forward, ForceMode.Force);
    }
}
