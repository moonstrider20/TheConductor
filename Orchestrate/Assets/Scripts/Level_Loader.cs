using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Loader : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
    public void LoadScoreLevel()
    {
        SceneManager.LoadScene("Score_Based_Level");
    }

    public void LoadTimedLevel()
    {
        SceneManager.LoadScene("Time_Based_Level");
    }

    public void LoadEndlessLevel()
    {
        SceneManager.LoadScene("Endless_Level");
    }
}
