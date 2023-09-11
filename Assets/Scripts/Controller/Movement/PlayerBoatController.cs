using Controller.SteeringWheel;
using UnityEngine;

namespace Controller.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerBoatController : BoatController
    {
        [Header("Controller")] public SteeringWheelController steeringWheelController;

        protected void OnEnable()
        {
            if (steeringWheelController != null)
                steeringWheelController.onSteer.AddListener(ChangeRotation);
        }

        protected void OnDisable()
        {
            if (steeringWheelController != null)
                steeringWheelController.onSteer.RemoveListener(ChangeRotation);
        }
    }
}