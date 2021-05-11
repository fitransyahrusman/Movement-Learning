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
   
    void Start()
    {
        _scoreText.text = "Score is : " + 0;
    }

    public void UpdateScore (int playerScore)
    {
        _scoreText.text = "Score is : " + playerScore.ToString();
    }
   
    public void UpdateLives(int currentLives)
    {
        _liveImg.sprite = _livesSprite[currentLives];
    }
}
