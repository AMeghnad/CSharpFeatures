using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Billiards
{
    public class Score : MonoBehaviour
    {
        private int count;

        // Use this for initialization
        void Start()
        {
            count = 0;
        }

        // Detect collisions
        void OnCollisionEnter(Collision other) {
            // If collided with Ball
            if (other.gameObject.tag == "Ball")
            {
                // Add 1 to score
                count += count;
            }
        }

    }
}
