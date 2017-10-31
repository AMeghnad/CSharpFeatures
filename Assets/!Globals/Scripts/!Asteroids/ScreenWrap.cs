using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script depends on the SpriteRenderer component attached to the same GameObject
[RequireComponent(typeof(SpriteRenderer))]
public class ScreenWrap : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    // Camera
    private Bounds camBounds;
    private float camWidth;
    private float camHeight;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void UpdateCameraBounds()
    {
        // Calculate camera bounds
        Camera cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
        camBounds = new Bounds(cam.transform.position, new Vector2(camWidth, camHeight));
    }

    // Use late update since we are using the camera to wrap around
    void LateUpdate()
    {
        UpdateCameraBounds();
        // Store position and size in shorter variable names
        Vector3 pos = transform.position;
        Vector3 size = spriteRenderer.bounds.size;
        // Calculate the sprite's half width and half height
        float halfWidth = size.x / 2;
        float halfHeight = size.y / 2;
        float halfCamWidth = camWidth / 2f;
        float halfCamHeight = camHeight / 2f;
        //Check left
        if (pos.x + halfWidth < camBounds.min.x)
        {
            pos.x = camBounds.max.x + halfWidth;
        }
        // Check right
        if (pos.x - halfWidth > camBounds.max.x)
        {
            pos.x = camBounds.min.x - halfWidth;
        }
        // Check top
        if (pos.y - halfHeight > camBounds.max.y)
        {
            pos.y = camBounds.min.y - halfHeight;
        }
        //Check bottom
        if (pos.y + halfHeight < camBounds.min.y)
        {
            pos.y = camBounds.max.y + halfHeight;
        }
        // Set new position
        transform.position = pos;
    }
}
