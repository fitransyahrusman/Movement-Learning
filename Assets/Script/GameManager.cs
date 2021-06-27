using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using GoogleMobileAds.Placement;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameover = false;
    [SerializeField]
    public InterstitialAdGameObject interstitial;
    public UnityEvent keydown;

    private void Start()
    {
        if (keydown == null)
        {
            keydown = new UnityEvent();
        }
        MobileAds.Initialize((success) => { });
        interstitial = MobileAds.Instance.GetAd<InterstitialAdGameObject>("Interstitial Ad");
        interstitial.LoadAd();
    }
    void Update()
    {
        if ( Input.anyKeyDown && _isGameover == true)
        {
            int interstitialNum = Random.Range(0, 2);
            if (interstitialNum == 0 )
            {
                keydown.Invoke();
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }   
    public void GameOver()
    {
        Debug.Log("GameManager::GameOver() Called");
        _isGameover = true;
    }
    public void Scene0()
    {
        SceneManager.LoadScene(0);
    }
}
