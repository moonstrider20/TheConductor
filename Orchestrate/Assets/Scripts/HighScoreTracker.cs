using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class HighScoreTracker : MonoBehaviour
{
    private List<int> HighScores = new List<int>{0};
    private Game_Controller GameController = null;

    private Main_Menu_Controller MainMenuController;
    private TextMeshProUGUI HighScoresListText;

    private bool AlreadyAddedToList = false;
    
    public bool Level1Completed = false, Level2Completed = false;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        HighScores.Remove(HighScores[0]);
    }

    void Update()
    {
        if ( GameController != null )
        {
            if ( ( GameController.WinConditionAchieved || GameController.Health <= 0 ) && !GameController.AlreadyWonOrLost && !AlreadyAddedToList)
            {
                if ( GameController.WinConditionAchieved )
                {
                    if ( SceneManager.GetActiveScene().name == "Score_Based_Level" && GameController.Accuracy >= 80 )
                        Level1Completed = true;
                    else if ( SceneManager.GetActiveScene().name == "Time_Based_Level" && GameController.Accuracy >= 80 )
                        Level2Completed = true;
                }

                HighScores.Add(GameController.Score);
                HighScores.Sort();
                AlreadyAddedToList = true;
            }
            else if (AlreadyAddedToList)
            {
                StartCoroutine("WaitABitAndThenResetAlreadyAddedToList");
            }
        }

        if ( SceneManager.GetActiveScene().name == "HighScoreTracker" )
            SceneManager.LoadScene("Main_Menu");

        if (SceneManager.GetActiveScene().name == "Main_Menu")
        {
            MainMenuController = GameObject.Find("MainMenuController").GetComponent<Main_Menu_Controller>();
            HighScoresListText = GameObject.Find("High_Scores_List_Text").GetComponent<TextMeshProUGUI>();
            if (HighScores.Count > 0)
                HighScoresListText.text = ($"1. {HighScores[HighScores.Count - 1].ToString()}");

            for ( int i = HighScores.Count - 1, index = 1; i >= 0; i--, index++ )
            {
                if ( i != HighScores.Count - 1 )                
                    HighScoresListText.text = ($"{HighScoresListText.text}\n{index.ToString()}. {HighScores[i].ToString()}");
            }
        }
        else if (SceneManager.GetActiveScene().name == "Map_Scene")
        {
            
        }
        else if (SceneManager.GetActiveScene().name != "HighScoreTracker")
        {
            GameController = GameObject.Find("GameController").GetComponent<Game_Controller>();
        }
    }

    private IEnumerator WaitABitAndThenResetAlreadyAddedToList()
    {
        yield return new WaitForSeconds(0.0001f);

        AlreadyAddedToList = false;

        yield return null;
    }
}
