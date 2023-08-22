using Controller.ScriptAbles.Spawner;
using TriInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controller.MonoBs.Spawner
{
    public class DonutSpawner : MonoBehaviour
    {
        public GameObject target;
        public SpawnerScriptable spawnerScriptable;
        public SphereCollider sphereCollider;
        public float noSpawnRadius = 10;

        Vector3 GeneratePointInDonut(Vector3 position, float holeRadius, float radius, bool zeroY)
        {
            Vector2 randomPoint2D;
            do
            {
                randomPoint2D = Random.insideUnitCircle * radius;
            } while (randomPoint2D.magnitude < holeRadius);

            Vector3 randomPoint3D = new Vector3(randomPoint2D.x, zeroY ? 0 : position.y, randomPoint2D.y);
            return randomPoint3D + position;
        }

        Quaternion GenerateBoatRotation(Vector3 position, Vector3 boatPosition)
        {
            Vector3 directionToIsland = position - boatPosition;
            Quaternion rotation = Quaternion.LookRotation(directionToIsland, Vector3.up);
            return rotation;
        }

        public Vector3[] GenerateSpawnPointList(Vector3 position, float holeRadius, float radius, bool zeroY,
            int amount)
        {
            Vector3[] spawnPositionList = new Vector3[amount];
            for (int i = 0; i < amount; i++)
            {
                spawnPositionList[i] = GeneratePointInDonut(position, holeRadius, radius, zeroY);
            }

            return spawnPositionList;
        }

        public void Spawn(Vector3 position, Vector3[] spawnPositionList, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var o = spawnerScriptable.pool.Get();
                o.transform.position = spawnPositionList[i];
                o.transform.rotation = GenerateBoatRotation(position, spawnPositionList[0]);
            }
        }

        [Button]
        public void Check(int amount = 5)
        {
            // spawnControllerScriptableObject.GeneratePosition(target.transform.position, noSpawnRadius,
            //     sphereCollider.radius/2,
            //     amount);
        }

        [Button]
        public void Spawn(int amount = 5)
        {
            // spawnControllerScriptableObject.SpawnArray(target.transform.position, target.transform.rotation.eulerAngles,
            //     noSpawnRadius,
            //     sphereCollider.radius / 2, amount, () => { });
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(target.transform.position, noSpawnRadius);

            // Gizmos.color = Color.green;
            // spawnControllerScriptableObject.spawnPositionList.ForEach((Vector3 position) =>
            //     Gizmos.DrawSphere(position, 1));
        }
    }
}