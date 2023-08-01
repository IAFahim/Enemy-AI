using Model.Identifier;
using Model.Interaction;
using Model.Missile;
using Star;
using TriInspector;
using UnityEngine;

namespace Homing_Missile
{
    public class Missile : MonoBehaviour, IKey
    {
        [DisableInEditMode]
        [field: SerializeField]
        public string Key { get; set; }

        public MissileModel missileModel;
        public StarSystemSo starSystem;
        public Target target;

        private void FixedUpdate()
        {
            missileModel.UpdatePosition(target.targetModel);
        }

        private void OnTriggerEnter(Collider other)
        {
            starSystem.starModel.AddPoints(this, 1);
            if (other.transform.TryGetComponent<IExplode>(out var ex)) ex.Explode();
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            missileModel.DrawGizmos();
        }

        public void SetAskKey()
        {
        }
    }
}