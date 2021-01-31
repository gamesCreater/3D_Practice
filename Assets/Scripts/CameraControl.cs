using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Camera camera;
    public Transform playerTransform;

    private Vector3 cameraOffset = new Vector3(-12f,12f,-12f);

    [Range(0.01f, 1f)]
    public float smoothMove = 0.05f; 

    private void Awake()
    {
        camera = Camera.main;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        LookAtPers();
        cameraOffset = transform.position - playerTransform.position;
    }

    public void LookAtPers()
    {
        camera.transform.position = playerTransform.position + cameraOffset;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = playerTransform.position + cameraOffset;

        transform.position = Vector3.Slerp(camera.transform.position, newPosition, smoothMove);
    }


}
