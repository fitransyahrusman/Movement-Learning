using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameover = false;
    void Update()
    {
        if ( Input.anyKeyDown && _isGameover == true)
        {
            SceneManager.LoadScene(0);
        }
    }   
    public void GameOver()
    {
        Debug.Log("GameManager::GameOver() Called");
        _isGameover = true;
    }
}
