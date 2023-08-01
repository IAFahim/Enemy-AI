using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Model.Missile
{
    [Serializable]
    public class MissileModel
    {
        public GameObject gameObject;

        [Header("REFERENCES")] [SerializeField]
        private Rigidbody rb;

        [SerializeField] private GameObject explosionPrefab;

        [Header("MOVEMENT")] [SerializeField] private float speed = 15;
        [SerializeField] private float rotateSpeed = 95;

        [Header("PREDICTION")] [SerializeField]
        private float maxDistancePredict = 100;

        [SerializeField] private float minDistancePredict = 5;
        [SerializeField] private float maxTimePrediction = 5;
        private Vector3 _standardPrediction, _deviatedPrediction;

        [Header("DEVIATION")] [SerializeField] private float deviationAmount = 50;
        [SerializeField] private float deviationSpeed = 2;

        public void UpdatePosition(TargetModel target)
        {
            rb.velocity = gameObject.transform.forward * speed; // Just adds a force in the forward direction

            var leadTimePercentage = Mathf.InverseLerp(minDistancePredict, maxDistancePredict,
                Vector3.Distance(gameObject.transform.position,
                    target.gameObject.transform
                        .position)); // Calculates the percentage of the distance between the missile and the target

            PredictMovement(target, leadTimePercentage);

            AddDeviation(leadTimePercentage);

            RotateRocket();
        }

        private void PredictMovement(TargetModel target, float leadTimePercentage)
        {
            var predictionTime =
                Mathf.Lerp(0, maxTimePrediction, leadTimePercentage); // Reduces the prediction Distance by time

            _standardPrediction = target.rb.position + target.rb.velocity * predictionTime;
        }

        private void AddDeviation(float leadTimePercentage)
        {
            float x = Time.time * deviationSpeed;
            var deviation = new Vector3(Mathf.Sin(x) / x, 0, 0);

            var predictionOffset = gameObject.transform.TransformDirection(deviation) *
                                   (deviationAmount * leadTimePercentage);

            _deviatedPrediction = _standardPrediction + predictionOffset;
        }

        private void RotateRocket()
        {
            var heading = _deviatedPrediction - gameObject.transform.position;

            var rotation = Quaternion.LookRotation(heading);
            rb.MoveRotation(Quaternion.RotateTowards(gameObject.transform.rotation, rotation,
                rotateSpeed * Time.deltaTime));
        }

        public void DrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_deviatedPrediction, 1);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_standardPrediction, 1);
        }
    }
}