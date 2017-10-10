using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inheritance
{
    public class Splody : Enemy
    {
        [Header("Splodey")]
        public float splosionRadius = 5f;
        public float splosionRate = 3f;
        public float impactForce = 10f;
        public GameObject splosionParticles;

        private float splosionTimer = 0f;

        public override void Attack()
        {
            // Start ignitionTimer
            // IF splosionTimer > splosionRate
                // Call Splode()
        }

        void Splode()
        {
            // Perform Physics OverlapSphere with splosionRadius
                // Loop through all hits
                    // If player
                        // Add impactForce to rigidbody
            // Destroy self

        }
    }
}
