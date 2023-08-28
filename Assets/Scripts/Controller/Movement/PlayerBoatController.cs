using Controller.MonoBehaviours.SteeringWheel;
using UnityEngine;

namespace Controller.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerBoatController : BoatController
    {
        [Header("Controller")]
        public SteeringWheelController steeringWheelController;
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