using System;
using UnityEngine;

namespace Controller.Movement
{
    public abstract class BoatController : MonoBehaviour
    {
        public Rigidbody rb;
        public float moveForce = 10f;

        #region Rotation
        
        public float rotationTorque = 5f;
        [Range(-1, 1)] public float rotationNormalized;

        protected Quaternion rotationQuaternion =>
            Quaternion.Euler(0, rotationNormalized * rotationTorque * Time.fixedDeltaTime, 0);

        protected bool _rotationChanged;

        #endregion


        public ForceMode forceMode;

        protected virtual void OnValidate()
        {
            rb = GetComponent<Rigidbody>();
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