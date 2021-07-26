using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using GooglePlayGames;
using GoogleMobileAds.Api;
using GoogleMobileAds.Placement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _livesSprite;
    [SerializeField]
    private Image _liveImg;
    [SerializeField]
    private Text _gameoverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private GameObject _pausePanel;
    private GameManager _gameManager;
    private PlayGames _playgames;
    private int _score;
    private Animator _pauseAnimation;
    
    public UnityEvent showAd;
    public InterstitialAdGameObject interstitial;
    

    void Start()
    {
        _scoreText.text = "Score is : " + 0;
        _gameoverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _pausePanel.gameObject.SetActive(true);
        _pauseAnimation = GameObject.Find("Pause Menu"). GetComponent<Animator>();
        _pauseAnimation.updateMode = AnimatorUpdateMode.UnscaledTime;
        
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if ( _gameManager == null)
        {
            Debug.LogError("Game Manager in Null");
        }
        _playgames = GameObject.Find("PlayGames").GetComponent<PlayGames>();
        if (_playgames == null)
        {
            Debug.LogError("Playgames error from UI manager");
        }
        if (showAd==null)
        {
            showAd = new UnityEvent();
        }
        MobileAds.Initialize((success) => { });
        interstitial = MobileAds.Instance.GetAd<InterstitialAdGameObject>("Interstitial Ad");
        interstitial.LoadAd();

    }
    public void UpdateScore (int playerScore)
    {
        _score = playerScore;
        _scoreText.text = "Score is : " + _score;
    }
    public void UpdateLives(int currentLives)
    {
        _liveImg.sprite = _livesSprite[currentLives];
        if (currentLives<1)
        {
            GameOverSquence();
        }
    }
    void UpdateLeaderboard()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            _playgames.PostScoreToLeaderboard(_score);
        }
    }
    IEnumerator GameoverFlicker()
    {
        while (true)
        {
            _gameoverText.text = "GAME OVER";      
            yield return new WaitForSeconds(0.25f);
            _gameoverText.text = "";
            yield return new WaitForSeconds(0.25f);
        }
    }
    private void GameOverSquence()
    {
        _gameoverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
        StartCoroutine(GameoverFlicker());
        UpdateLeaderboard();
    }
    public void PauseGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            _pauseAnimation.SetBool("isPaused", true);
        }
        else if (Time.timeScale== 0)
        {
            _pauseAnimation.SetBool("isPaused", false);
            Time.timeScale = 1;
        }  
    }
    public void ToMainMenu()
    {
        int interstitialNum = Random.Range(0,2);
        if (interstitialNum==0)
        {
            showAd.Invoke();
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
