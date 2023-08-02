using System;
using Model.Identifier;
using TriInspector;
using UnityEngine;

namespace Model.View
{
    [Serializable]
    public class keyInt : IKey
    {
        [DisableInEditMode] public string key;
        public string Key => key;
        [field: SerializeField] public int Point { get; set; }

        public void SetAsKey()
        {
            
        }
    }
}