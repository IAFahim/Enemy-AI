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

        private void Update()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float deltaAngle = data.ReleaseSpeed * Time.deltaTime;

            if (Mathf.Abs(horizontalInput) > 0.1f)
            {
                data.WheelAngle += horizontalInput * data.MaxSteerAngle * Time.deltaTime;
                data.WheelAngle = Mathf.Clamp(data.WheelAngle, -data.MaxSteerAngle, data.MaxSteerAngle);
            }
            else if (!data.Wheelbeingheld && data.WheelAngle != 0f)
            {
                if (Mathf.Abs(deltaAngle) > Mathf.Abs(data.WheelAngle))
                    data.WheelAngle = 0f;
                else if (data.WheelAngle > 0f)
                    data.WheelAngle -= deltaAngle;
                else
                    data.WheelAngle += deltaAngle;
            }

            data.Wheel.localEulerAngles = new Vector3(0, 0, -data.MaxSteerAngle * normalized);
            Value = data.WheelAngle / data.MaxSteerAngle;
        }

        public void OnDrag(PointerEventData eventData)
        {
            float newAngle = Vector2.Angle(Vector2.up, eventData.position - data.center);
            if ((eventData.position - data.center).sqrMagnitude >= 400)
            {
                if (eventData.position.x > data.center.x)
                    data.WheelAngle += newAngle - data.LastWheelAngle;
                else
                    data.WheelAngle -= newAngle - data.LastWheelAngle;
            }

            data.WheelAngle = Mathf.Clamp(data.WheelAngle, -data.MaxSteerAngle,
                data.MaxSteerAngle);
            data.LastWheelAngle = newAngle;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            data.Wheelbeingheld = true;
            data.center =
                RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, data.Wheel.position);
            data.LastWheelAngle = Vector2.Angle(Vector2.up, eventData.position - data.center);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnDrag(eventData);
            data.Wheelbeingheld = false;
        }
    }
}