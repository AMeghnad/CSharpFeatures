using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Billiards
{
    [RequireComponent(typeof(Rigidbody))]
    public class Ball : MonoBehaviour
    {
        public float gravity = -9.81f;

        private Rigidbody rigid;

        // Use this for initialization
        void Start()
        {
            rigid = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            rigid.velocity = rigid.velocity.normalized + Vector3.back * gravity;
        }
    }
}
