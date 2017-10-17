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

        protected override void Update()
        {
            base.Update();

            // Start ignitionTimer
            splosionTimer += Time.deltaTime;
        }

        void Splode()
        {
            // Perform Physics OverlapSphere with splosionRadius
            Collider[] hits = Physics.OverlapSphere(transform.position, splosionRadius);
            // Loop through all hits
            foreach (var hit in hits)
            {
                Health h = hit.GetComponent<Health>();
                // If player
                if (h != null)
                {
                    h.TakeDamage(damage);                    
                }
                Rigidbody r = hit.GetComponent<Rigidbody>();
                if(r != null)
                {
                    Vector3 dir = hit.transform.position - transform.position;
                    r.AddForce(dir * impactForce, ForceMode.Impulse);
                }
            }
        }

        protected override void OnAttackEnd()
        {
            // IF splosionTimer > splosionRate
            if (splosionTimer > splosionRate)
            {
                // Call Splode()
                Splode();
                Destroy(gameObject);
            }
        }
    }
}
