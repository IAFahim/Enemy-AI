using UnityEngine;

namespace Controller.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class TailingAIBoatController : AIBoatController
    {
        [Header("Tailing")] public bool tailing;
        public float tailingRadius = 40f;
        

        protected override void Start()
        {
            afterDetection += () =>
            {
                tailing = distanceFromPlayer < tailingRadius;
            };
            base.Start();
        }

        protected override void FixedUpdate()
        {
            if (tailing)
            {
                Movement(playerBoatController.moveForce);
            }
            else
            {
                base.FixedUpdate();
            }
        }

        protected new void OnDrawGizmosSelected()
        {
            base.OnDrawGizmos();

            if (tailing)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawLine(transform.position, playerBoatController.transform.position);
            }

            Gizmos.DrawWireSphere(transform.position, tailingRadius);
        }
    }
}