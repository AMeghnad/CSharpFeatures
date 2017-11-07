using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaperMarioClone
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PlayerController))]
    public class MarioAnimator : MonoBehaviour
    {        
        private Animator anim;
        private PlayerController pController;
        private bool jump = false;

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();
            pController = GetComponent<PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 inputDir = pController.inputDir;
            if(inputDir.x > 0)
            {
                anim.SetBool("IsFlipped", true);
            }
            else
            {
                anim.SetBool("IsFlipped", false);
            }
        }
    }
}

