using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiation : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float timeAlive = 10f;
    public float timeDead = 5f;

    void Start()
    {
        StartCoroutine(SpawnPrefab());
    }

    IEnumerator SpawnPrefab()
    {
        while (true)
        {
            GameObject spawnedPrefab = Instantiate(prefabToSpawn, new Vector3(5,5,5), Quaternion.identity);

            yield return new WaitForSeconds(timeAlive);

            Destroy(spawnedPrefab);

            yield return new WaitForSeconds(timeDead);
        }
    }
}