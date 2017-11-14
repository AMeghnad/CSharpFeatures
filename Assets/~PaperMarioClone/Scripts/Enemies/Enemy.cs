using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaperMarioClone
{
    [RequireComponent(typeof(BoxCollider))]
    public class Enemy : MonoBehaviour
    {
        public float movementSpeed = 4f;
        public float rayDistance = 1.2f;        
        public enum MovementDirection
        {
            IDLE,
            LEFT,
            RIGHT
        }
        public MovementDirection movementDirection;

        private BoxCollider box;
        private Ray leftRay, rightRay;

        private void Awake()
        {
            box = GetComponent<BoxCollider>();
        }
       
        void RecalculateRays()
        {
            Vector3 halfSize = box.bounds.size * 0.5f;
            Vector3 leftPos = transform.position - Vector3.left * halfSize.x;
            Vector3 rightPos = transform.position - Vector3.right * halfSize.x;
            leftRay = new Ray(leftPos, Vector3.down);
            rightRay = new Ray(rightPos, Vector3.down);
        }

        void OnDrawGizmos()
        {
            box = GetComponent<BoxCollider>();
            RecalculateRays();
            Gizmos.color = Color.red;
            Gizmos.DrawLine(leftRay.origin, leftRay.origin + leftRay.direction * rayDistance);
            Gizmos.DrawLine(rightRay.origin, rightRay.origin + rightRay.direction * rayDistance);
        }

        public void Move()
        {
            RecalculateRays();
            //Store transform's position in smaller variable
            Vector3 pos = transform.position;

            // Perform raycast check
            bool isLeftHitting = Physics.Raycast(leftRay, rayDistance);
            bool isRightHitting = Physics.Raycast(rightRay, rayDistance);

            // Is the player close to left edge?
            if (isLeftHitting && !isRightHitting)
            {
                // Move right
                movementDirection = MovementDirection.RIGHT;
            }
            // Is the player close to right edge?
            else if (isRightHitting && !isLeftHitting)
            {
                //Move left
                movementDirection = MovementDirection.LEFT;
            }

            Vector3 dir = Vector3.zero;
            switch (movementDirection)
            {
                case MovementDirection.IDLE:
                    break;

                case MovementDirection.LEFT:
                    dir += Vector3.left;
                    break;

                case MovementDirection.RIGHT:
                    dir += Vector3.right;
                    break;
                default:
                    break;
            }

            // Apply movement speed and deltaTime
            pos += dir * movementSpeed * Time.deltaTime;

            // Set the position to newly modified pos
            transform.position = pos;
        }

        // Update is called once per frame
        public virtual void Update()
        {
            Move();
        }

        public virtual void Damage(int combo = 0)
        {

        }
    }
}
