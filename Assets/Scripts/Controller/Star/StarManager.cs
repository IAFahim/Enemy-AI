using Model.Identifier;
using UnityEngine;
using UnityEngine.Events;

namespace Controller.Star
{
    [CreateAssetMenu(fileName = "StarManager", menuName = "ScriptableObjects/StarManager", order = 1)]
    public class StarManager : ScriptableObject
    {
        [Range(-1, 101), SerializeField] private int point;

        public UnityEvent<int> onPointChange;

        [SerializeField] public int star;
        public UnityEvent<int> onStarChangedEvent;
        [SerializeField] protected int[] starRange;

        public int inCount;

        void OnValidate()
        {
            Point = point;
        }

        private void OnEnable()
        {
            Point = point;
        }

        public int Point
        {
            get => point;
            private set
            {
                point = value;
                onPointChange?.Invoke(point);
                for (var i = 0; i < starRange.Length; i++)
                {
                    if (point < starRange[i])
                    {
                        if (star == i) return;
                        Star = i;
                        return;
                    }
                }

                Star = starRange.Length;
            }
        }

        public int Star
        {
            get => star;
            private set
            {
                star = value;
                Debug.Log("Star: " + star);
                onStarChangedEvent?.Invoke(star);
            }
        }

        public void AddPoints(IKey id, int points = 1)
        {
            inCount++;
            Point += points;
        }

        private void Reset()
        {
            Point = 0;
            inCount = 0;
        }
    }
}