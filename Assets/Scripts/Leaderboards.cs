using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboards
{
    public List<LeaderBoardItem> leaderboardItems;
    int TotalHighScores;
    public Leaderboards()
    {
        TotalHighScores = GetTotalHighScores();
        if(TotalHighScores != 0)
        {
            SetLeaderboardItems();
        }
    }
    public Leaderboards(int val)
    {
        ClearLeaderBoard();
        SetTotalHighScores(val);
        SetLeaderboardItems();
    }
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
    public void AddToLeaderBoard(LeaderBoardItem val)
    {
        if(val.PlayerName == "")
        {
            val.PlayerName = "Player" + leaderboardItems.Count.ToString();
        }
        leaderboardItems.Add(val);
        leaderboardItems.Sort(delegate (LeaderBoardItem x, LeaderBoardItem y) {
            return x.HighScore.CompareTo(y.HighScore);
        });
        if(leaderboardItems.Count > TotalHighScores && TotalHighScores != 0)
        {
            leaderboardItems.RemoveRange(TotalHighScores,leaderboardItems.Count -1);
        }
        SaveLeaderBoardItems();
    }
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
