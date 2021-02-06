using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    float SnakeSpeed;
    public int MapWidth, MapHeight;
    [SerializeField]
    float PizzaTimer;
    [HideInInspector]
    int Score = 0;
    public MapController mapController;
    public SteeringWheelController steeringWheelController;
    bool isPlaying = false; // indicates if the game is in play or pause
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
            mapController.MapGeneration(MapWidth,MapHeight);
        }
    }
    void StartGame()
    {

    }
    void PlayPause()
    {
        if (isPlaying)
        {
            
        }
        else
        {
            
        }
    }
    void GameOver()
    {

    }
    void RestartGame()
    {
        Score = 0;
    }
    public void PlayerScored()
    {
        Score++;
    }
    public float GetSteeringWheelAxis()
    {
        return steeringWheelController.SteeringWheelHorizontalAxis;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
