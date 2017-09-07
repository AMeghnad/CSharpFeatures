using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class Enemy : MonoBehaviour
    {
        public float health = 100f; // Enemies' health

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DealDamage(float damage)
        {
            // SET health -= damage
            health -= damage;
            // IF health <=0
            if (health <= 0)
            {
                // Destroy the enemy
                Destroy(gameObject);
            }

        }
    }
}

