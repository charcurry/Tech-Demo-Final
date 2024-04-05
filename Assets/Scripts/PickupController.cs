using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickupController : MonoBehaviour
{
    public int numberOfPickups;

    public TextMeshProUGUI countText;

    private int count;
    public AudioSource audioSource;

    public AudioClip pickupSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        numberOfPickups = CountPickups();
        Time.timeScale = 1f;
        SetCountText();
        count = 0;
    }

    // Update is called once per frame
    void SetCountText()
    {
        int pickupsLeft = numberOfPickups - count;
        countText.text = "Pickups Collected: " + count.ToString() + "/" + numberOfPickups;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            audioSource.PlayOneShot(pickupSound);
            other.gameObject.SetActive(false);
            count ++;
            SetCountText();
        }

    }

    public int CountPickups()
    {
        return GameObject.FindGameObjectsWithTag("Pickup").Count();
    }
}
