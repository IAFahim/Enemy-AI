using System;
using System.Collections.Generic;
using Model.Spawner;
using TriInspector;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Controller.ScriptableObjects.Spawner
{
    [CreateAssetMenu(fileName = "Spawner", menuName = "Spawner", order = 0)]
    public class SpawnControllerScriptableObject : SpawnModel
    {
        public void OnEnable()
        {
            Pool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy, false, 5, 10);
        }

        private void ActionOnDestroy(GameObject o)
        {
            o.gameObject.SetActive(false);
        }

        private void ActionOnRelease(GameObject o)
        {
            Destroy(o);
        }

        private void ActionOnGet(GameObject o)
        {
            o.gameObject.SetActive(true);
        }

        private GameObject CreateFunc()
        {
            return Instantiate(prefab);
        }


        public GameObject Prefab
        {
            get => prefab;
            set => prefab = value;
        }

        public List<Vector3> GeneratePosition(Vector3 position, float noSpawnRadius, float radius, int amount)
        {
            spawnPositionList.Clear();
            for (int i = 0; i < amount; i++)
            {
                spawnPositionList.Add(GeneratePosition(position, noSpawnRadius, radius));
            }

            return spawnPositionList;
        }

        private Vector3 GeneratePosition(Vector3 position, float noSpawnRadius, float radius)
        {
            Vector2 randomPoint2D;
            do
            {
                randomPoint2D = Random.insideUnitCircle * radius;
            } while (randomPoint2D.magnitude < noSpawnRadius);

            Vector3 randomPoint3D = new Vector3(randomPoint2D.x, position.y, randomPoint2D.y);
            return randomPoint3D + position;
        }

        [Button]
        public void SpawnArray(Vector3 position, Vector3 rotation, float noSpawnRadius, float radius, int amount,
            Action callbackPer)
        {
            foreach (var portion in GeneratePosition(position, noSpawnRadius, radius, amount))
            {
                SpawnSingle(portion, rotation, callbackPer);
            }
        }

        public void SpawnSingle(Vector3 position, Vector3 rotation, Action callback)
        {
            GameObject gameObject = Pool.Get();
            gameObject.transform.position = position;
            gameObject.transform.eulerAngles = rotation;
            callback();
        }


        public void DeSpawn(GameObject gameObject)
        {
            Pool.Release(gameObject);
        }
    }
}