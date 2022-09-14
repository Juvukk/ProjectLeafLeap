using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    //this script is to keep constantly updated with the latest high scores. Scores will be stored in a LeaderboardData array, updated every time a game round ends. 

    public TextMeshProUGUI topScore1Text;
    public TextMeshProUGUI topScore2Text;
    public TextMeshProUGUI topScore3Text;
    public TextMeshProUGUI topScore4Text;
    public TextMeshProUGUI topScore5Text;

    public SortedList leaderboardData = new();

    //    public SortedList<LeaderboardData> = new SortedList<LeaderboardData>(); //this is a list not an array




    // Start is called before the first frame update
    void Start()
    {

    }


    //the following script is for subscribing to the player score

    public void UpdateLeaderboard(int newScore) //player name may need to be removed
    {
        //checking if there's already an identical score, if so add new score 1 below, if not, add score
        if (leaderboardData.ContainsKey(newScore))
        {
            leaderboardData.Add(newScore -1, newScore.ToString());
        }
        else
        {
            leaderboardData.Add(newScore, newScore.ToString());
        }
        //checking if there's more than 5 scores, if so deleting lowest score
        if (leaderboardData.Count > 5)
        {
            leaderboardData.RemoveAt(5);
        }
        //setting the scores in the leaderboard UI element
        topScore1Text.SetText(leaderboardData.GetByIndex(0).ToString());
        topScore2Text.SetText(leaderboardData.GetByIndex(1).ToString());
        topScore3Text.SetText(leaderboardData.GetByIndex(2).ToString());
        topScore4Text.SetText(leaderboardData.GetByIndex(3).ToString());
        topScore5Text.SetText(leaderboardData.GetByIndex(4).ToString());
    }
}
