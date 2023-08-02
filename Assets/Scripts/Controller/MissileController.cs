using System;
using Model.Missile;
using UnityEngine;

namespace Controller
{
    [Serializable]
    public class MissileController : MissileModel
    {
        public void UpdatePosition(GameObject target)
        {
            if (target == null) return;
            if (target.activeSelf == false) return;
            if (TargetRigidbody == null) TargetRigidbody = target.GetComponent<Rigidbody>();
            var o = gameObject;
            rb.velocity = o.transform.forward * speed; // Just adds a force in the forward direction

            var leadTimePercentage = Mathf.InverseLerp(minDistancePredict, maxDistancePredict,
                Vector3.Distance(o.transform.position,
                    target.gameObject.transform
                        .position)); // Calculates the percentage of the distance between the missile and the target

            PredictMovement(leadTimePercentage);

            AddDeviation(leadTimePercentage);

            RotateRocket();
        }

        private void PredictMovement(float leadTimePercentage)
        {
            var predictionTime =
                Mathf.Lerp(0, maxTimePrediction, leadTimePercentage); // Reduces the prediction Distance by time

            StandardPrediction = TargetRigidbody.position + TargetRigidbody.velocity * predictionTime;
        }

        private void AddDeviation(float leadTimePercentage)
        {
            float x = Time.time * deviationSpeed;
            var deviation = new Vector3(Mathf.Sin(x) / x, 0, 0);

            var predictionOffset = gameObject.transform.TransformDirection(deviation) *
                                   (deviationAmount * leadTimePercentage);

            DeviatedPrediction = StandardPrediction + predictionOffset;
        }

        private void RotateRocket()
        {
            var heading = DeviatedPrediction - gameObject.transform.position;

            var rotation = Quaternion.LookRotation(heading);
            rb.MoveRotation(Quaternion.RotateTowards(gameObject.transform.rotation, rotation,
                rotateSpeed * Time.deltaTime));
        }

        public void DrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(DeviatedPrediction, 1);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(StandardPrediction, 1);
        }
    }
}