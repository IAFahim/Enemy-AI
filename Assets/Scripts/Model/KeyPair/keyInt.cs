using System;
using Model.Identifier;
using Pancake.Apex;
using UnityEngine;

namespace Model.KeyPair
{
    [Serializable]
    public class keyInt : IKey
    {
        [DisableInEditorMode] public string key;
        public string Key => key;
        [field: SerializeField] public int Point { get; set; }

        public void SetAsKey()
        {
            
        }
    }
}