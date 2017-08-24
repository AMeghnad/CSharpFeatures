using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Billiards
{
    public class Cue : MonoBehaviour
    {
        public Ball targetball; // Target ball selected (which is generally the Cue ball)
        public float minPower = 0f;  // The min power which maps to the distance
        public float maxPower = 20f; // The max power which maps to the distance
        public float maxDistance = 5f; // The maximum distance in units the cue can be dragged back

        private float hitPower; // The final calculated hit power to fire the ball
        private Vector3 aimDirection; // The aim direction the ball should fire
        private Vector3 prevMousPos; // The mouse position obtained when left clicking
        private Ray mouseRay; // The ray of the mouse

        // Helps visualise the mouse ray and direction and fire
        void OnDrawGizmos()
        {
            Gizmos.DrawLine(mouseRay.origin, mouseRay.origin + mouseRay.direction * 1000);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(targetball.transform.position, targetball.transform.position + aimDirection * hitPower);
        }
        
        // Update is called once per frame
        void Update()
        {
            // Check if left mouse button is pressed
            if (Input.GetMouseButtonDown(0))
            {
                // Store the click position as the 'prevMousePos'
                prevMousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            // Check if left mouse button is pressed
            if (Input.GetMouseButton(0))
            {
                // Perform drag mechanic
                Drag();
            }
            else
            {
                // Perform aim mechanic
                Aim();
            }

            // Check if left mouse button is up
            if (Input.GetMouseButtonUp(0))
            {
                // Hit the ball
                Fire();
            }
        }

        // Rotates the cue to wherever the mouse is pointing (using Raycast) 
        void Aim()
        {
            // Calculate mouse ray before performing raycast
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Raycast hit container for the hit information
            RaycastHit hit;
            // Perform the raycast
            if(Physics.Raycast(mouseRay, out hit))
            {
                // Obtain direction from the cue's position to the raycast's hit point
                Vector3 dir = transform.position - hit.point;
                // Convert direction to angle in degrees
                float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                // Rotate towards the angle
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
                // Position cue to the ball's position
                transform.position = targetball.transform.position;
            }
        }

        // Deactivates the cue
        void Deactivate()
        {
            gameObject.SetActive(false);
        }
        
        // Activates the cue
        void Activate()
        {
            Aim();
            gameObject.SetActive(true);
        }

        // Allows you to drag the cue back and calculates force dealt to the ball
        void Drag()
        {
            // Score target ball's position in smaller variable
            Vector3 targetPos = targetball.transform.position;
            // Get mouse position in world units
            Vector3 curMousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Calculate distance from previous mouse position to the current mouse position
            float distance = Vector3.Distance(prevMousPos, curMousepos);
            // Clamp the distance between 0 and maxDistance
            float distPercentage = distance / maxDistance;
            // Use percentage of distance to map to the minPower - maxPower values
            hitPower = Mathf.Lerp(minPower, maxPower, distPercentage);
            // Position the cue back using distance
            transform.position = targetPos - transform.forward * distance;
            // Get direction to target ball
            aimDirection = (targetPos - transform.position).normalized;
        }

        // Fires off the ball
        void Fire()
        {
            // Hit the ball with direction and power
            targetball.Hit(aimDirection, hitPower);
            // Deactivate the cue when done
            Deactivate();
        }
    }
}
