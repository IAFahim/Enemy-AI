using System;
using UnityEngine;

namespace ModelController.Float
{
    [Serializable]
    [RequireComponent(typeof(Rigidbody))]
    public class Floater : MonoBehaviour
    {
        public Rigidbody rb;
        public float force = 10;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void FixedUpdate()
        {
            Float();
        }

        public void Float()
        {
            if (gameObject.transform.position.y < 0)
            {
                rb.AddForce(Vector3.up * force);
            }
        }
    }
}