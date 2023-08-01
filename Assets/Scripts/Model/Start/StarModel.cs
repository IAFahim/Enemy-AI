using System;
using System.Collections.Generic;
using System.Linq;
using Model.Identifier;
using Model.View;
using TriInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Model.Start
{
    [Serializable]
    [DeclareHorizontalGroup("StarRange")]
    public class StarModel
    {
        public int star;

        [Group("StarRange")] public int minStar;
        [Group("StarRange")] public int maxStar;

        [Range(-1, 101)] [OnValueChanged(nameof(Check))]
        public int point;

        [OnValueChanged(nameof(OnStageCountChanged))]
        public int[] pointStage;

        [DisableInEditMode] public string lastAddedObjectKey;
        [DisableInEditMode] public float lastAddedPoint;
        public int inCount;

        [FormerlySerializedAs("starPointContributors")]
        public List<keyInt> objectList;

        public UnityEvent<StarModel> onStarCountChanged;

        void Check()
        {
            Point = point;
        }

        public int Star
        {
            get => star;
            set
            {
                star = Math.Clamp(value, minStar, maxStar);
                onStarCountChanged?.Invoke(this);
            }
        }

        public int Point
        {
            get => point;
            set
            {
                point = value;
                if (point < 0)
                {
                    Star = minStar;
                    return;
                }

                for (var i = 0; i < pointStage.Length; i++)
                {
                    if (point <= pointStage[i])
                    {
                        Star = i + 1;
                        return;
                    }
                }

                Star = maxStar;
            }
        }

        public void AddPoints(IKey key, int points = 1)
        {
            inCount++;
            AddPoints(key.Key, points);
        }

        private void AddPoints(String key, int points = 1)
        {
            Point += points;

            lastAddedPoint = points;
            lastAddedObjectKey = key;

            var existingKeyInt = objectList.Find(keyInt => keyInt.key == key);

            if (existingKeyInt != null)
            {
                existingKeyInt.Point += points;
            }
            else
            {
                objectList.Add(new keyInt
                {
                    key = key,
                    Point = points
                });
            }
        }


        private void OnStageCountChanged()
        {
            maxStar = pointStage.Length + 1;
        }
    }
}