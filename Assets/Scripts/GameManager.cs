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
    public int Score,NonTraversedTileCount;
    public MapController mapController;
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
        NonTraversedTileCount = MapWidth * MapHeight;
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

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
