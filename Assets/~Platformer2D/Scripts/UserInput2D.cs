using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    [RequireComponent(typeof(Controller2D))]
    public class UserInput2D : MonoBehaviour
    {
        private Controller2D controller;

        // Use this for initialization
        void Start()
        {
            controller = GetComponent<Controller2D>();
        }

        // Update is called once per frame
        void Update()
        {
            float inputH = Input.GetAxis("Horizontal");
            controller.Move(inputH);
            if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
            {
                controller.Jump();
            }
        }
    }
}
