using System;
using System.Collections;
using Controller.GizmosUtils;
using Controller.Movement;
using Controller.ScriptAbles.Spawner;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controller.Petrol
{
    public sealed class PetrolSubmarine : MonoBehaviour
    {
        [FormerlySerializedAs("scriptableSpawner")] public ScriptablePool scriptablePool;
        public PlayerBoatController playerBoatController;
        public Transform submarine;

        public float depth = -10;
        public float diveDepth = -10;

        [Header("Detection")] public float detectionInterval = 0.5f;
        public bool lockedOn;
        public float detectionRange = 1000;
        public float distanceFromPlayer = 1000;
        public Action afterDetection;
        public LayerMask detectionLayerMask;

        public GameObject torpedo;

        private void OnValidate()
        {
            depth = diveDepth = transform.position.y;
            submarine ??= transform.Find("Submarine").transform;
            torpedo ??= transform.Find("Torpedo").gameObject;
        }

        private void Start()
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

        private IEnumerator LookForPlayer()
        {
            while (true)
            {
                yield return new WaitForSeconds(detectionInterval);
                var position = transform.position;
                var direction = playerBoatController.transform.position - position;
                distanceFromPlayer = direction.magnitude;

                Ray ray = new Ray(position, direction);
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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            GizmosExtensions.DrawWireCircle(submarine.position, detectionRange);
        }

        private void OnDisable()
        {
            scriptablePool.Release(gameObject);
            StopAllCoroutines();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}