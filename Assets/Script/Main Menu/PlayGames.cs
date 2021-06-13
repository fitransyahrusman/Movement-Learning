using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class PlayGames : MonoBehaviour
{
    private int _endScore;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        Initialize();
    }
    void Initialize()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().RequestServerAuthCode(false).Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        SigningUserWithPlayGames();
    }
    public void SigningUserWithPlayGames()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways , (success) =>
      {} );
    }
    public void SigningOutUserWithPlayGames()
    {
        PlayGamesPlatform.Instance.SignOut();
    }
    public void PostScoreToLeaderboard (int valuefromUI)
    {
        _endScore = valuefromUI;
        Social.ReportScore( _endScore, "CgkI7vjHtcQJEAIQAA", (bool success) =>
        { });
    }
    public void ShowLeaderboard()
    {
       if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            SigningUserWithPlayGames();
        }
        
    }
}
