using System;
using System.Collections;
using Controller.ScriptAbles.Spawner;
using Nomnom.RaycastVisualization;
using UnityEngine;

namespace Controller.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class AIBoatController : BoatController
    {
        public PlayerBoatController playerBoatController;
        public ScriptableSpawner scriptableSpawner;
        
        [Header("Detection")]
        public float detectionInterval = 0.5f;
        public bool lockedOn;
        public float detectionRange = 1000;
        public float distanceFromPlayer = 1000;
        public Action afterDetection;

        protected virtual void Start()
        {
            if (playerBoatController != null)
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
                var direction = playerBoatController.transform.position - position;
                distanceFromPlayer = direction.magnitude;
                
                var ray = new Ray(position, direction);
                if (VisualPhysics.Raycast(ray, out var hit, detectionRange))
                {
                    lockedOn = hit.collider.gameObject.name == playerBoatController.gameObject.name;
                    if (lockedOn)
                    {
                        afterDetection?.Invoke();
                    }
                }
            }
        }

        protected override void FixedUpdate()
        {
            if (playerBoatController == null) return;
            if (lockedOn == false) return;
            Movement();
            RotateToTarget();
        }

        protected virtual void RotateToTarget()
        {
            var targetRotation = Quaternion.LookRotation(playerBoatController.transform.position - transform.position);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation,
                rotationTorque * Time.fixedDeltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == playerBoatController.gameObject)
            {
                gameObject.SetActive(false);
            }
        }

        protected virtual void OnDrawGizmos()
        {
            if (playerBoatController == null) return;
            if (lockedOn)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, playerBoatController.transform.position);
            }
        }

        public void OnDisable()
        {
            StopAllCoroutines();
            scriptableSpawner.DeSpawn(gameObject);
        }
    }
}