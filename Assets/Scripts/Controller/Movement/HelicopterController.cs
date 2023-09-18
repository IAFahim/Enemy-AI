using Stateless;
using TriInspector;
using UnityEngine;

namespace Controller.Movement
{
    public class HelicopterController : MonoBehaviour
    {
        public HeliRotorController mainRotorController;
        public HeliRotorController subRotorController;
        public AudioSource helicopterSound;

        public LayerMask groundMaskLayer;
        public float turnForce = 3f;
        public float forwardForce = 10f;
        public float forwardTiltForce = 20f;
        public float turnTiltForce = 30f;
        public float effectiveHeight = 100f;

        public float turnTiltForcePercent = 1.5f;
        public float turnForcePercent = 1.3f;

        private float engineForce;

        public float EngineForce
        {
            get => engineForce;
            set
            {
                mainRotorController.RotorSpeed = value * 80;
                subRotorController.RotorSpeed = value * 40;
                helicopterSound.pitch = Mathf.Clamp(value / 40, 0, 1.2f);
                engineForce = value;
            }
        }

        private float distence;

        public enum HeliState
        {
            OnPetrol,
            Detected,
            Cheesing,
            Attacking,
        }

        public enum HeliTrigger
        {
            OnPetrol,
            Detected,
            Cheesing,
            Attacking,
        }

        public HeliState currentState;
        [OnValueChanged(nameof(SetState))]public HeliTrigger currentTrigger;
        private StateMachine<HeliState, HeliTrigger> Sm;


        private void Start()
        {
            
        }

        public void SetState()
        {
            Sm.Fire(currentTrigger);
        }
        
        

        private void FixedUpdate()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out var hit, 1000, groundMaskLayer))
            {
                distence = hit.distance;
            }

            if (distence < effectiveHeight)
            {
                var forcePercent = 1 - (distence / effectiveHeight);
                var force = forcePercent * EngineForce;
                var rigidbody = GetComponent<Rigidbody>();
                rigidbody.AddForceAtPosition(transform.up * force, transform.position);
            }
        }
    }
}