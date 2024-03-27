
using System.Collections;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public float slideDistance;
    public float slideSpeed;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool isOpen = false;
    private Coroutine doorCoroutine;
    public GameObject door;

    private void Start()
    {
        initialPosition = door.transform.position;
        targetPosition = initialPosition + Vector3.up * slideDistance;
    }

    private void OnTriggerEnter(Collider other)
    {
            OpenDoor();
    }

    private void OnTriggerExit(Collider other)
    {
            CloseDoor();
    }

    private void OpenDoor()
    {
        if (!isOpen)
        {
            if (doorCoroutine != null)
            {
                StopCoroutine(doorCoroutine);
            }

            doorCoroutine = StartCoroutine(MoveDoor(targetPosition));
            isOpen = true;
        }
    }

    private void CloseDoor()
    {
        if (isOpen)
        {
            if (doorCoroutine != null)
            {
                StopCoroutine(doorCoroutine);
            }

            doorCoroutine = StartCoroutine(MoveDoor(initialPosition));
            isOpen = false;
        }
    }

    private IEnumerator MoveDoor(Vector3 target)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = door.transform.position;

        while (elapsedTime < 1f)
        {
            door.transform.position = Vector3.Lerp(startingPosition, target, elapsedTime);
            elapsedTime += Time.deltaTime * slideSpeed;
            yield return null;
        }

        door.transform.position = target;
    }

}