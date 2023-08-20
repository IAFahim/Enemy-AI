using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace Controller.ScriptableObjects.Spawner
{
    [CreateAssetMenu(fileName = "Spawner", menuName = "Spawner", order = 0)]
    public class SpawnControllerSO : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        public ObjectPool<GameObject> pool;

        private void OnEnable()
        {
            pool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy, false,
                10, 50);
        }

        private void ActionOnDestroy(GameObject o)
        {
            o.SetActive(false);
        }

        private void ActionOnRelease(GameObject o)
        {
            Destroy(o);
        }

        private void ActionOnGet(GameObject o)
        {
            o.SetActive(true);
        }

        private GameObject CreateFunc()
        {
            return Instantiate(prefab);
        }

        public void DeSpawn(GameObject gameObject)
        {
            pool.Release(gameObject);
        }
    }
}