using Pancake.Apex;
using UnityEngine;

namespace Model.Identifier
{
    [HideMonoScript]
    public class ID : MonoBehaviour, IKey
    {
        [DisableInPlayMode, DisableInEditorMode] public string key;
        public string Key => key;
    }
}