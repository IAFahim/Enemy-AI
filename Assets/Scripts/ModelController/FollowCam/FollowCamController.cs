using UnityEngine;

namespace ModelController.FollowCam
{
    public class FollowCamController : MonoBehaviour
    {
        public GameObject target;
        public Vector3 starting;

        void Start()
        {
            starting = transform.position;
        }

        private void Update()
        {
            Vector3 targetPosition = target.transform.position + starting; 
            transform.position = targetPosition;
        }
    }
}