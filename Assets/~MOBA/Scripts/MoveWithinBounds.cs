using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MOBA
{
    public class MoveWithinBounds : MonoBehaviour
    {
        public float movementSpeed = 20f;
        public float zoomSensitivity = 10f;
        public CameraBounds bounds;

        // Update is called once per frame
        void Update()
        {
            // Store transform.position in smaller variable
            Vector3 pos = transform.position;
            // Get input
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            // Store input in vector (for movement)
            Vector3 inputDir = new Vector3(inputH, 0, inputV);
            pos += inputDir * movementSpeed * Time.deltaTime;
            // Get scroll wheel
            float inputScroll = Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;
            Vector3 scrollDir = transform.forward * inputScroll;
            pos += scrollDir;
            // Adjust position with bounds
            pos = bounds.GetAdjustedPos(pos);
            // Overwrite original position with adjustedPos
            transform.position = pos;

        }
    }
}
