using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed;

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(speed * Time.deltaTime, 0, 0, Space.Self);
    }
}