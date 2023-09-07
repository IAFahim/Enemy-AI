using System;
using System.Collections.Generic;
using Model.View;
using TriInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Controller.Star
{
    [Serializable]
    [DeclareHorizontalGroup("StarRange")]
    public class StarModelController : ScriptableObject
    {
        [SerializeField] protected int star;

        [Group("StarRange")] [SerializeField] protected int minStar;
        [Group("StarRange")] [SerializeField] protected int maxStar;

        [Range(-1, 101)] [OnValueChanged(nameof(Check))]
        public int point;

        [OnValueChanged(nameof(OnStageModified))] [SerializeField]
        protected int[] pointStage;

        [DisableInEditMode] [SerializeField] protected keyInt lastEnteredObject;
        public int inCount;

        [SerializeField]
        protected List<keyInt> inList;

        public UnityEvent<StarModelController> onStarCountChanged;

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

        private void OnStageModified()
        {
            maxStar = pointStage.Length + 1;
        }
    }
}