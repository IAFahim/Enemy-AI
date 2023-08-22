using UnityEngine;

namespace Controller.Controller
{
    [RequireComponent(typeof(Rigidbody))]
    public class TailingAIBoatController : AIBoatController
    {
        [Header("Tailing")] public bool tailing;
        public float tailingRadius = 40f;
        public float gainedSpeed;
        public float tailingDuration = 15f;
        public float maxSpeedGain = 10f;
        [SerializeField]
        private AnimationCurve speedGainCurve = AnimationCurve.Linear(0, 0, 1, 1);
 
        protected override void Start()
        {
            base.Start();
            afterDetection += () => { tailing = distanceFromPlayer < tailingRadius; };
        }

        protected new void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            if (tailing)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawLine(transform.position, targetBoatController.transform.position);
            }

            Gizmos.DrawWireSphere(transform.position, tailingRadius);
        }

        protected override void Movement()
        {
            float speed;
            if (tailing)
            {
                float normalizedTime = Mathf.Clamp01(gainedSpeed / tailingDuration); // Ensure normalized time is between 0 and 1
                float curveValue = speedGainCurve.Evaluate(normalizedTime);
                speed = targetBoatController.moveForce + curveValue * maxSpeedGain;

                gainedSpeed += Time.deltaTime;
            }
            else
            {
                speed = moveForce;
                gainedSpeed = 0;
            }

            Vector3 movementForce = transform.forward * speed;
            rb.AddForce(movementForce, forceMode);
        }
    }
}