using System;
using TriInspector;
using UnityEngine;

namespace Controller.Movement
{
    [HideMonoScript]
    public abstract class BoatController : MonoBehaviour, ISpeedModifier
    {
        [Header("Physics"), GroupNext("Physics")] public Rigidbody rb;
        public ForceMode forceMode;

        [Header("Speed"), GroupNext("Speed")] public float moveForce = 500f;

        [field: SerializeReference, Range(-1, 5)]
        public float speedModifier = 1f;


        #region Rotation

        [Header("Rotation"), GroupNext("Rotation")]
        [OnValueChanged(nameof(Rotate))]
        public float rotationTorque = 30f;

        private bool _rotationChanged;
        [Range(-1, 1)] public float rotationNormalized;

        #endregion
        
        public void SetSpeedModifier(float value)
        {
            speedModifier = value;
        }

        public virtual void BalanceSpeedOverTime()
        {
            if (Math.Abs(speedModifier - 1) > 0.001)
            {
                speedModifier = Mathf.MoveTowards(speedModifier, 1, Time.deltaTime);
            }
        }

        public void Update()
        {
            BalanceSpeedOverTime();
        }

        protected virtual void OnValidate()
        {
            if (rb == null) rb = GetComponent<Rigidbody>();
            _rotationChanged = true;
        }

        protected virtual void FixedUpdate()
        {
            if (_rotationChanged) Rotate();
            Movement();
        }

        public virtual void Movement() => rb.AddForce(transform.forward * moveForce * speedModifier, forceMode);

        public virtual void Movement(float value) => rb.AddForce(transform.forward * value * speedModifier, forceMode);

        public void ChangeRotation(float normalized)
        {
            _rotationChanged = true;
            rotationNormalized = normalized;
        }

        public virtual void Rotate() => rb.MoveRotation(rb.rotation *
                                                        Quaternion.Euler(0,
                                                            rotationNormalized * rotationTorque * Time.fixedDeltaTime,
                                                            0));

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