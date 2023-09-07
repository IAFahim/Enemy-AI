using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Controller.Score
{
    public class NumberStatus : MonoBehaviour
    {
        public Image backgroundImage;
        public Image horizontalBar;
        [FormerlySerializedAs("xp")] public Text NumberText;
        public Text label;

        private void OnValidate()
        {
            backgroundImage ??= GetComponent<Image>();
            horizontalBar ??= transform.GetChild(0).GetComponent<Image>();
            NumberText ??= transform.GetChild(1).GetComponent<Text>();
            label ??= transform.GetChild(2).GetComponent<Text>();
        }

        public void SetXp(float value)
        {
            
        }
    }
}