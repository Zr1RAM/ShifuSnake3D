using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class LeaderBoardItem
{
    public string PlayerName;
    public int HighScore;
    public LeaderBoardItem(string playerName, int highScore)
    {
        this.PlayerName = playerName;
        this.HighScore = highScore;
    }
}
