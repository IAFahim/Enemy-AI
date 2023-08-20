using UnityEngine;

namespace ModelController.Look_At
{
    public class FaceTowardCam : MonoBehaviour
    {
        public Camera cam;

        void Start()
        {
            cam = Camera.main;
        }

        void Update()
        {
            // look at only the y axis
            var position = cam.transform.position;
            transform.LookAt(new Vector3(position.x, transform.position.y, position.z));
        }
    }
}