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
    private float sizeX, sizeY, ratio, targetAspect;
    private float widthScale = 1;
    private float heightScale = 1;

    private Func<Vector3> GetCameraFollowPositionFunc;
    public void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Transform>();
        this.GetCameraFollowPositionFunc = () => playerTransform?.position ?? new Vector3(0,0);
        this.cam = GetComponent<Camera>();
        targetAspect = 16.0f / 9.0f;
    }

    // Update is called once per frame
    void Update()
    {
        sizeY = cam.orthographicSize * 2;
        ratio = (float)Screen.width / (float)Screen.height;
        sizeX = sizeY * ratio;

        heightScale = ratio / targetAspect;
        widthScale = 1.0f / heightScale;

        Rect rect = cam.rect;
        rect.width = heightScale < 1    ? 1 : widthScale;
        rect.height = heightScale > 1   ? 1 : heightScale;
        rect.x = heightScale < 1        ? 0 : (1.0f - widthScale) / 2.0f;
        rect.y = heightScale > 1        ? 0 : (1.0f - heightScale) / 2.0f;

        cam.rect = rect;
    }
    //Framerate dependent Update()
    private void FixedUpdate()
    {
        if(playerTransform != null)
        {
            Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
            cameraFollowPosition.z = transform.position.z;

            Vector3 boundPosition = new Vector3(
                Mathf.Clamp(cameraFollowPosition.x, (minValues.x + 3.200652f - (sizeX / 2) * widthScale), (maxValues.x - 3.200652f + (sizeX / 2) * widthScale)),
                Mathf.Clamp(cameraFollowPosition.y, minValues.y, maxValues.y),
                Mathf.Clamp(cameraFollowPosition.z, minValues.z, maxValues.z)
                );

            Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }
    }
}
