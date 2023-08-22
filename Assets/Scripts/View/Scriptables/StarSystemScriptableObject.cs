using System;
using Model.Identifier;
using Model.View;
using ModelController.Star;
using UnityEngine;

namespace View.ScriptableObjects
{
    [CreateAssetMenu(fileName = "StarSystem", menuName = "ScriptableObjects/StarSystem", order = 1)]
    public class StarSystemScriptableObject : StarModelController
    {
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
    }
}