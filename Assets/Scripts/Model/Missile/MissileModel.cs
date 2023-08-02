using System;
using Model.Identifier;
using TriInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Model.Missile
{
    [Serializable]
    [DeclareHorizontalGroup("Key")]
    public class MissileModel : MonoBehaviour, IKey
    {
        [DisableInEditMode, Group("Key")] public string key;
        public string Key => key;

        [Header("REFERENCES")] [SerializeField]
        protected Rigidbody rb;

        public Rigidbody TargetRigidbody;

        [SerializeField] protected GameObject explosionPrefab;

        [Header("MOVEMENT")] [SerializeField] protected float speed = 15;
        [SerializeField] protected float rotateSpeed = 95;

        [Header("PREDICTION")] [SerializeField]
        protected float maxDistancePredict = 100;

        [SerializeField] protected float minDistancePredict = 5;
        [SerializeField] protected float maxTimePrediction = 5;
        protected Vector3 StandardPrediction, DeviatedPrediction;

        [Header("DEVIATION")] [SerializeField] protected float deviationAmount = 50;
        [SerializeField] protected float deviationSpeed = 2;
        
        [Group("Key"), Button]
        public void SetAsKey()
        {
            key = gameObject.name;
        }
    }
}