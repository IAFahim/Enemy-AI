using UnityEngine;
using View.ScriptableObjects;

namespace View.MonoBehaviors
{
    public class StarRegister : MonoBehaviour
    {
        public StarSystemScriptableObject starSystemScriptableObject;
        public float points=1;
        public float star;
        
        private void OnTriggerEnter(Collider other)
        {
            starSystemScriptableObject.Point++;
        }
    }
}
