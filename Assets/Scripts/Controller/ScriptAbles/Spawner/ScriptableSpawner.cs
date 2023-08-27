using UnityEngine;
using UnityEngine.Pool;

namespace Controller.ScriptAbles.Spawner
{
    [CreateAssetMenu(fileName = "Spawner", menuName = "Spawner", order = 0)]
    public class ScriptableSpawner : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        public ObjectPool<GameObject> pool;

        private void OnEnable()
        {
            pool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy, false,
                10, 50);
        }
        
        private GameObject CreateFunc()
        {
            return Instantiate(prefab);
        }
        
        private void ActionOnRelease(GameObject o)
        {
            o.SetActive(false);
        }
        
        private void ActionOnGet(GameObject o)
        {
            o.SetActive(true);
        }
        
        private void ActionOnDestroy(GameObject o)
        {
            Destroy(o);
        }
        
        public void DeSpawn(GameObject gameObject)
        {
            Debug.Log("Yamete Kudasai");
            pool.Release(gameObject);
        }
    }
}