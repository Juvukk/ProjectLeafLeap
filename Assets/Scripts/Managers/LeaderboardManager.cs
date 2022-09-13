using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LeaderboardManager : MonoBehaviour
{
    //this script is to keep constantly updated with the latest high scores. Scores will be stored in a LeaderboardData array, updated every time a game round ends. 

    public List<LeaderboardData> leaderboards; //this is a list not an array




    // Start is called before the first frame update
    void Start()
    {
        
    }


    //the following script is for subscribing to the player score

    public void UpdateLeaderboard(int newScore, string playerName) //player name may need to be removed
    {
        LeaderboardData newEntry = new LeaderboardData
        {
            playerName = playerName,
            playerScore = newScore
        };

        leaderboards.Add(newEntry);
       // leaderboards.Sort
      //  list

    }
}
