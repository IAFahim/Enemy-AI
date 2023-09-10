using Pancake.Apex;
using UnityEngine;

namespace Controller.Movement
{
    public abstract class BoatController : MonoBehaviour
    {
        public Rigidbody rb;
        public float moveForce = 500f;

        #region Rotation

        [OnValueChanged(nameof(Rotate))] public float rotationTorque = 30f;
        [Range(-1, 1)] public float rotationNormalized;

        public Quaternion rotationQuaternion =>
            Quaternion.Euler(0, rotationNormalized * rotationTorque * Time.fixedDeltaTime, 0);

        private bool _rotationChanged;

        #endregion


        public ForceMode forceMode;

        protected virtual void OnValidate()
        {
            if (rb == null) rb = GetComponent<Rigidbody>();
        }

        protected virtual void FixedUpdate()
        {
            if (_rotationChanged) Rotate();
            Movement();
        }

        public virtual void Movement() => rb.AddForce(transform.forward * moveForce, forceMode);

        public virtual void Movement(float value) => rb.AddForce(transform.forward * value, forceMode);

        public void ChangeRotation(float normalized)
        {
            _rotationChanged = true;
            rotationNormalized = normalized;
        }

        public virtual void Rotate() => rb.MoveRotation(rb.rotation * rotationQuaternion);

        public virtual void Rotate(Quaternion quaternion)
        {
            rotationNormalized = quaternion.eulerAngles.y;
            rb.MoveRotation(quaternion);
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}