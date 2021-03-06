﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breakout
{
    public class Paddle : MonoBehaviour
    {
        public float movementSpeed = 20f;
        public Ball currentBall;
        //Directions array default to two values
        public Vector2[] directions = new Vector2[]
        {
            new Vector2(-.5f, 0.5f),
            new Vector2(0.5f, 0.5f)
        };
        public bool isStart;

        // Use this for initialization
        void Start()
        {
            currentBall = GetComponentInChildren<Ball>();
            isStart = true;
        }

        public void Fire()
        {
            // Detach as child
            currentBall.transform.SetParent(null);
            // Generate random dir from list of directions
            Vector3 randomDir = directions[Random.Range(0, directions.Length)];
            // Fire off ball in randomDir
            currentBall.Fire(randomDir);
            isStart = false;
        }

        void CheckInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isStart == true)
                {
                    Fire();
                }                                                                                            
            }            
        }

        void Movement()
        {
            // Get input on the horizontal axis
            float inputH = Input.GetAxis("Horizontal");
            // Set force to direction (inputH to decide which direction)
            Vector3 force = transform.right * inputH;
            // Apply movement speed to force
            force *= movementSpeed;
            // Apply delta time to force
            force *= Time.deltaTime;
            // Add force to position
            transform.position += force;
        }

        // Update is called once per frame
        void Update()
        {
            CheckInput();
            Movement();
        }
    }
}
