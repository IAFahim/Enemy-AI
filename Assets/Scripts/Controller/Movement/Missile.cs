using Model.Interaction;
using TriInspector;
using UnityEngine;

namespace Controller.Movement
{
    [RequireComponent(typeof(Rigidbody)), HideMonoScript]
    public class Missile : MissileController
    {
        public GameObject target;

        public void OnValidate()
        {
            rb ??= GetComponent<Rigidbody>();
            if (target != null) TargetRigidbody ??= target.GetComponent<Rigidbody>();
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