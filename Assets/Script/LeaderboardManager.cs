using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class LeaderboardManager : MonoBehaviour
{
    private UIManager _uiManager;
    private int _endScore;
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_uiManager==null)
        {
            Debug.LogError("UI Manager failed from Leaderboard Manager");
        }
        PlayGamesPlatform.Activate();
        Login();
    }
    public void Login()
    {
        Social.localUser.Authenticate((bool success) => { });
    }
    public void AddScoreToLeaderboard(int UIscore)
    {
        if (Social.localUser.authenticated)
        {
            _endScore = UIscore;
            Social.ReportScore(_endScore, Leaderboards.leaderboard_high_score, (bool success) => { });
        }
        else
        {
            Login();      
        }
    }
}
