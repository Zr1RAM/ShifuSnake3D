//Class that contains all functions and variables for saving and loading Leaderboards Data from PlayerPrefs.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboards
{
    public List<LeaderBoardItem> leaderboardItems; //Used to store and retrieve leaderboard scores from PlayerPrefs.
    int TotalHighScores; //Maximum number of highscores to display.
    // Constructor for when retrieving Leaderboards data.
    public Leaderboards() 
    {
        TotalHighScores = GetTotalHighScores();
        if(TotalHighScores != 0)
        {
            SetLeaderboardItems();
        }
    }
    // Constructor for when Setting up leaderboards for the first time. 
    // Or after clearing the Leaderboard
    public Leaderboards(int val)
    {
        ClearLeaderBoard();
        SetTotalHighScores(val);
        SetLeaderboardItems();
    }
    //called when retrieving Leaderboards data and populating leaderboardItems.
    public void SetLeaderboardItems()
    {
        int NumberOfHighScores = PlayerPrefs.GetInt("TotalScores");
        leaderboardItems = new List<LeaderBoardItem>();
        string PlayerName;
        int HighScore;
        for(int i = 0; i < NumberOfHighScores; i++)
        {
            HighScore = PlayerPrefs.GetInt("PlayerScore" + i.ToString());
            if (HighScore != 0)
            {
                PlayerName = PlayerPrefs.GetString("PlayerName" + i.ToString());
                leaderboardItems.Add(new LeaderBoardItem(PlayerName, HighScore));
            }
        }
    }
    public List<LeaderBoardItem> GetLeaderboardItems()
    {
        return leaderboardItems;
    }
    public void SetTotalHighScores(int val)
    {
        PlayerPrefs.SetInt("TotalScores",val);
    }
    public int GetTotalHighScores()
    {
        return PlayerPrefs.GetInt("TotalScores");
    }
    public void ClearLeaderBoard()
    {
        PlayerPrefs.DeleteAll();
    }
    //Called when updating or adding new Highscore to leaderboard
    public void AddToLeaderBoard(LeaderBoardItem val)
    {
        if(val.PlayerName == "")
        {
            val.PlayerName = "Player" + leaderboardItems.Count.ToString();
        }
        leaderboardItems.Add(val);
        leaderboardItems.Sort(delegate (LeaderBoardItem x, LeaderBoardItem y) { //For sorting list based on highscore
            return x.HighScore.CompareTo(y.HighScore);
        });
        if(leaderboardItems.Count > TotalHighScores && TotalHighScores != 0)
        {
            leaderboardItems.RemoveRange(TotalHighScores,leaderboardItems.Count -1);
        }
        SaveLeaderBoardItems();
    }
    //Called when saving leaderboards data into PlayerPrefs.
    void SaveLeaderBoardItems()
    {
        for(int i = 0; i < leaderboardItems.Count; i++)
        {
            PlayerPrefs.SetString("PlayerName" + i.ToString(), leaderboardItems[i].PlayerName);
            PlayerPrefs.SetInt("PlayerScore" + i.ToString(), leaderboardItems[i].HighScore);
        }
        PlayerPrefs.Save();
    }
}
