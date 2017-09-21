using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RollABall
{   
    public class Player : MonoBehaviour
    {
        public float movementSpeed = 50f;
        private Rigidbody rigid;

        void Awake()
        {
            rigid = GetComponent<Rigidbody>();
        }
       
        // Update is called once per frame
        void Update()
        {
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");

            // Calculate Quaternion from Camera's Y euler rotation
            float camY = Camera.main.transform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, camY, 0);

            Vector3 inputDir = rotation * new Vector3(inputH, 0, inputV);
            rigid.AddForce(inputDir * movementSpeed);
        }
    }
}
