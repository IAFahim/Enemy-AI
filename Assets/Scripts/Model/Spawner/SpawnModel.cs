using TriInspector;
using UnityEngine;
using UnityEngine.Pool;

namespace Model.Spawner
{
    public class SpawnModel : ScriptableObject
    {
        protected ObjectPool<GameObject> Pool;
        [SerializeField] protected GameObject prefab;
    }
}