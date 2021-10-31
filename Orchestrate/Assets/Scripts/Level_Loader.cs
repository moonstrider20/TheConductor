using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Loader : MonoBehaviour
{
    public void LoadScoreLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadTimedLevel()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadEndlessLevel()
    {
        SceneManager.LoadScene(4);
    }
}
