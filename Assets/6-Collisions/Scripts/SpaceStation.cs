using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collisions
{
    public class SpaceStation : MonoBehaviour
    {
        public float rotationSpeed = 20f;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}
