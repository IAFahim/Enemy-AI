using Controller.Interest;
using Model.Identifier;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controller.Star
{
    [RequireComponent(typeof(Collider), typeof(ID))]
    public class StarRegisterOnTouch : MonoBehaviour
    {
        [FormerlySerializedAs("identifier")] public ID id;
        public StarSystem starSystem;
        public float points = 1;
        public float star;

        private void OnValidate()
        {
            id ??= GetComponent<ID>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) starSystem.AddPoints(id, (int)points);
        }
    }
}