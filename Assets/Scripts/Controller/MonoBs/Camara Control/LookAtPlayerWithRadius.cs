using TriInspector;
using UnityEngine;

namespace Controller.MonoBs.Camara_Control
{
    public class LookAtPlayerWithRadius : MonoBehaviour
    {
        public Transform player;

        [OnValueChanged(nameof(Update))][Range(0, 360)] public float xAngle;
        [OnValueChanged(nameof(Update))][Range(0, 360)] public float yAngle;
        [OnValueChanged(nameof(Update))][Range(0, 360)] public float radius;

        [Button]
        private void Update()
        {
            var position = player.position;
            Transform selfTransform = transform;
            
            selfTransform.rotation = Quaternion.Euler(xAngle, yAngle, 0);
            Vector3 newPosition = position + (selfTransform.forward * -radius);
            selfTransform.position = newPosition;
            transform.LookAt(position);
        }
    }
}