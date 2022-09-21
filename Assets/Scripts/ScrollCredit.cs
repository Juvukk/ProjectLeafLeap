using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCredit : MonoBehaviour
{
    [SerializeField] private float Top;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 startpos;

    // Start is called before the first frame update
    private void Start()
    {
        startpos = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);

        if (transform.localPosition.y > Top)
        {
            transform.position = startpos;
        }
    }
}