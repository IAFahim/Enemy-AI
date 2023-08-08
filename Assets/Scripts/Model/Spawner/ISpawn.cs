using UnityEngine;
using UnityEngine.Pool;

namespace Model.Spawner
{
    public interface ISpawn
    {
        GameObject Prefab { get; set; }
        void Spawn(Vector3 position, Vector3 rotation, int amount);
        
        void Destroy(GameObject gameObject);
    }
}