using UnityEngine;
using System.Collections;
using GGL;

namespace MOBA
{
    public class Seek : SteeringBehaviour
    {
        // Public:
        public Transform target;
        public float stoppingDistance = 0f;

        public override Vector3 GetForce()
        {
            // SET force to Vector3 zero
            Vector3 force = Vector3.zero;

            // IF target is null, return force
            if (target == null) return force;

            // SET desiredForce
            Vector3 desiredForce = target.position - transform.position;

            #region GizmosGL
            GizmosGL.AddLine(transform.position, target.position, 0.1f, 0.1f, Color.red, Color.red);
            Sphere s = GizmosGL.AddSphere(target.position, stoppingDistance * 2f);
            s.color = new Color(0, 1, 1, 0.2f);
            #endregion

            // Check if the direction is valid
            if (desiredForce.magnitude > stoppingDistance)
            {
                // Calculate force
                desiredForce = desiredForce.normalized * weighting;
                force = desiredForce - owner.velocity;
            }

            // Return the force!
            return force;
        }
    }
}