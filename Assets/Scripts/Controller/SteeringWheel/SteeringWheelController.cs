using System;
using Model.SteeringWheel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Controller.SteeringWheel
{
    public class SteeringWheelController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public SteeringWheelModel data;
        public UnityEvent<float> onSteer;
        
        public float normalized;

        public float Value
        {
            get => normalized;
            set
            {
                normalized = value;
                onSteer?.Invoke(value);
            }
        }

        private void OnValidate()
        {
            data.wheel ??= GetComponent<RectTransform>();
        }

        private void Update()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float deltaAngle = data.releaseSpeed * Time.deltaTime;

            if (Mathf.Abs(horizontalInput) > 0.1f)
            {
                data.wheelAngle += horizontalInput * data.maxSteerAngle * Time.deltaTime;
                data.wheelAngle = Mathf.Clamp(data.wheelAngle, -data.maxSteerAngle, data.maxSteerAngle);
            }
            else if (!data.wheelbeingheld && data.wheelAngle != 0f)
            {
                if (Mathf.Abs(deltaAngle) > Mathf.Abs(data.wheelAngle))
                    data.wheelAngle = 0f;
                else if (data.wheelAngle > 0f)
                    data.wheelAngle -= deltaAngle;
                else
                    data.wheelAngle += deltaAngle;
            }

            data.wheel.localEulerAngles = new Vector3(0, 0, -data.maxSteerAngle * normalized);
            Value = data.wheelAngle / data.maxSteerAngle;
        }

        public void OnDrag(PointerEventData eventData)
        {
            float newAngle = Vector2.Angle(Vector2.up, eventData.position - data.center);
            if ((eventData.position - data.center).sqrMagnitude >= 400)
            {
                if (eventData.position.x > data.center.x)
                    data.wheelAngle += newAngle - data.lastWheelAngle;
                else
                    data.wheelAngle -= newAngle - data.lastWheelAngle;
            }

            data.wheelAngle = Mathf.Clamp(data.wheelAngle, -data.maxSteerAngle,
                data.maxSteerAngle);
            data.lastWheelAngle = newAngle;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            data.wheelbeingheld = true;
            data.center =
                RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, data.wheel.position);
            data.lastWheelAngle = Vector2.Angle(Vector2.up, eventData.position - data.center);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnDrag(eventData);
            data.wheelbeingheld = false;
        }
    }
}