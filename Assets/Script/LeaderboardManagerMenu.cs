using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class LeaderboardManagerMenu : MonoBehaviour
{
    public static LeaderboardManagerMenu instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        Login();
    }
    public void Login()
    {
        Social.localUser.Authenticate((bool success) => { });
    }
    public void ShowLeaderboardMenu()
    {
        if (Social.localUser.authenticated)
        { 
            PlayGamesPlatform.Instance.ShowLeaderboardUI(Leaderboards.leaderboard_high_score); 
        }
        else
        {
            Login();
        }
    }
}
