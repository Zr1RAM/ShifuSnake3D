using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SteeringWheelController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    bool isSteeringWheelHeld = false;
    [SerializeField]
    RectTransform SteeringWheelUI;
    float SteeringWheelAngle = 0f;
    float PreviousSteeringWheelAngle = 0f;
    Vector2 TouchCenter;
    [SerializeField]
    float MaxSteerAngle = 200f;
    [SerializeField]
    float ReleaseSpeed = 300f;
    public float SteeringWheelHorizontalAxis;
    void Update()
    {
        if (!isSteeringWheelHeld && SteeringWheelAngle != 0f)
        {
            float DeltaAngle = ReleaseSpeed * Time.deltaTime;
            if (Mathf.Abs(DeltaAngle) > Mathf.Abs(SteeringWheelAngle))
                SteeringWheelAngle = 0f;
            else if (SteeringWheelAngle > 0f)
                SteeringWheelAngle -= DeltaAngle;
            else
                SteeringWheelAngle += DeltaAngle;
        }
        SteeringWheelUI.localEulerAngles = new Vector3(0, 0, -MaxSteerAngle * SteeringWheelHorizontalAxis);
        SteeringWheelHorizontalAxis = SteeringWheelAngle / MaxSteerAngle;
    }
    public void OnPointerDown(PointerEventData data)
    {
        isSteeringWheelHeld = true;
        TouchCenter = RectTransformUtility.WorldToScreenPoint(data.pressEventCamera, SteeringWheelUI.position);
        PreviousSteeringWheelAngle = Vector2.Angle(Vector2.up, data.position - TouchCenter);
    }
    public void OnDrag(PointerEventData data)
    {
        float NewAngle = Vector2.Angle(Vector2.up, data.position - TouchCenter);
        if ((data.position - TouchCenter).sqrMagnitude >= 400)
        {
            if (data.position.x > TouchCenter.x)
                SteeringWheelAngle += NewAngle - PreviousSteeringWheelAngle;
            else
                SteeringWheelAngle -= NewAngle - PreviousSteeringWheelAngle;
        }
        SteeringWheelAngle = Mathf.Clamp(SteeringWheelAngle, -MaxSteerAngle, MaxSteerAngle);
        PreviousSteeringWheelAngle = NewAngle;
    }
    public void OnPointerUp(PointerEventData data)
    {
        //OnDrag(data);
        isSteeringWheelHeld = false;
    }
}
