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

    private Camera cam;
    private float sizeX, sizeY, ratio, targetaspect;
    private float scalewidth = 1;
    private float scaleheight = 1;

    private Func<Vector3> GetCameraFollowPositionFunc;
    public void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Transform>();
        this.GetCameraFollowPositionFunc = () => playerTransform?.position ?? new Vector3(0,0);
        this.cam = GetComponent<Camera>();
        targetaspect = 16.0f / 9.0f;
    }

    // Update is called once per frame
    void Update()
    {
        sizeY = cam.orthographicSize * 2;
        ratio = (float)Screen.width / (float)Screen.height;
        sizeX = sizeY * ratio;

        scaleheight = ratio / targetaspect;
        scalewidth = 1.0f / scaleheight;

        if (scaleheight < 1.0f)
        {
            Rect rect = cam.rect;

            rect.width = 1.0f;

            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            cam.rect = rect;

        }
        else // add pillarbox
        {

            Rect rect = cam.rect;

            rect.width = scalewidth;

            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            cam.rect = rect;

        }
    }

    private void FixedUpdate()
    {
        if(playerTransform != null)
        {
            Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
            cameraFollowPosition.z = transform.position.z;

            Vector3 boundPosition = new Vector3(
                Mathf.Clamp(cameraFollowPosition.x, (minValues.x + 3.200652f - (sizeX / 2) * scalewidth), (maxValues.x - 3.200652f + (sizeX / 2) * scalewidth)),
                Mathf.Clamp(cameraFollowPosition.y, minValues.y, maxValues.y),
                Mathf.Clamp(cameraFollowPosition.z, minValues.z, maxValues.z)
                );

            Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }
    }
}
