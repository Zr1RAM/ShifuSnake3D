using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    float SnakeSpeed;
    public int MapWidth, MapHeight;
    [SerializeField]
    float PizzaTimer;
    [HideInInspector]
    public int Score = 0;
    public MapController mapController;
    public SteeringWheelController steeringWheelController;
    public UIManager uiManager;
    //[HideInInspector]
    public bool isPlaying = true; // indicates if the game is in play or pause
    [HideInInspector]
    public GameObject PlayerObject;
    public static GameManager gameManagerInstance;
    
    void Awake()
    {
        Initialise();    
    }
    void Initialise()
    {
        if(gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
        if(mapController != null)
        {
            mapController.InitializeMapController(MapWidth,MapHeight);
        }
        uiManager.InitializeUIManager();
    }
     
    public void StartGame()
    {
        isPlaying = true;
    }
    public void PlayPause(bool val)
    {
        if (val) // true means we are resuming 
        {
            Time.timeScale = 1;
        }
        else // False means we are pausing;
        {
            Time.timeScale = 0;
        }
        isPlaying = val;
        //isPlaying = !isPlaying;
    }
    public void GameOver()
    {
        PlayPause(false);
        uiManager.InvokeGameOverEvent();
    }
    public void RestartGame()
    {
        Score = 0;
        mapController.ResetTiles();
    }
    public void PlayerScored()
    {
        Score++;
        uiManager.UpdateScore(Score);
    }
    public float GetSteeringWheelAxis()
    {
        return steeringWheelController.SteeringWheelHorizontalAxis;
    }
    public Vector3 GetAvailableTilePositionInMap()
    {
        return mapController.AvailableTilePosition();
    }
}
