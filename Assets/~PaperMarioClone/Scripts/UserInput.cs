using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaperMarioClone
{
    [RequireComponent(typeof(PlayerController))]
    public class UserInput : MonoBehaviour
    {
        private PlayerController pController;

        // Use this for initialization
        void Start()
        {
            pController = GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            // Get inputH and inputH
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            // Move the PlayerController
            pController.Move(inputH, inputV);
            // Is space being pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Jump
                pController.Jump();
            }
        }
    }
}
