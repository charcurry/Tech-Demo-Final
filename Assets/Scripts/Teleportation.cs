using System.Collections;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public Transform destination;
    public GameObject player;
    public float heightOffset = 1;
    public bool arrived;
    public string gameObjectTag;

    public float chargingTime;

    private AudioSource audioSource;
    private AudioSource destinationAudioSource;
    public AudioClip teleporterCharge;
    public AudioClip teleporterTeleport;

    private float chargeTimeRemaining;
    private bool isCharging;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        destinationAudioSource = destination.GetComponent<AudioSource>();
        chargeTimeRemaining = chargingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCharging)
        {
            chargeTimeRemaining -= Time.deltaTime;
            if (chargeTimeRemaining <= 0)
            {
                chargeTimeRemaining = chargingTime;
                isCharging = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!arrived)
        {
            if (gameObjectTag != "")
            {
                if (other.gameObject.CompareTag(gameObjectTag))
                {
                    StartCoroutine(Teleporter(other.gameObject));
                }
            }
            else
            {
                StartCoroutine(Teleporter(other.gameObject));
            }
        }
    }

    IEnumerator Teleporter(GameObject target)
    {
        float originalPitch = audioSource.pitch;
        audioSource.pitch = teleporterCharge.length / chargingTime;
        audioSource.PlayOneShot(teleporterCharge);
        isCharging = true;
        yield return new WaitForSeconds(chargingTime);
        target.transform.position = destination.position + new Vector3(0, heightOffset, 0);
        destination.GetComponent<Teleportation>().arrived = true;
        destinationAudioSource.PlayOneShot(teleporterTeleport);
        audioSource.pitch = originalPitch;
    }

    private void OnTriggerExit(Collider other)
    {
        if (arrived)
        {
            arrived = false;
        }
        else
        {
            StopAllCoroutines();
            audioSource.Stop();
        }
    }
}
