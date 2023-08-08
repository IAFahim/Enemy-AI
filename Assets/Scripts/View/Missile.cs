﻿using Controller.MonoBehaviours;
using Model.Interaction;
using TriInspector;
using UnityEngine;
using View.ScriptableObjects;

namespace View
{
    [RequireComponent(typeof(Rigidbody))]
    public class Missile : MissileController
    {
        public GameObject target;
        public StarSystemScriptableObject starSystem;


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