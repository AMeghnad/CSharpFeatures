using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inheritance
{

    public class Charger : Enemy
    {
        [Header("Charger")]
        public float chargeForce = 5f;
        public float knockback = 4.5f;

        void Awake()
        {

        }
    }
}
