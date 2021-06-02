using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private LeaderboardManagerMenu _leaderboardMenu;
    private void Start()
    {
        _leaderboardMenu = GameObject.Find("Leaderboard_Manager_Menu").GetComponent<LeaderboardManagerMenu>();
        if (_leaderboardMenu==null)
        {
            Debug.LogError("LeaderboardManagerMenu failed from Main Menu");
        }
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
    public void ShowLeaderboard()
    {
        LeaderboardManagerMenu.instance.ShowLeaderboardMenu();
    }
}
