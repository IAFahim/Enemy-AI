using Model.IDs;
using TriInspector;
using UnityEngine;

namespace Controller.Star
{
    [HideMonoScript, RequireComponent(typeof(Collider), typeof(ID))]
    public class StarRegisterOnTouch : MonoBehaviour
    {
        public ID id;
        public StarManager starManager;
        public int points = 1;

        private void OnValidate()
        {
            id ??= GetComponent<ID>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player")) 
                starManager.AddPoints(id, points);
        }
    }
}