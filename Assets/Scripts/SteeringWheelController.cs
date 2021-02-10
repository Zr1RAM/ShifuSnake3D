// Class that handles the steering wheel image rotation as well as provides the steering wheel axis value.

using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SteeringWheelController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    bool isSteeringWheelHeld = false; // Used to find if steering wheel is being touch (hold) on screen.
    [SerializeField]
    RectTransform SteeringWheelUI;
    float SteeringWheelAngle = 0f;
    float PreviousSteeringWheelAngle = 0f;
    Vector2 TouchCenter;
    [SerializeField]
    float MaxSteerAngle = 200f;
    [SerializeField]
    float ReleaseSpeed = 300f;
    public float SteeringWheelHorizontalAxis = 0;
    void Update()
    {
        // This if block is for when steering wheel gets back to default position on release.
        // Brings the axis value to zero on release. 
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
    //interface to pointer down callbacks
    // Get initial world to screen position of steering wheel.
    // Set previous steering wheel angle.
    public void OnPointerDown(PointerEventData data)
    {
        isSteeringWheelHeld = true;
        TouchCenter = RectTransformUtility.WorldToScreenPoint(data.pressEventCamera, SteeringWheelUI.position);
        PreviousSteeringWheelAngle = Vector2.Angle(Vector2.up, data.position - TouchCenter);
    }
    // interface to on drag callbacks
    public void OnDrag(PointerEventData data)
    {
        float CurrentSteeringWheelAngle = Vector2.Angle(Vector2.up, data.position - TouchCenter);
        if ((data.position - TouchCenter).sqrMagnitude >= 400)
        {
            if (data.position.x > TouchCenter.x)
                SteeringWheelAngle += CurrentSteeringWheelAngle - PreviousSteeringWheelAngle;
            else
                SteeringWheelAngle -= CurrentSteeringWheelAngle - PreviousSteeringWheelAngle;
        }
        SteeringWheelAngle = Mathf.Clamp(SteeringWheelAngle, -MaxSteerAngle, MaxSteerAngle);
        PreviousSteeringWheelAngle = CurrentSteeringWheelAngle;
    }
    //interface to pointer up callbacks
    public void OnPointerUp(PointerEventData data)
    {
        //OnDrag(data);
        isSteeringWheelHeld = false;
    }
}
