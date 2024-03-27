using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{

    public Camera cameraToLookAt;
    private Vector3 offset;
    public float distanceAboveCharacter;
    public GameObject NPC; 

    private void Start()
    {
        offset = new Vector3 (0 , distanceAboveCharacter, 0);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = NPC.transform.position + offset;
        transform.LookAt(cameraToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
    }
}
