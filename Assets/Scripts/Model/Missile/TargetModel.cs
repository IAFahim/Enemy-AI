using System;
using Model.Interaction;
using UnityEngine;
using UnityEngine.Serialization;

namespace Model.Missile
{
    [Serializable]
    public class TargetModel : ITargetAble
    {
        public GameObject gameObject;
        [SerializeField] public Rigidbody rb;
        [SerializeField] public float size = 10;
        [SerializeField] public float speed = 10;
    }
}