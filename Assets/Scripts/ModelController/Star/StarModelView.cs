using System;
using System.Collections.Generic;
using Model.Identifier;
using Model.View;
using TriInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace ModelController.Star
{
    [Serializable]
    [DeclareHorizontalGroup("StarRange")]
    public class StarModelView : ScriptableObject
    {
        public int star;

        [Group("StarRange")] public int minStar;
        [Group("StarRange")] public int maxStar;

        [Range(-1, 101)] [OnValueChanged(nameof(Check))]
        public int point;

        [OnValueChanged(nameof(OnStageCountChanged))]
        public int[] pointStage;

        [DisableInEditMode] public keyInt lastEnteredObject;
        public int inCount;

        [FormerlySerializedAs("starPointContributors")]
        public List<keyInt> objectList;

        public UnityEvent<StarModelView> onStarCountChanged;

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
                        Star = minStar + i;
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

            var existingKeyInt = objectList.Find(keyInt => keyInt.key == key);

            if (existingKeyInt != null)
            {
                existingKeyInt.Point += points;
                lastEnteredObject = existingKeyInt;
            }
            else
            {
                lastEnteredObject = new keyInt
                {
                    key = key,
                    Point = points
                };
                objectList.Add(lastEnteredObject);
            }
        }


        private void OnStageCountChanged()
        {
            maxStar = pointStage.Length + 1;
        }
    }
}