using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inheritance
{
    public class Splodey : Enemy
    {
        [Header("Splodey")]
        public float explosionRadius = 5f;
        public float knockback = 20f;
        
        // Polymorphism!
        protected override void Attack()
        {
            // Play an animation
            // Perform explosion physics!
            Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (var hit in hits)
            {
                Health h = hit.gameObject.GetComponent<Health>();
                if(h != null)
                {
                    h.TakeDamage(damage);
                }              
            }

            // Add explosion force
            rigid.AddExplosionForce(knockback, transform.position, explosionRadius);
        }

        protected override void OnAttackEnd()
        {
            Destroy(gameObject);
        }
    }
}
