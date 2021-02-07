using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRigController : MonoBehaviour
{
    public enum CameraRigSetUp {Default, PlayerFollowFar , PlayerFollowClose }; 

    [SerializeField]
    Camera MainCamera;
    [SerializeField]
    Transform TopWall, RightWall, BottomWal;

    public CameraRigSetUp cameraRigSetUp = CameraRigSetUp.Default;
    // Start is called before the first frame update
    void Start()
    {
        CameraRigSetupOnStart();
    }
    void CameraRigSetupOnStart()
    {
        switch(cameraRigSetUp)
        {
            case CameraRigSetUp.Default:
                {
                    TopViewCameraSetup();
                    break;
                }
            case CameraRigSetUp.PlayerFollowFar:
                {
                    FarFollowCam();
                    break;
                }
            case CameraRigSetUp.PlayerFollowClose:
                {
                    ClosefollowCam();
                    break;
                }
        }
    }
    void TopViewCameraSetup()
    {

    }
    void FarFollowCam()
    {

    }
    void ClosefollowCam()
    {

    }
    // Update is called once per frame
    void Update()
    {
        //print("top wall: " + MainCamera.WorldToViewportPoint(TopWall.position));
        //print("Right wall: " + MainCamera.WorldToViewportPoint(RightWall.position));
        //print("Bottom wall: " + MainCamera.WorldToViewportPoint(BottomWal.position));
    }
}
