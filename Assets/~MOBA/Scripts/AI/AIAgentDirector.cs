using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GGL;

/*
Obtains user selection on the nav mesh and 
'directs' all algents to that location using either
Seek or PathFollowing behaviours
*/

namespace MOBA
{
    [RequireComponent(typeof(Camera))]
    public class AIAgentDirector : MonoBehaviour
    {
        public LayerMask hitLayers;
        public float rayDistance = 1000f;
        public AIAgent[] agentsToDirect;

        private Camera cam;
        private Transform selectionPoint;

        void Start()
        {
            cam = GetComponent<Camera>();
            GameObject g = new GameObject("Target Location");
            selectionPoint = g.transform;
        }

        // Assigns target to all agents in 'agentsToDirect'
        void AssignTargetToAllAgents(Transform target)
        {
            // Loop through all agentsToDirect
            foreach (var agent in agentsToDirect)
            {
                Seek s = agent.GetComponent<Seek>();
                // Is seek attached to agent?
                if (s != null)
                    s.target = target; // Assign target to seek component on agent

                // PathFollowing
                PathFollowing p = agent.GetComponent<PathFollowing>();
                // Is PathFollowing attached to agent?
                if (p != null)
                    p.target = target; // Assign target to PathFollowing comonenet anafe

            }
        }

        void Update()
        {
            // Is mouse down?
            if (Input.GetMouseButtonDown(0))
            {
                // Calculate ray from camera (using mouse pos)
                Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                // Perform raycast
                if (Physics.Raycast(camRay, out rayHit, rayDistance, hitLayers))
                {
                    NavMeshHit navHit;
                    // Check if raycast point is in on nav mesh
                    if (NavMesh.SamplePosition(rayHit.point, out navHit, rayDistance, -1))
                    {
                        // Set selection point
                        selectionPoint.position = navHit.position;
                        // Assign target to all agents
                        AssignTargetToAllAgents(selectionPoint);
                    }
                }
            }
        }
    }
}
