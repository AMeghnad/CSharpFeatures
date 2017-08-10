using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Variables
{

    public class MovementScript : MonoBehaviour
    {
        public float movementSpeed = 20f;
        public float rotationSpeed = 2f;

        // Update is called once per frame
        void Update()
        {
            // Get Horizontal input
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            //                    Direction       X Input(sign)  X Speed         X DeltaTime
            transform.position += transform.right * inputV * movementSpeed * Time.deltaTime;
            //               Direction       X Speed         X DeltaTime  
            transform.eulerAngles += Vector3.back * inputH * rotationSpeed * Time.deltaTime;
        }
    }
}