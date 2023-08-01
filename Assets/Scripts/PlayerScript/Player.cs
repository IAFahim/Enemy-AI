using Model.Health;
using UnityEngine;

namespace PlayerScript
{
    public class Player : MonoBehaviour
    {
        public Health health;

        private void Start()
        {
            health.onDeath.AddListener(OnDeath);
        }

        private void OnDeath(Health arg0)
        {
            Destroy(gameObject);
        }
    }
}