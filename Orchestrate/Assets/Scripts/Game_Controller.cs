using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Game_Controller : MonoBehaviour
{
    #region Global Variable declarations

    // This is the actual fill that will fill up the health bar, so to say.
    private Image HealthBarFill;

    //This is the amount of points needed to win the level.
    public int ScoreMaximumValue;

    [Tooltip("This is the maximum amount of how many hits the player can take. This number can be adjusted freely and the health bar should decrease / increase accordingly.")]
    public int HealthMax;

    // This is how many more hits the player can take currently.
    [HideInInspector]
    public int Health;

    // This is the player's current score.
    [HideInInspector]
    public int Score = 0;

    [Tooltip("This is whether or not the level is in the Story Mode or not.")]
    public bool IsStoryMode;

    [Tooltip("This is the type of level that the scene is. 1 for Score, 2 for Timed, and 3 for Endless.")]
    public int Level_Type;

    [Tooltip("This is how much score each note is worth.")]
    public int NoteScoreValue;

    [Tooltip("This is the maximum amount of time for the level. This is only useful in the \"Timed\" level.")]
    public float MaxTime;

    [Tooltip("This is how fast the notes move.")]
    public float NoteSpeed;

    [HideInInspector]
    public float TimeRemaining;

    [Tooltip("These are the possible spawn locations for the notes.")]

    private Transform NoteSpawn1, NoteSpawn2, NoteSpawn3, NoteSpawn4, NoteSpawn5, SteamSpawn1, SteamSpawn2, SteamSpawn3, SteamSpawn4, SteamSpawn5;

    // The Restart Button is the button that will allow the player to restart the level, the "Quit" button will allow the player to quit out of the application, and the "Main Menu" button allows for quick return to the Main Menu."
    private GameObject RestartButton, QuitButton, Main_Menu_Button, BackButton;

    [Tooltip("This is an array of the Note prefabs.")]
    public GameObject[] Notes;

    public GameObject Steam, SteamScreen;

    [Tooltip("\"SuccessfulText\" is the text that appears when you successfully complete a level and \"ScoreText\" is the player's current score.")]
    private TextMeshProUGUI SuccessfulText, ScoreText;

    [Tooltip("This is the sound effect for the player missing a note.")]
    private AudioSource MissSFX;

    // This is the sound effect for the player tapping a note.
    private AudioSource TapSFX;

    [Tooltip("This is \"Beats Per Minute\" (or BPM), essentially this is how many notes are spawned per minute.")]
    public float BPM;

    // This is if the win condition for the level has been achieved yet or not
    [HideInInspector]
    public bool WinConditionAchieved = false;

    private Steam_Screen Steam_Screen;

    private int randomNumber;

    #endregion

    void Awake()
    {
        #region GameObject.Find assignments

        BackButton = GameObject.Find("Back_Button");

        SuccessfulText = GameObject.Find("SuccessfulText").GetComponent<TextMeshProUGUI>();

        ScoreText = GameObject.Find("Score_Text").GetComponent<TextMeshProUGUI>();

        Main_Menu_Button = GameObject.Find("Main_Menu_Button");

        RestartButton = GameObject.Find("Restart_Button");

        QuitButton = GameObject.Find("Quit_Button");

        HealthBarFill = GameObject.Find("HealthBarFill").GetComponent<Image>();

        Steam_Screen = GameObject.Find("Bottom_Screen_Boundry").GetComponent<Steam_Screen>();

        NoteSpawn1 = GameObject.Find("NoteSpawn1").GetComponent<Transform>();

        NoteSpawn2 = GameObject.Find("NoteSpawn2").GetComponent<Transform>();

        NoteSpawn3 = GameObject.Find("NoteSpawn3").GetComponent<Transform>();

        NoteSpawn4 = GameObject.Find("NoteSpawn4").GetComponent<Transform>();

        NoteSpawn5 = GameObject.Find("NoteSpawn5").GetComponent<Transform>();

        SteamSpawn1 = GameObject.Find("SteamSpawn1").GetComponent<Transform>();

        SteamSpawn2 = GameObject.Find("SteamSpawn2").GetComponent<Transform>();

        SteamSpawn3 = GameObject.Find("SteamSpawn3").GetComponent<Transform>();

        SteamSpawn4 = GameObject.Find("SteamSpawn4").GetComponent<Transform>();

        SteamSpawn5 = GameObject.Find("SteamSpawn5").GetComponent<Transform>();

        #endregion

        RestartButton.SetActive(false);

        QuitButton.SetActive(false);

        BackButton.SetActive(false);

        HealthBarFill.fillAmount = 1;

        MissSFX = gameObject.GetComponent<AudioSource>();
        
        Health = HealthMax;

        TimeRemaining = MaxTime;
        
        StartCoroutine("SpawnNotes");
    }

    void Update()
    {
        if ( Level_Type == 2 && !WinConditionAchieved && Health > 0 )
            TimeRemaining -= Time.deltaTime;
        //else
            //ScoreText.text = "";

        if ( Level_Type == 2 && TimeRemaining > MaxTime )
            TimeRemaining = MaxTime;

        if ( !WinConditionAchieved && Health > 0 )
        {
            UpdateWinCondition();
            UpdateScoreText();
        }
        else if ( WinConditionAchieved )
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

        randomNumber = Random.Range(1, 7);
        
        int randomNumber2 = Random.Range(1, 6);

        GameObject SteamScreenClone = GameObject.Find("Steamscreen(Clone)");

        // This ensures that the game doesn't spawn two Steam objects at the same time in the world or that the Steam screen is currently active.
        if ( ( GameObject.FindWithTag("Steam") != null && randomNumber == 6 ) || ( (SteamScreenClone != null && SteamScreenClone.activeInHierarchy) && randomNumber == 6 ) )
            randomNumber = Random.Range(1, 6);

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

            case 5:
                Instantiate(Notes[4], NoteSpawn5);
                break;

            default:
                switch ( randomNumber2 )
                {
                    case 1:
                        Instantiate(Steam, SteamSpawn1);
                        break;
                    case 2:
                        Instantiate(Steam, SteamSpawn2);
                        break;
                    case 3:
                        Instantiate(Steam, SteamSpawn3);
                        break;
                    case 4:
                        Instantiate(Steam, SteamSpawn4);
                        break;
                    default:
                        Instantiate(Steam, SteamSpawn5);
                        break;
                }
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
            UpdateSuccessfulText("You lost. Restart, Return to Map, or Quit?");
        }
    }

    public void ChangeScore(int value)
    {
        if ( Score > 0 && value < 0 )
            Score -= Mathf.Abs(value);
        else if ( Score >= 0 && value > 0 )
            Score += value;
        
        if (Score >= ScoreMaximumValue && Level_Type == 1)
        {
            SuccessfulText.text = "You won! Restart or quit?";
            RestartButton.SetActive(true);
            QuitButton.SetActive(true);
            BackButton.SetActive(true);
        }
    }

    private void UpdateSuccessfulText(string newText)
    {
        SuccessfulText.text = newText;
        RestartButton.SetActive(true);
        QuitButton.SetActive(true);
        BackButton.SetActive(true);
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

    public void Back()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void PlayMissSFX()
    {
        MissSFX.Play();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
