using System;
using Controller.SteeringWheel;
using UnityEngine;

namespace ModelController.Controller
{
    [RequireComponent(typeof(Rigidbody))]
    public class AIBoatController : MonoBehaviour
    {
        public GameObject target;
        public Rigidbody rb;
        public float moveForce = 10f; // Adjust the force for movement
        public float rotationTorque = 5f; // Adjust the torque for rotation
        public ForceMode forceMode;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Vector3 movementForce = transform.forward * (moveForce);
            rb.AddForce(movementForce, forceMode);
            RotateToTarget();
        }

        private void RotateToTarget()
        {
            var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationTorque * Time.fixedDeltaTime));
        }
    }
}