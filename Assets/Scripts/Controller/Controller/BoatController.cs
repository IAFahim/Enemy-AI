using Controller.MonoBehaviours.SteeringWheel;
using UnityEngine;

namespace Controller.Controller
{
    [RequireComponent(typeof(Rigidbody))]
    public class BoatController : MonoBehaviour
    {
        public Rigidbody rb;
        public float moveForce = 10f; // Adjust the force for movement
        public float rotationTorque = 5f; // Adjust the torque for rotation
        public SteeringWheelController steeringWheelController;
        public ForceMode forceMode;

        private void OnEnable()
        {
            steeringWheelController.onSteer.AddListener(Rotate);
        }
        
        private void OnDisable()
        {
            steeringWheelController.onSteer.RemoveListener(Rotate);
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Vector3 movementForce = transform.forward * (moveForce);
            rb.AddForce(movementForce, forceMode);
        }

        private void Rotate(float normalized)
        {
            var rotation = Quaternion.Euler(0, normalized * rotationTorque * Time.fixedDeltaTime, 0);
            rb.MoveRotation(rb.rotation * rotation);
        }
    }
}