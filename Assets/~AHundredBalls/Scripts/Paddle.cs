using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hundredballs
{
    public class Paddle : MonoBehaviour
    {
        public bool isOpened;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isOpened = !isOpened;
            }
        }
    }
}
