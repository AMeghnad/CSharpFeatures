using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class Moving : MonoBehaviour
    {
        public float acceleration = 50f;
        public float rotationSpeed = 360f;

        private Rigidbody2D rigid;
        // Use this for initialization
        void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        // Accelerates the player
        void Accelerate()
        {
            float inputV = Input.GetAxis("Vertical");
            rigid.AddForce(transform.up * inputV * acceleration);
        }

        // Rotates the player (in degrees)
        void Rotate()
        {
            float inputH = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.back, rotationSpeed * inputH * Time.deltaTime);
        }

        // Update is called once per frame
        void Update()
        {
            Accelerate();
            Rotate();
        }
    }
}
