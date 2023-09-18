using Model.Identifier;
using TriInspector;
using UnityEngine;

namespace Model.IDs
{
    [HideMonoScript]
    public class ID : MonoBehaviour, IKey
    {
        [DisableInPlayMode, DisableInEditMode] public string key;
        public string Key => key;
    }
}