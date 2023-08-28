using System;
using System.Collections;
using Controller.Movement;
using Controller.ScriptAbles.Spawner;
using UnityEngine;

namespace Controller.Petrol
{
    public class PetrolSubmarine : MonoBehaviour
    {
        public PlayerBoatController playerBoatController;
        public ScriptableSpawner scriptableSpawner;
        public float depth = -10;
        public float diveDepth = -10;

        [Header("Detection")] public float detectionInterval = 0.5f;
        public bool lockedOn;
        public float detectionRange = 1000;
        public float distanceFromPlayer = 1000;
        public Action afterDetection;
        public LayerMask detectionLayerMask;

        public Transform splineTransform;
        public GameObject torpedo;

        protected void OnValidate()
        {
            depth = diveDepth = transform.position.y;
            if (splineTransform == null)
            {
                splineTransform = transform.Find("Spline");
            }
        }

        protected virtual void Start()
        {
            afterDetection += () =>
            {
                // surface the submarine learping from base postion to Vector3.zero and then start shooting
                depth = Mathf.Lerp(diveDepth, 0, distanceFromPlayer / detectionRange);
                var currentTransform = transform;
                var position = currentTransform.position;
                position = new Vector3(position.x, depth, position.z);
                currentTransform.position = position;
            };
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
                if (Physics.Raycast(ray, out var hit, detectionRange, detectionLayerMask))
                {
                    lockedOn = hit.collider.gameObject.name == playerBoatController.gameObject.name;
                    if (lockedOn)
                    {
                        afterDetection?.Invoke();
                    }
                }
            }
        }

        private void OnDisable()
        {
            scriptableSpawner.DeSpawn(gameObject);
            StopAllCoroutines();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}