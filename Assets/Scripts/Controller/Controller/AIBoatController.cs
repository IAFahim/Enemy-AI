using System;
using System.Collections;
using Controller.ScriptAbles.Spawner;
using Nomnom.RaycastVisualization;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controller.Controller
{
    [RequireComponent(typeof(Rigidbody))]
    public class AIBoatController : MonoBehaviour
    {
        public Rigidbody rb;
        [FormerlySerializedAs("otherBoatController")] public BoatController targetBoatController;
        public float moveForce = 500f;
        public float rotationTorque = 5f;
        public ForceMode forceMode;
        public SpawnerScriptable spawner;

        [Header("Detection")]
        public float detectionInterval = 0.5f;
        public bool lockedOn;
        public float detectionRange = 1000;
        public float distanceFromPlayer = 1000;
        public Action afterDetection;

        protected virtual void Start()
        {
            if (targetBoatController != null)
            {
                StartCoroutine(LookForPlayer());
            }
        }

        protected virtual IEnumerator LookForPlayer()
        {
            while (true)
            {
                yield return new WaitForSeconds(detectionInterval);
                var position = transform.position;
                var direction = targetBoatController.transform.position - position;
                distanceFromPlayer = direction.magnitude;
                
                var ray = new Ray(position, direction);
                if (VisualPhysics.Raycast(ray, out var hit, detectionRange))
                {
                    lockedOn = hit.collider.gameObject.name == targetBoatController.gameObject.name;
                    if (lockedOn)
                    {
                        afterDetection?.Invoke();
                    }
                }
            }
        }

        protected void FixedUpdate()
        {
            if (targetBoatController == null) return;
            if (lockedOn == false) return;
            Movement();
            RotateToTarget();
        }

        protected virtual void Movement()
        {
            Vector3 movementForce = transform.forward * (moveForce);
            rb.AddForce(movementForce, forceMode);
        }

        protected virtual void RotateToTarget()
        {
            var targetRotation = Quaternion.LookRotation(targetBoatController.transform.position - transform.position);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation,
                rotationTorque * Time.fixedDeltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == targetBoatController.gameObject)
            {
                gameObject.SetActive(false);
            }
        }

        protected virtual void OnDrawGizmos()
        {
            if (targetBoatController == null) return;
            if (lockedOn)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, targetBoatController.transform.position);
            }
        }

        public void OnDisable()
        {
            StopAllCoroutines();
            spawner.DeSpawn(gameObject);
        }
    }
}