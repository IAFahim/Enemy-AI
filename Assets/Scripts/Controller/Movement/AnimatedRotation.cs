using TriInspector;
using UnityEngine;

namespace Controller.Movement
{
    public class AnimatedRotation : MonoBehaviour
    {
        public float duration;

        public bool lockX;
        public bool lockY;
        public bool lockZ;

        [Header("Runtime Parameters")] [SerializeField]
        private Quaternion fromDirection;

        [SerializeField] private Quaternion targetDirection;
        [SerializeField] private float timeRemaining;
        [SerializeField] private Vector3 startingEulerAngles;

        public AnimatedRotation(bool lockX)
        {
            this.lockX = lockX;
        }

        void Start()
        {
            startingEulerAngles = this.transform.localEulerAngles;
        }

        void Update()
        {
            if (timeRemaining <= 0f)
            {
                return;
            }

            timeRemaining -= Time.deltaTime;

            var newRotation  = Quaternion.Lerp(
                fromDirection, 
                targetDirection, 
                (duration - timeRemaining) / duration
            );

            
            Vector3 eulerAngles = newRotation.eulerAngles;
            
            if (lockX)
            {
                eulerAngles.x = startingEulerAngles.x;
            }

            if (lockY)
            {
                eulerAngles.y = startingEulerAngles.y;
            }

            if (lockZ)
            {
                eulerAngles.z = startingEulerAngles.z;
            }

            transform.localEulerAngles = eulerAngles;
        }
        
        [Button]
        public void SetDirection(Vector3 direction)
        {
            timeRemaining = duration;
            fromDirection = transform.rotation;
            targetDirection = Quaternion.FromToRotation(
                Vector3.forward,
                direction
            );
        }
    }
}