using System;
using Model.Identifier;
using Model.View;
using UnityEngine;

namespace Controller.Star
{
    [CreateAssetMenu(fileName = "StarSystem", menuName = "ScriptableObjects/StarSystem", order = 1)]
    public class StarSystem : StarModelController
    {
        public void AddPoints(IKey key, int points = 1)
        {
            AddPoints(key.Key, points);
        }

        public void AddPoints(String key, int points = 1)
        {
            inCount++;
            Point += points;

            var existingKeyInt = inList.Find(keyInt => keyInt.key == key);

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
                inList.Add(lastEnteredObject);
            }
        }
    }
}