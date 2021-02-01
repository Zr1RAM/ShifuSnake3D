using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    float SnakeSpeed;
    [SerializeField]
    int MapWidth, MapHeight;
    [SerializeField]
    float PizzaTimer;
    [HideInInspector]
    public int Score,NonTraversedTileCount;
    [SerializeField]
    MapController mapController;
    bool isPlaying = false; // indicates if the game is in play or pause
    
    void Awake()
    {
        Initialise();    
    }
    void Initialise()
    {
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
