using UnityEngine;
using UnityEngine.Serialization;

namespace Controller.Movement
{
    public class HeliRotorController : MonoBehaviour
    {
        public enum Axis
        {
            X,
            Y,
            Z,
        }

        [FormerlySerializedAs("InverseRotate")] public bool inverseRotate = false;
        [FormerlySerializedAs("RotateAxis")] public Axis rotateAxis;
        public float RotorSpeed { get; set; }

        private Quaternion _originalRotation;

        private void Start()
        {
            _originalRotation = transform.localRotation;
        }

        private void Update()
        {
            float rotationSpeed = inverseRotate ? -RotorSpeed : RotorSpeed;
            float rotationAngle = rotationSpeed * Time.deltaTime;
            Quaternion rotationDelta = Quaternion.Euler(
                rotateAxis == Axis.X ? rotationAngle : 0f,
                rotateAxis == Axis.Y ? rotationAngle : 0f,
                rotateAxis == Axis.Z ? rotationAngle : 0f
            );
        
            transform.localRotation *= rotationDelta;
        }
    }
}