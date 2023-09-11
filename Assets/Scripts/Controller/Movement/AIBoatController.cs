using System;
using System.Collections;
using Controller.ScriptAbles.Spawner;
using Nomnom.RaycastVisualization;
using Pancake.Apex;
using UnityEngine;

namespace Controller.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class AIBoatController : BoatController
    {
        [Header("Target")] public PlayerBoatController playerBoatController;
        private LayerMask _detectionLayerMask;

        [Header("Detection")] public float detectionInterval = 0.5f;
        public float detectionRange = 1000;
        [DisableInEditorMode, DisableInPlayMode] public float distanceFromPlayer = 1000;
        [DisableInEditorMode, DisableInPlayMode] public bool lockedOn;

        protected Action AfterDetection;
        public ScriptableSpawner scriptableSpawner;

        protected override void OnValidate()
        {
            base.OnValidate();
            if (playerBoatController == null) playerBoatController = FindObjectOfType<PlayerBoatController>();
        }

        protected virtual void Start()
        {
            _detectionLayerMask = 1 << playerBoatController.gameObject.layer;
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
                if (VisualPhysics.Raycast(ray, out var hit, detectionRange, _detectionLayerMask))
                {
                    lockedOn = hit.collider.gameObject.name == playerBoatController.gameObject.name;

                    if (lockedOn)
                    {
                        AfterDetection?.Invoke();
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
            var quaternion = Quaternion.RotateTowards(transform.rotation, targetRotation,
                rotationTorque * Time.fixedDeltaTime);
            Rotate(quaternion);
        }

        public void OnTriggerEnter(Collider other)
        {
            gameObject.SetActive(false);
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