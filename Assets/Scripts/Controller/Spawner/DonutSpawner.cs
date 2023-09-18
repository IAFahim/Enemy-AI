using Controller.GizmosUtils;
using TriInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Controller.Spawner
{
    public class DonutSpawner : MonoBehaviour
    {
        public GameObject prefab;
        public float minorRadius = 5;
        public float majorRadius = 10;
        private Vector3 _lastSpawnedPosition;

        private void OnEnable()
        {
            GetPointInside2dDonut();
        }
        
        [Button]
        public Vector3 GetPointInside2dDonut()
        {
            float distanceInside = Random.Range(minorRadius, majorRadius);
            float angle = Random.Range(0, 2 * Mathf.PI);
            return _lastSpawnedPosition =
                new Vector3(Mathf.Cos(angle) * distanceInside, 0, Mathf.Sin(angle) * distanceInside) +
                transform.position;
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(_lastSpawnedPosition, 0.5f);

            var position = transform.position;
            Gizmos.color = Color.green;
            GizmosExtensions.DrawWireCircle(position, minorRadius);
            Gizmos.color = Color.red;
            GizmosExtensions.DrawWireCircle(position, majorRadius);
        }
    }
}