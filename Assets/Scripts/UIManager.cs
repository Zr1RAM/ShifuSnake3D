// Unity UI System from the editor is mostly used for transitions from Start Game Screen to Game View , Pause menu, etc
// During those transitions some of the funtions and states of the Script components are executed using UI Manager.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text ScoreText; //Reference to Text Component that displays score
    Animator CanvasAnimator; //Reference to the UI CAnvas Animator 
    [SerializeField]
    UnityEvent GameOverEvent, StartGameEvent; // List of events and functions that can be provided from the 
                                              //editor to be called from GameManager for Game Start and Game Over
    [SerializeField]
    Text LeaderboardText; // Text that displays the Scoreboard
    // Starting up UI Manager.
    public void InitializeUIManager() 
    {
        CanvasAnimator = transform.GetComponent<Animator>();
    }
    // Function to update score text and score effects when player scores.
    public void UpdateScore(int Score) 
    {
        ScoreText.text = " " + Score;
        CanvasAnimator.SetTrigger("Score");
    }
    public void InvokeGameOverEvent()
    {
        if(GameOverEvent != null)
        {
            GameOverEvent.Invoke();
        }
    }
    public void InvokeStartGameEvent()
    {
        if (StartGameEvent != null)
        {
            StartGameEvent.Invoke();
        }
    }
    //Called when updating the leaderboard.
    public void UpdateLeaderBoardText(List<LeaderBoardItem> val)
    {
        LeaderboardText.text = "";
        for (int i = val.Count - 1; i >= 0; i--)
        { 
            if(val[i].HighScore != 0)
            {
                LeaderboardText.text += val[i].PlayerName + " " + val[i].HighScore.ToString() + "\n";
            }
        }
    }

}
