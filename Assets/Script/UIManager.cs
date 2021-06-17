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
    public UnityEvent livesLess1;
    public InterstitialAdGameObject interstitial;

    void Start()
    {
        _scoreText.text = "Score is : " + 0;
        _gameoverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _pausePanel.gameObject.SetActive(false);
        
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
        if (livesLess1==null)
        {
            livesLess1 = new UnityEvent();
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
            livesLess1.Invoke();
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
    public void GameOverSquence()
    {
        _gameoverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
        StartCoroutine(GameoverFlicker());
        UpdateLeaderboard();
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        _pausePanel.gameObject.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        _pausePanel.gameObject.SetActive(false);
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
