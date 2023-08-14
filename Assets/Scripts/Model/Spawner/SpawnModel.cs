using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Model.Spawner
{
    public class SpawnModel : ScriptableObject
    {
        protected ObjectPool<GameObject> Pool;
        public GameObject target;
        [SerializeField] protected GameObject prefab;
        public List<Vector3> spawnPositionList;
    }
}