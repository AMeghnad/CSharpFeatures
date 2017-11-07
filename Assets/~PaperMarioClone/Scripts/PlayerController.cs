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
            if(isGrounded)
            {
                // Cancel out gravity only if you're grounded
                gravity = Vector3.zero;
                // Is the controller jumping?
                if (jump)
                {
                    // Make character jump
                    gravity.y = jumpHeight;
                    jump = false;
                }
            }
            else
            {
                // Applying gravity
                gravity += Physics.gravity * Time.deltaTime;
            }

            // Apply movement
            movement += gravity;
            controller.Move(movement * Time.deltaTime);
        }
        // Controller Jump
        public void Jump()
        {
            if (isGrounded)
            {
                //Jump!
                jump = true;
            }
        }

        public void Move(float inputH, float inputV)
        {
            // is moveInJump enabled? OR
            // is moveInJump disabled AND controller isGrounded?
            if(moveInJump || (moveInJump == false && isGrounded))
            {
                inputDir = new Vector3(inputH, 0, inputV);
            }

            // Move player based on inputDir
            movement = inputDir;
        }
    }    
}
