using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Instantiation : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float timeAlive = 10f;
    public float timeDead = 5f;
    private bool isGrounded;
    private AudioSource audioSource;
    public AudioClip hittingGroundSound;
    private GameObject spawnedPrefab;

    public float distance;

    void Start()
    {
        spawnedPrefab = null;
        StartCoroutine(SpawnPrefab());
    }

    private void Update()
    {
        if (spawnedPrefab != null)
        {
            audioSource = spawnedPrefab.GetComponent<AudioSource>();
            if (!isGrounded)
            {
                CheckGround(spawnedPrefab);
            }
        }
    }

    private void CheckGround(GameObject target)
    {
        Debug.DrawRay(target.transform.position, Vector3.down * distance, Color.red);

        if (Physics.Raycast((target.transform.position), Vector3.down * distance, out RaycastHit hit, distance))
        {
            isGrounded = true;
            audioSource.PlayOneShot(hittingGroundSound);
        }
        else
        {
            isGrounded = false;
        }
    }

    IEnumerator SpawnPrefab()
    {
        while (true)
        {
            spawnedPrefab = Instantiate(prefabToSpawn, new Vector3(5,5,5), Quaternion.identity);

            yield return new WaitForSeconds(timeAlive);

            Destroy(spawnedPrefab);
            isGrounded= false;

            yield return new WaitForSeconds(timeDead);
        }
    }
}