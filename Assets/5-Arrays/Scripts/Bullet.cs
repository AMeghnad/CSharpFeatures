using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arrays
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 10f;
        public Vector2 direction;

        // Update is called once per frame
        void Update()
        {
            // position += direction x speed x deltaTime
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }
    }
}
