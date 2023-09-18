using System;
using TriInspector;
using UnityEngine;
using UnityEngine.Pool;

namespace Controller.ScriptAbles.Spawner
{
    [CreateAssetMenu(fileName = "Pool", menuName = "ScriptableObjects/Scriptable Pool", order = 0), HideMonoScript]
    public class ScriptablePool : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        private ObjectPool<GameObject> _pool;

        [OnValueChanged(nameof(SetMaxCapacityToNextNextPowerOfDefaultCapacity))]
        public int defaultCapacity = 2;

        public int maxCapacity = 4;

        private void OnEnable()
        {
            _pool = new ObjectPool<GameObject>(CreateFunc, ActionOnGet, ActionOnRelease, ActionOnDestroy, true,
                defaultCapacity, maxCapacity);
        }

        public GameObject Get()
        {
            return _pool.Get();
        }

        private void SetMaxCapacityToNextNextPowerOfDefaultCapacity()
        {
            maxCapacity = defaultCapacity << 1;
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

        public void Release(GameObject gameObject)
        {
            _pool.Release(gameObject);
        }

        public void OnDisable()
        {
            _pool.Clear();
        }
    }
}