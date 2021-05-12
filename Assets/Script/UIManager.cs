using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
   
    void Start()
    {
        _scoreText.text = "Score is : " + 0;
        _gameoverText.gameObject.SetActive(false);
    }

    public void UpdateScore (int playerScore)
    {
        _scoreText.text = "Score is : " + playerScore.ToString();
    }
   
    public void UpdateLives(int currentLives)
    {
        _liveImg.sprite = _livesSprite[currentLives];
        if (currentLives<1)
        {
            _gameoverText.gameObject.SetActive(true);
            StartCoroutine(GameoverFlicker());
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
}
