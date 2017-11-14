using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaperMarioClone
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        public float walkSpeed = 20f;
        public float runSpeed = 30f;
        public float jumpHeight = 10f;
        public bool moveInJump = false;
        public bool isRunning = false;
        public bool isGrounded
        {
            get { return controller.isGrounded; }
        }
        [HideInInspector]
        public Vector3 inputDir;

        private CharacterController controller;
        private Vector3 gravity;
        private Vector3 movement;
        private bool jump = false;
        private bool jumpInstant = false; // << ADDED THIS 

        // Use this for initialization
        void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            // Is the controller running?
            if (isRunning)
                movement *= runSpeed;
            else
                movement *= walkSpeed;

            // Is the controller grounded?
            if (!isGrounded)
            {
                gravity += Physics.gravity * Time.deltaTime;
            }
            else
            {
                gravity = Vector3.zero;
                if (jump)
                {
                    gravity.y = jumpHeight;
                    jump = false;
                }
            }

            if (jumpInstant)
            {
                gravity.y = jumpHeight;
                jumpInstant = false;
            }
            movement += gravity;
            controller.Move(movement * Time.deltaTime);

        }
        // Controller Jump
        public void Jump(bool instant = false)
        {
            if (instant)
                jumpInstant = true;
            else
                jump = true;            
        }

        public void Move(float inputH, float inputV)
        {
            Vector3 inputDir = new Vector3(inputH, 0, inputV);
            movement = transform.TransformDirection(inputDir);
        }
    }
}
