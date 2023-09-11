using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Model.SteeringWheel
{
    [Serializable]
    public class SteeringWheelModel 
    {
        public bool wheelbeingheld;
        public RectTransform wheel;
        public float wheelAngle;
        public float lastWheelAngle;
        public Vector2 center;
        public float maxSteerAngle = 200f;
        public float releaseSpeed = 300f;
        
    }
}
