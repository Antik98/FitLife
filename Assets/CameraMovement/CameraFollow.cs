using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 cameraFollowPosition;
    public float CameraZoom;
    [Range(1, 10)]
    public float smoothFactor;
    public Vector3 minValues, maxValues;
    private Transform playerTransform;

    private Func<Vector3> GetCameraFollowPositionFunc;
    public void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        this.GetCameraFollowPositionFunc = () => playerTransform.position;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;

        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(cameraFollowPosition.x, minValues.x, maxValues.x),
            Mathf.Clamp(cameraFollowPosition.y, minValues.y, maxValues.y),
            Mathf.Clamp(cameraFollowPosition.z, minValues.z, maxValues.z)
            );

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}
