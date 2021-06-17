using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AdMob _adMob;
    private void Start()
    {
        _adMob = GameObject.Find("Ads_Manager").GetComponent<AdMob>();
    }
   
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }


}
