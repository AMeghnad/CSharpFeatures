using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaperMarioClone
{
    [RequireComponent(typeof(Enemy))]
    public class RotateEnemyByMovement : MonoBehaviour
    {
        private Enemy e;

        // Use this for initialization
        void Start()
        {
            e = GetComponent<Enemy>();    
        }

        // Update is called once per frame
        void Update()
        {
            Quaternion rot = transform.rotation;
            if (e.movementDirection == Enemy.MovementDirection.LEFT)
                rot = Quaternion.AngleAxis(0, Vector3.up);
            else if (e.movementDirection == Enemy.MovementDirection.RIGHT)
                rot = Quaternion.AngleAxis(180, Vector3.up);
            transform.rotation = rot;            
        }
    }
}
