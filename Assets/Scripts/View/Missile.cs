using Controller.Movement;
using Model.Interaction;
using UnityEngine;

namespace View
{
    [RequireComponent(typeof(Rigidbody))]
    public class Missile : MissileController
    {
        public GameObject target;

        public void OnValidate()
        {
            rb ??= GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            UpdatePosition(target);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.TryGetComponent<IExplode>(out var ex)) ex.Explode();
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            DrawGizmos();
        }
    }
}