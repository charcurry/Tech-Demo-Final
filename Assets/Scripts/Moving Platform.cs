using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private GameObject[] positions;
    public int destination;

    public float deceleration;
    public float acceleration;

    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        positions = GameObject.FindGameObjectsWithTag("Waypoint");

        if (positions.Length != 0)
        {
            destination = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(positions[destination].transform.position, transform.position);

        if (distance < 0.01f)
        {
            destination++;
            moveSpeed = 0;
        }

        if (destination >= positions.Length)
        {
            positions.Reverse();
            destination = 0;
        }

        moveSpeed = Mathf.Lerp(moveSpeed, 1.0f, Time.deltaTime * acceleration);

        float t = 1 - Mathf.Exp(-moveSpeed * Time.deltaTime * deceleration);

        transform.position = Vector3.Lerp(transform.position, positions[destination].transform.position, t);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
