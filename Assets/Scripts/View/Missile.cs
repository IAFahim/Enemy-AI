using Controller;
using Model.Interaction;
using TriInspector;
using UnityEngine;

namespace View
{
    [RequireComponent(typeof(Rigidbody))]
    public class Missile : MissileController
    {
        public GameObject target;
        public StarSystemSo starSystem;


        private void FixedUpdate()
        {
            UpdatePosition(target);
        }

        private void OnTriggerEnter(Collider other)
        {
            starSystem.AddPoints(this, 1);
            if (other.transform.TryGetComponent<IExplode>(out var ex)) ex.Explode();
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            DrawGizmos();
        }

        [Button]
        public void AttachProperties()
        {
            rb = GetComponent<Rigidbody>();
        }
        
    }
}