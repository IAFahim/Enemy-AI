using Controller.ScriptableObjects.Spawner;
using TriInspector;
using UnityEngine;

namespace Controller.MonoBehaviours.Spawner
{
    public class Spawner : MonoBehaviour
    {
        public GameObject target;
        public SpawnControllerScriptableObject spawnControllerScriptableObject;
        public SphereCollider sphereCollider;
        public float noSpawnRadius = 10;

        [Button]
        public void Check(int amount = 5)
        {
            spawnControllerScriptableObject.GeneratePosition(target.transform.position, noSpawnRadius,
                sphereCollider.radius/2,
                amount);
        }

        [Button]
        public void Spawn(int amount = 5)
        {
            spawnControllerScriptableObject.SpawnArray(target.transform.position, target.transform.rotation.eulerAngles,
                noSpawnRadius,
                sphereCollider.radius / 2, amount, () => { });
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(target.transform.position, noSpawnRadius);

            Gizmos.color = Color.green;
            spawnControllerScriptableObject.spawnPositionList.ForEach((Vector3 position) =>
                Gizmos.DrawSphere(position, 1));
        }
    }
}