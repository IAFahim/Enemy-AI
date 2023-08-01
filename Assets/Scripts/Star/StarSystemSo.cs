using Model.Start;
using UnityEngine;
using UnityEngine.Serialization;

namespace Star
{
    [CreateAssetMenu(fileName = "StarSystem", menuName = "ScriptableObjects/StarSystem", order = 1)]
    public class StarSystemSo : ScriptableObject
    {
        [FormerlySerializedAs("starLogic")] public StarModel starModel;
    }
}