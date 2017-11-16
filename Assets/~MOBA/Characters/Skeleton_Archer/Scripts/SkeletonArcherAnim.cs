using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MOBA
{
    public class SkeletonArcherAnim : MonoBehaviour
    {
        public Animator anim;

        private AIAgent aiAgent;

        void Start()
        {
            aiAgent = GetComponent<AIAgent>();
            // Freeze position on start
            aiAgent.updatePosition = false;
            aiAgent.updateRotation = false;
        }

        void Update()
        {
            AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);
            if (!state.IsName("spawn"))
            {
                aiAgent.updatePosition = true;
                aiAgent.updateRotation = true;
                float moveSpeed = aiAgent.velocity.magnitude;
                anim.SetFloat("MoveSpeed", moveSpeed);
            }            
        }
    }
}