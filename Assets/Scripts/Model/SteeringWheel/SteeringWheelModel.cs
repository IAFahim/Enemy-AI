using System;
using UnityEngine;

namespace Model.SteeringWheel
{
    [Serializable]
    public class SteeringWheelModel 
    {
        public bool Wheelbeingheld = false;
        public RectTransform Wheel;
        public float WheelAngle = 0f;
        public float LastWheelAngle = 0f;
        public Vector2 center;
        public float MaxSteerAngle = 200f;
        public float ReleaseSpeed = 300f;
        
    }
}
