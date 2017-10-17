using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Inheritance
{
    public class Enemy : MonoBehaviour
    {
        [Header("Enemy")]
        public Transform target;
        public int health = 100;
        public int damage = 20;
        public float attackRate = 0.5f;
        public float attackRange = 2f;
        public float attackDuration = 1f;

        protected NavMeshAgent nav;
        protected Rigidbody rigid;

        private float attackTimer = 0f;       

        protected virtual void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
            rigid = GetComponent<Rigidbody>();
        }

        protected virtual void Attack() { }
        protected virtual void OnAttackEnd() { }

        IEnumerator AttackDelay(float delay)
        {
            // stop navigation
            nav.Stop();
            yield return new WaitForSeconds(delay);
            
            if (nav.isOnNavMesh)
            {
                // resume navigation
                nav.Resume();
            }
            // CALL OnAttackEnd()
            OnAttackEnd();
        }

        protected virtual void Update()
        {
            if (target == null)
            {
                return;
            }

            // Increase attack Timer
            attackTimer += Time.deltaTime;
            // IF attackTimer > attackRate
            if (attackTimer >= attackRate)
            {
                // Set distance from enemy to target
                float distance = Vector3.Distance(transform.position, target.position);
                // IF distance < attackRange
                if (distance <= attackRange)
                {
                    // Call Attack()
                    Attack();
                    // reset attackTimer
                    attackTimer = 0f;
                    StartCoroutine(AttackDelay(attackDuration));
                }
            }
            if (gameObject.activeSelf)
            {
                // Navigate to target
                nav.SetDestination(target.position);
            }
        }
    }
}
