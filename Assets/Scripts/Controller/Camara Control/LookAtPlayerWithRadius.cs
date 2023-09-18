using TriInspector;
using UnityEngine;

namespace Controller.Camara_Control
{
    public class LookAtPlayerWithRadius : MonoBehaviour
    {
        public Transform player;

        [OnValueChanged(nameof(Update))] public float xAngle;

        [OnValueChanged(nameof(Update))] public float yAngle;

        [OnValueChanged(nameof(Update))] public float radius;
        

        private void Update()
        {
            var selfTransform = transform;
            selfTransform.rotation = Quaternion.Euler(xAngle, yAngle, 0);
            var playerPosition = player.transform.position;
            Vector3 newPosition = playerPosition + (selfTransform.forward * -radius);
            selfTransform.position = newPosition;
            selfTransform.LookAt(playerPosition);
        }
    }
}