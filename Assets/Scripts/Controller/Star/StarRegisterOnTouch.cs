using Controller.Interest;
using Model.Identifier;
using UnityEngine;

namespace Controller.Star
{
    [RequireComponent(typeof(Collider), typeof(Identifier))]
    public class StarRegisterOnTouch : MonoBehaviour
    {
        public Identifier identifier;
        public StarSystem starSystem;
        public float points = 1;
        public float star;

        private void OnValidate()
        {
            identifier ??= GetComponent<Identifier>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) starSystem.AddPoints(identifier, (int)points);
        }
    }
}