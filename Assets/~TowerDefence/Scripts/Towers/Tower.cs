using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    public class Tower : MonoBehaviour
    {
        public Cannon cannon; // Reference to cannon inside of tower
        public float attackRate = 0.25f; // Rate of attack in seconds
        public float attackRadius = 5f; // Distance of attack in world units
        private float attackTimer = 0f; // Timer to count up to attackRate
        private List<Enemy> enemies = new List<Enemy>(); // List of enemies within radius

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            // SET attackTimer = attackTimer + deltaTime
            attackTimer += Time.deltaTime;
            // IF attackTimer >= attackRate
            if (attackTimer >= attackRate)
            {
                // CALL Attack()
                Attack();
                // SET attackTimer = 0
                attackTimer = 0;
            }
        }

        void OnTriggerEnter(Collider col)
        {
            // LET e = col's enemy component
            Enemy e = col.GetComponent<Enemy>();
            // If e != null
            if (e != null)
            {
                // Add e to enemies list
                enemies.Add(e);
            }
        }

        void OnTriggerExit(Collider col)
        {
            // Let e = col's enemy component
            Enemy e = col.GetComponent<Enemy>();
            // If e != null
            if (e != null)
            {
                // Remove e from enemies list
                enemies.Remove(e);
            }
        }

        Enemy GetClosestEnemy()
        {
            // Let closest = null
            Enemy closest = null;
            // LET minDistance = float.MaxValue
            float minDistance = float.MaxValue;
            // FOREACH enemy in enemies
            foreach (Enemy e in enemies)
            {
                // LET distance = the distance between transform's position and enemy's position
                float distance = Vector3.Distance(transform.position, e.transform.position);
                // IF distance < minDistance
                if (distance < minDistance)
                {
                    //SET minDistance = distance
                    minDistance = distance;
                    // SET closest = enemy
                    closest = e;
                }
            }
            // RETURN closest
            return closest;
        }

        void Attack()
        {
            // Let closest to GetClosesEnemy()
            Enemy closest = GetClosestEnemy();
            // IF closest != null
            if (closest != null)
            {
                // CALL cannon.Fire and pass closest as argument
                cannon.Fire(closest);
            }
        }
    }
}
