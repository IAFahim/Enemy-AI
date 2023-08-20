using System;
using System.Collections;
using Controller.ScriptableObjects.Spawner;
using Nomnom.RaycastVisualization;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controller.Controller
{
    [RequireComponent(typeof(Rigidbody))]
    public class AIBoatController : MonoBehaviour
    {
        public GameObject target;

        [FormerlySerializedAs("spawnControllerScriptableObject")]
        public SpawnControllerSO spawnControllerSo;

        public Rigidbody rb;
        public float moveForce = 10f; // Adjust the force for movement
        public float rotationTorque = 5f; // Adjust the torque for rotation
        public ForceMode forceMode;
        [FormerlySerializedAs("inRange")] [FormerlySerializedAs("seenPlayer")] public bool playerInRange;


        protected virtual void Start()
        {
            if (target != null)
            {
                StartCoroutine(LookForPlayer());
            }
        }

        protected virtual IEnumerator LookForPlayer()
        {
            while (true)
            {
                if (playerInRange) yield break;
                yield return new WaitForSeconds(0.5f);
                var position = transform.position;
                var direction = target.transform.position - position;
                var ray = new Ray(position, direction);
                if (VisualPhysics.Raycast(ray, out var hit, 1000))
                {
                    playerInRange = hit.collider.gameObject == target;
                }
            }
        }

        protected void FixedUpdate()
        {
            if (target == null) return;
            if (playerInRange == false) return;
            Vector3 movementForce = transform.forward * (moveForce);
            rb.AddForce(movementForce, forceMode);
            RotateToTarget();
        }

        protected void RotateToTarget()
        {
            var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation,
                rotationTorque * Time.fixedDeltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == target)
            {
                spawnControllerSo.DeSpawn(gameObject);
            }
        }

        protected virtual void OnDrawGizmos()
        {
            if (target == null) return;
            if (playerInRange)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, target.transform.position);
            }
        }
    }
}