using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inheritance
{
    public class Charger : Enemy
    {
        [Header("Charger")]
        public float impactForce = 10f;
        public float knockback = 5f;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Attack()
        {
            // Add force to self
            rigid.AddForce(transform.forward * knockback, ForceMode.Impulse);            
        }

        void OnCollisionEnter(Collision col)
        {
            // If collision hits player
            if (col.gameObject != null && gameObject.tag == "Player")
            {
                Rigidbody r = col.collider.GetComponent<Rigidbody>();
                // Add impactForce to player
                r.AddForce(transform.forward * impactForce, ForceMode.Impulse);
            }
        }
    }
}
