using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _besTScoreText;
    private int _score;
    private int _bestScore;
    [SerializeField]
    private Sprite[] _livesSprite;
    [SerializeField]
    private Image _liveImg;
    [SerializeField]
    private Text _gameoverText;
    [SerializeField]
    private Text _restartText;
    private GameManager _gameManager;
    [SerializeField]
    private GameObject _pausePanel;
    
    
       
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

        _bestScore = PlayerPrefs.GetInt("Highscore", 0);
        _besTScoreText.text = "Best score : " + _bestScore;

    }
    private void Update()
    {
        
           
        
    }
    public void UpdateScore (int playerScore)
    {
        _score = playerScore;
        _scoreText.text = "Score is : " + _score;
    }
    public void CheckForBestScore()
    {
        if (_score > _bestScore)
        {
            _bestScore = _score;
            _besTScoreText.text = "Best score : " + _bestScore;
            PlayerPrefs.SetInt("Highscore", _bestScore);
        }
    }

    public void UpdateLives(int currentLives)
    {
        _liveImg.sprite = _livesSprite[currentLives];
        if (currentLives<1)
        {
            GameOverSquence();
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
    void GameOverSquence()
    {
        _gameoverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
        StartCoroutine(GameoverFlicker());
        CheckForBestScore();
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
