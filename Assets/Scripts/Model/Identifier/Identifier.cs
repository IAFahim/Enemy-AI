using TriInspector;
using UnityEngine;

namespace Model.Identifier
{
    public class Identifier : MonoBehaviour, IKey
    {
        [DisableInEditMode] public string key;
        public string Key => key;
        
        [Button]
        public void SetAsKey()
        {
            key = gameObject.name;
        }
    }
}