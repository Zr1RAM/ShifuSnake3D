using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboards
{
    public LeaderBoardItem[] leaderboardItems;
    public Leaderboards()
    {
        if(GetTotalHighScores() != 0)
        {
            SetLeaderboardItems();
        }
    }
    public Leaderboards(int val)
    {
        ClearLeaderBoard();
        SetMaxHighScores(val);
    }
    public void SetLeaderboardItems()
    {
        int NumberOfHighScores = PlayerPrefs.GetInt("TotalScores");
        leaderboardItems = new LeaderBoardItem[NumberOfHighScores];
        string PlayerName;
        int HighScore;
        for(int i = 0; i < NumberOfHighScores; i++)
        {
            PlayerName = PlayerPrefs.GetString("PlayerName" + i.ToString());
            HighScore = PlayerPrefs.GetInt("PlayerScore" + i.ToString());
            leaderboardItems[i] = new LeaderBoardItem(PlayerName, HighScore);
        }
    }
    public LeaderBoardItem[] GetLeaderboardItems()
    {
        return leaderboardItems;
    }
    public void SetMaxHighScores(int val)
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
        for(int i = 0; i < GetTotalHighScores(); i++)
        {
            if(val.HighScore > PlayerPrefs.GetInt("PlayerScore" + i.ToString()))
            {
                AddtoLeaderBoardAt(val,i);
                break;
            }
        }
    }
    void AddtoLeaderBoardAt(LeaderBoardItem val,int LeaderBoardIndex)
    {
        PlayerPrefs.SetString("PlayerName" + LeaderBoardIndex.ToString(), val.PlayerName);
        PlayerPrefs.SetInt("PlayerScore" + LeaderBoardIndex.ToString(),val.HighScore);
    }
}
