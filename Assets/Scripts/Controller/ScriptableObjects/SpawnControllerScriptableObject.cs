using Model.Spawner;
using TriInspector;
using UnityEngine;
using UnityEngine.Pool;

namespace Controller.ScriptableObjects
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

        [Button]
        public void Spawn(Vector3 position, Vector3 rotation, int amount)
        {
            GameObject gameObject = Pool.Get();
            gameObject.transform.position = position;
            gameObject.transform.eulerAngles = rotation;
        }
        
        public void DeSpawn(GameObject gameObject)
        {
            Pool.Release(gameObject);
        }
    }
}