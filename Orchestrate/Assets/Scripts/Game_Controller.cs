using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Game_Controller : MonoBehaviour
{
    [Tooltip("This is the actual fill that will fill up the health bar, so to say.")]
    public Image HealthBarFill;

    //This is the amount of points needed to win the level. It is randomly set.
    [HideInInspector]
    public int ScoreMaximumValue;

    [Tooltip("This is the maximum amount of how many hits the player can take. This number can be adjusted freely and the health bar should decrease / increase accordingly.")]
    public int HealthMax;

    // This is how many more hits the player can take currently.
    [HideInInspector]
    public int Health;

    // This is the player's current score.
    [HideInInspector]
    public int Score = 0;

    [Tooltip("This is the type of level that the scene is. 1 for Score, 2 for Timed, and 3 for Endless.")]
    public int Level_Type;

    [Tooltip("This is the maximum amount of time for the level. This is only useful in the \"Timed\" level.")]
    public float MaxTime;

    [Tooltip("This is how fast the notes move.")]
    public float NoteSpeed;

    [HideInInspector]
    public float TimeRemaining;

    [Tooltip("These are the possible spawn locations for the notes.")]

    public Transform NoteSpawn1, NoteSpawn2, NoteSpawn3, NoteSpawn4, NoteSpawn5;

    [Tooltip("The Restart Button is the button that will allow the player to restart the level, the \"Quit\" button will allow the player to quit out of the application, and the \"Main Menu\" button allows for quick return to the Main Menu.")]
    public GameObject RestartButton, QuitButton, Main_Menu_Button;

    [Tooltip("This is an array of the Note prefabs.")]
    public GameObject[] Notes;

    [Tooltip("\"SuccessfulText\" is the text that appears when you successfully complete a level and \"ScoreText\" is the player's current score.")]
    public TextMeshProUGUI SuccessfulText, ScoreText;

    [Tooltip("This is the sound effect for the player missing a note.")]
    private AudioSource MissSFX;

    // This is the sound effect for the player tapping a note.
    private AudioSource TapSFX;

    [Tooltip("This is \"Beats Per Minute\" (or BPM), essentially this is how many notes are spawned per minute.")]
    public int BPM;

    // This is if the win condition for the level has been achieved yet or not
    [HideInInspector]
    public bool WinConditionAchieved = false;

    void Awake()
    {
        HealthBarFill.fillAmount = 1;
        MissSFX = gameObject.GetComponent<AudioSource>();
        TapSFX = GameObject.Find("TapSFX").GetComponent<AudioSource>();
        Health = HealthMax;
        TimeRemaining = MaxTime;
        ScoreMaximumValue = Random.Range(15, 31);
        ScoreMaximumValue *= 100;
        StartCoroutine("SpawnNotes");
    }

    void Update()
    {
        TimeRemaining -= Time.deltaTime;

        if ( !WinConditionAchieved )
        {
            UpdateWinCondition();
            UpdateScoreText();
        }
        else
        {
            UpdateSuccessfulText("You win! Restart or Quit?");
        }
        
    }

    void FixedUpdate()
    {
        if ( !WinConditionAchieved && Health > 0 && Level_Type == 3 )
            NoteSpeed += 0.00004f;
    }

    public void UpdateWinCondition()
    {
        if ( Level_Type == 1 )
        {
            if ( Score >= ScoreMaximumValue )
                WinConditionAchieved = true;
        }
        else if ( Level_Type == 2 )
        {
            if ( Mathf.Round(TimeRemaining) <= 0 )
                WinConditionAchieved = true;
        }
    }

    public IEnumerator SpawnNotes()
    {
        if ( Health > 0 && !WinConditionAchieved )
        {
            
        yield return new WaitForSeconds(60 / BPM);

        int randomNumber = Random.Range(1, 6);

        switch(randomNumber)
        {
            case 1:
                Instantiate(Notes[0], NoteSpawn1);
                break;

            case 2:
                Instantiate(Notes[1], NoteSpawn2);
                break;

            case 3:
                Instantiate(Notes[2], NoteSpawn3);
                break;

            case 4:
                Instantiate(Notes[3], NoteSpawn4);
                break;


            default:
                Instantiate(Notes[4], NoteSpawn5);
                break;
        }

        if ( Health > 0 && !WinConditionAchieved )
            StartCoroutine("SpawnNotes");
        }
    }

    public void ChangeHealth(int value)
    {
        Health += value;

        if ( Health > 0 && value < 0 )
            HealthBarFill.fillAmount -= (1f / HealthMax);
        else if ( Health > 0 && value > 0 )
            HealthBarFill.fillAmount += (1f / HealthMax);
        else if ( Health == 0 )
        {
            HealthBarFill.fillAmount = 0;
            UpdateSuccessfulText("You lost. Restart or quit?");
        }
    }

    public void ChangeScore(int value)
    {
        Score += value;
        
        if (Score >= ScoreMaximumValue && Level_Type == 1)
        {
            SuccessfulText.text = "You won! Restart or quit?";
            RestartButton.SetActive(true);
            QuitButton.SetActive(true);
        }
    }

    private void UpdateSuccessfulText(string newText)
    {
        SuccessfulText.text = newText;
        Main_Menu_Button.SetActive(true);
        RestartButton.SetActive(true);
        QuitButton.SetActive(true);
    }

    private void UpdateScoreText()
    {
        switch ( Level_Type )
        {
            case 1:
                ScoreText.text = ($"Score: {Score} / {ScoreMaximumValue}");
                break;
            case 2:
                ScoreText.text = ($"Time remaining: {Mathf.Round(TimeRemaining)}");
                break;
            default:
                ScoreText.text = ($"Score: {Score}");
                break;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayMissSFX()
    {
        MissSFX.Play();
    }

    public void PlayHitSFX()
    {
        TapSFX.Play();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
