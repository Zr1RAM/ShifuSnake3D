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
    public SoundsManager soundsManager;
    //[HideInInspector]
    public bool isPlaying = false; // indicates if the game is in play or pause
    [HideInInspector]
    public GameObject PlayerObject;
    Leaderboards leaderboards;
    [SerializeField]
    int MaxNumOfHighScores;
    public static GameManager gameManagerInstance;
    void Awake()
    {
        Initialise();
        //for (int i = 0; i < MaxNumOfHighScores; i++)
        //{
        //    string PlayerName = PlayerPrefs.GetString("PlayerName" + i.ToString());
        //    int HighScore = PlayerPrefs.GetInt("PlayerScore" + i.ToString());
        //    print("Player name: " + PlayerName + " HighScore: " + HighScore.ToString());
        //}
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
        PlayPause(true);
        uiManager.InvokeStartGameEvent();
        InitializeLeaderBoard();
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
        UpdateLeaderboard();
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
        soundsManager.PlayScoredSound();
    }
    public float GetSteeringWheelAxis()
    {
        return steeringWheelController.SteeringWheelHorizontalAxis;
    }
    public Vector3 GetAvailableTilePositionInMap()
    {
        return mapController.AvailableTilePosition();
    }
    public void PlayWallBounceSoundFromSoundManager()
    {
        soundsManager.PlayWallBounceSound();
    }
    public void PlayPizzaLostSoundFromSoundManager()
    {
        soundsManager.PlayPizzaLostSound();
    }
    void UpdateLeaderboard()
    {
        leaderboards.AddToLeaderBoard(new LeaderBoardItem("",Score));
        uiManager.UpdateLeaderBoardText(leaderboards.GetLeaderboardItems());
    }
    void InitializeLeaderBoard()
    {
        leaderboards = new Leaderboards();
        if (leaderboards.leaderboardItems == null)
        {
            leaderboards = new Leaderboards(MaxNumOfHighScores);
        }
    }
    public void UpdateLeaderBoardText()
    {
        uiManager.UpdateLeaderBoardText(leaderboards.GetLeaderboardItems());
    }
}
