using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Checkpoints : MonoBehaviour
{
    public Material activated;
    public Material deactivated;

    private AudioSource audioSource;
    public AudioClip checkpointActivateSound;

    [SerializeField] private GameObject[] checkpoints;
    [SerializeField] private GameObject currentCheckpoint;

    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        initialPosition = transform.position;
    }

    void OnDeath()
    {
        if (currentCheckpoint == null)
        {
            transform.position = initialPosition;
        }
        else if (currentCheckpoint.transform.position != null)
        {
            transform.position = currentCheckpoint.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Kill Box"))
        {
            OnDeath();
        }

        if (other.gameObject.CompareTag("Checkpoint"))
        {
            foreach (var checkpoint in checkpoints)
            {
                if (currentCheckpoint == null)
                {
                    currentCheckpoint = other.gameObject;
                    currentCheckpoint.transform.position = other.transform.position;
                    other.GetComponent<Renderer>().material = activated;
                    audioSource.PlayOneShot(checkpointActivateSound);
                    Debug.Log("no current checkpoint");
                }
                else if (other.gameObject != currentCheckpoint)
                {
                    currentCheckpoint.GetComponent<Renderer>().material = deactivated;
                    currentCheckpoint = other.gameObject;
                    currentCheckpoint.transform.position = other.transform.position;
                    other.GetComponent<Renderer>().material = activated;
                    audioSource.PlayOneShot(checkpointActivateSound);
                    Debug.Log("different checkpoint");
                }
            }
        }
    }
}
