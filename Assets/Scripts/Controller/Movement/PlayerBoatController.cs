using Controller.MonoBehaviours.SteeringWheel;
using UnityEngine;

namespace Controller.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerBoatController : BoatController
    {
        [Header("Controller")]
        public SteeringWheelController steeringWheelController;
        protected override void OnValidate()
        {
            base.OnValidate();
            rb = GetComponent<Rigidbody>();
        }
        protected void OnEnable()
        {
            steeringWheelController.onSteer.AddListener(ChangeRotation);
        }
        protected void OnDisable()
        {
            steeringWheelController.onSteer.RemoveListener(ChangeRotation);
        }
    }
}