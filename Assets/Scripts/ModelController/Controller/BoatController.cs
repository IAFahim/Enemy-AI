using UnityEngine;

namespace ModelController.Controller
{
    [RequireComponent(typeof(Rigidbody))]
    public class BoatController : MonoBehaviour
    {
        public Rigidbody rb;
        public float moveForce = 10f; // Adjust the force for movement
        public float decelerationForce = 5f; // Adjust the force for deceleration
        public float maxSpeed = 10f; // Maximum speed of the boat
        public float rotationTorque = 5f; // Adjust the torque for rotation
        public Camera cam;
        public float startingCamHeight = 10f;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            startingCamHeight = cam.transform.position.y;
        }

        private void FixedUpdate()
        {
            float verticalInput = Input.GetAxis("Vertical");
            float horizontalInput = Input.GetAxis("Horizontal");

            Vector3 movementForce = transform.forward * (verticalInput * moveForce);

            // Apply movement force
            if (Mathf.Abs(rb.velocity.magnitude) < maxSpeed)
            {
                rb.AddForce(movementForce);
            }

            // Deceleration when not moving forward or backward
            if (Mathf.Approximately(verticalInput, 0f))
            {
                Vector3 deceleration = -rb.velocity.normalized * decelerationForce;
                rb.AddForce(deceleration);
            }

            // Boat Rotation
            var rotation = Quaternion.Euler(0, horizontalInput * rotationTorque * Time.fixedDeltaTime, 0);
            rb.MoveRotation(rb.rotation * rotation);
        }
    }
}