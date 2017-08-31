using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breakout
{
    public class Ball : MonoBehaviour
    {
        public float speed = 5f; // speed at which the ball travels

        public Vector3 velocity; // velocity = direction x speed

        // send the ball flying in a given direction
        public void Fire(Vector3 direction)
        {
            // Calculate velocity
            velocity = direction * speed;
        }

        // Detect collisions
        void OnCollisionEnter2D(Collision2D other)
        {
            // Grab the contact point of collision
            ContactPoint2D contact = other.contacts[0];
            // Calculate reflect using velocity and normal
            Vector3 reflect = Vector3.Reflect(velocity, contact.normal);
            // Redirecting the velocity to reflection
            velocity = reflect.normalized * velocity.magnitude;
            if (other.gameObject.CompareTag("Block"))
            {
               Destroy(other.gameObject);
            }
        }
     
        // Update is called once per frame
        void Update()
        {
            // Move position based on velocity
            transform.position += velocity * Time.deltaTime;
        }
    }
}