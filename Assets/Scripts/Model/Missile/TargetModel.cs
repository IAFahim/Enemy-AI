using System;
using Model.Interaction;
using TriInspector;
using UnityEngine;

namespace Model.Missile
{
    [Serializable]
    public class TargetModel : MonoBehaviour, ITargetAble
    {
        [SerializeField] public Rigidbody rb;
        [SerializeField] public float size = 10;
        [SerializeField] public float speed = 10;
        
        [Button]
        public void AttachProperties()
        {
            rb = GetComponent<Rigidbody>();
        }
        
    }
}