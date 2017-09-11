using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{

    public class Projectile : MonoBehaviour
    {
        public float damage = 50f; // Damage dealth to whatever gets hit
        public float speed = 50f;  // Speed the projectile travels
        public Vector3 direction;  // Direction the projectile travels

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // LET velocity = direction.normalised x speed            
            Vector3 velocity = direction.normalized * speed;
            // SET projectile's position += velocity x deltaTime
            transform.position += velocity * Time.deltaTime;
        }

        void OnTriggerEnter(Collider col)
        {
            // LET e = col's enemy opponent
            Enemy e = col.GetComponent<Enemy>();
            // IF e != null
            if (e != null)
            {
                // CALL e.DealDamage(damage)
                e.DealDamage(damage);
                // Destroy gameObject
                Destroy(gameObject);
            }

            // IF col's name == "Ground"
            if (col.name == "Ground")
            {
                // Destroy the projectile
                Destroy(gameObject);
            }
        }
    }
}
