using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collisions
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Reflect : MonoBehaviour
    {
        private Rigidbody2D rigid;
        // Use this for initialization
        void Start()
        {
        rigid = GetComponent<Rigidbody2D>();
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            // Input Direction for the reflect function
            Vector3 inDirection = rigid.velocity.normalized;
            // Contact information with collision
            ContactPoint2D contact = other.contacts[0];
            // Input normal of the contact's surface
            Vector3 inNormal = contact.normal;
            // Reflection vector pointing int the direction we want to go
            Vector3 reflect = Vector3.Reflect(inDirection, inNormal);
            // Newly calculated force from reflection
            Vector3 newForce = reflect * rigid.velocity.magnitude;
            // Replace velocity on object with reflection direction
            rigid.velocity = newForce;
        }
    }
}
