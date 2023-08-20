using System.Collections;
using Nomnom.RaycastVisualization;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controller.Controller
{
    [RequireComponent(typeof(Rigidbody))]
    public class OffsetAIBoatController : AIBoatController
    {
        public float offset = 10f;
        [FormerlySerializedAs("slowDownArea")] public bool inSlowDownArea;

        protected override void Start()
        {
            inSlowDownArea = Vector3.Distance(transform.position, target.transform.position) < offset;
            
            if (target != null)
            {
                StartCoroutine(LookForPlayer());
            }
            
        }
        
        protected override IEnumerator LookForPlayer()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.5f);
                var position = transform.position;
                var direction = target.transform.position - position;
                var ray = new Ray(position, direction);
                if (VisualPhysics.Raycast(ray, out var hit, 1000))
                {
                    playerInRange = hit.collider.gameObject == target;
                    if (playerInRange)
                    {
                        inSlowDownArea = Vector3.Distance(transform.position, target.transform.position) < offset;
                    }
                }
            }
        }


        protected new void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            if (inSlowDownArea)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawLine(transform.position, target.transform.position);
            }

            Gizmos.DrawWireSphere(transform.position, offset);
        }

        protected new void FixedUpdate()
        {
            if (target == null) return;
            if (playerInRange == false) return;
            Vector3 movementForce = transform.forward * (inSlowDownArea ? moveForce * 0.8f : moveForce);
            rb.AddForce(movementForce, forceMode);
            RotateToTarget();
        }
    }
}