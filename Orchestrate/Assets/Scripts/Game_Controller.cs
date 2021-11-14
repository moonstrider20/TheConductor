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
    public float HealthMax;

    // This is how many more hits the player can take currently.
    [HideInInspector]
    public float Health;

    // This is the player's current score.
    [HideInInspector]
    public int Score = 0;

    [Tooltip("This is whether or not the level is in the Story Mode or not.")]
    public bool IsStoryMode;

    [Tooltip("This is the type of level that the scene is. 1 for Score, 2 for Timed, and 3 for Endless.")]
    public int Level_Type;

    [Tooltip("This is how much score each note is worth.")]
    public int NoteScoreValue;

    [Tooltip("This is how much time has been added since the start of the level. This is only useful in the \"Timed\" level.")]
    [HideInInspector]
    public int TimeAdded = 0, NotesMissed = 0, SuccessfulNotes = 0;

    [Tooltip("This is the maximum amount of time for the level. This is only useful in the \"Timed\" level.")]
    public float MaxTime;

    [Tooltip("This is how fast the notes move.")]
    public float NoteSpeed;

    [HideInInspector]
    public float TimeRemaining;

    [Tooltip("These are the possible spawn locations for the notes.")]

    private Transform NoteSpawn1, NoteSpawn2, NoteSpawn3, NoteSpawn4, NoteSpawn5, SteamSpawn1, SteamSpawn2, SteamSpawn3, SteamSpawn4, SteamSpawn5, HealthNoteSpawn1, HealthNoteSpawn2, HealthNoteSpawn3, HealthNoteSpawn4, HealthNoteSpawn5;

    // The Restart Button is the button that will allow the player to restart the level, the "Quit" button will allow the player to quit out of the application, and the "Main Menu" button allows for quick return to the Main Menu."
    private GameObject RestartButton, QuitButton, Main_Menu_Button, BackButton;

    [Tooltip("This is an array of the Note prefabs.")]
    public GameObject[] Notes;

    public GameObject Steam, SteamScreen;

    [Tooltip("\"SuccessfulText\" is the text that appears when you successfully complete a level and \"ScoreText\" is the player's current score. TimeAddedText is for the amount of time added since the start of the level.")]
    private TextMeshProUGUI SuccessfulText, ScoreText, TimeAddedText, ScoreTextEndScreen, AccuracyText, NotesMissedText, LevelText, CountdownText;

    [Tooltip("This is the sound effect for the player missing a note.")]
    private AudioSource MissSFX;

    // This is the sound effect for the player tapping a note.
    private AudioSource TapSFX;

    [Tooltip("This is \"Beats Per Minute\" (or BPM), essentially this is how many notes are spawned per minute.")]
    public float BPM;

    [HideInInspector]
    [Tooltip("This is how accurate you were over the course of the song.")]
    public float Accuracy = 0f;

    // This is if the win condition for the level has been achieved yet or not
    [HideInInspector]
    public bool WinConditionAchieved = false;

    // This is the actual particle system steam that appears when the steam ball is triggered.
    private Steam_Screen Steam_Screen;

    private int randomNumber;

    private GameObject Poor_Rating, Fair_Rating, Good_Rating, Excellent_Rating, Superior_Rating, Timed_End_Screen, Score_End_Screen, NotesMissedGameObject, AccuracyGameObject, HealthGoDownParticleSystem;

    public bool AlreadyWonOrLost = false;

    private Canvas IntroduceLevelCanvas;

    private bool TimerCanDecrease = false;

    #endregion

    void Awake()
    {
        #region GameObject.Find assignments

        BackButton = GameObject.Find("Back_Button");

        SuccessfulText = GameObject.Find("SuccessfulText").GetComponent<TextMeshProUGUI>();

        ScoreText = GameObject.Find("Score_Text").GetComponent<TextMeshProUGUI>();

        TimeAddedText = GameObject.Find("Time_Added_Text").GetComponent<TextMeshProUGUI>();

        TimeAddedText.text = "";

        ScoreTextEndScreen = GameObject.Find("ScoreTextEndScreen").GetComponent<TextMeshProUGUI>();

        ScoreTextEndScreen.text = "";

        AccuracyText = GameObject.Find("Accuracy_Text").GetComponent<TextMeshProUGUI>();

        AccuracyText.text = "";

        NotesMissedText = GameObject.Find("Notes_Missed_Text").GetComponent<TextMeshProUGUI>();

        NotesMissedText.text = "";

        NotesMissedGameObject = GameObject.Find("Notes_Missed");

        AccuracyGameObject = GameObject.Find("Accuracy");

        NotesMissedGameObject.SetActive(false);

        AccuracyGameObject.SetActive(false);

        Poor_Rating = GameObject.Find("Poor_Rating");

        Fair_Rating = GameObject.Find("Fair_Rating");

        Good_Rating = GameObject.Find("Good_Rating");

        Excellent_Rating = GameObject.Find("Excellent_Rating");

        Superior_Rating = GameObject.Find("Superior_Rating");

        Timed_End_Screen = GameObject.Find("Timed_End_Screen");

        Score_End_Screen = GameObject.Find("Score_End_Screen");

        Poor_Rating.SetActive(false);

        Fair_Rating.SetActive(false);

        Good_Rating.SetActive(false);

        Excellent_Rating.SetActive(false);

        Superior_Rating.SetActive(false);

        Timed_End_Screen.SetActive(false);

        Score_End_Screen.SetActive(false);

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

        HealthNoteSpawn1 = GameObject.Find("HealthNoteSpawn1").GetComponent<Transform>();

        HealthNoteSpawn2 = GameObject.Find("HealthNoteSpawn2").GetComponent<Transform>();

        HealthNoteSpawn3 = GameObject.Find("HealthNoteSpawn3").GetComponent<Transform>();

        HealthNoteSpawn4 = GameObject.Find("HealthNoteSpawn4").GetComponent<Transform>();

        HealthNoteSpawn5 = GameObject.Find("HealthNoteSpawn5").GetComponent<Transform>();

        IntroduceLevelCanvas = GameObject.Find("IntroduceLevelCanvas").GetComponent<Canvas>();

        IntroduceLevelCanvas.enabled = false;

        LevelText = GameObject.Find("LevelText").GetComponent<TextMeshProUGUI>();

        CountdownText = GameObject.Find("CountdownText").GetComponent<TextMeshProUGUI>();

        HealthGoDownParticleSystem = GameObject.Find("Health_Loss_Particle_System");

        #endregion

        HealthGoDownParticleSystem.SetActive(false);

        RestartButton.SetActive(false);

        QuitButton.SetActive(false);

        BackButton.SetActive(false);

        HealthBarFill.fillAmount = 1;

        MissSFX = gameObject.GetComponent<AudioSource>();
        
        Health = HealthMax;

        TimeRemaining = MaxTime;

        StartCoroutine("WaitAtBeginningOfLevel");
    }

    void Update()
    {
        if ( Health < 0)
            Health = 0;

        if ( Level_Type == 2 && !WinConditionAchieved && Health > 0 && TimerCanDecrease )
            TimeRemaining -= Time.deltaTime;

        if ( Level_Type == 2 && TimeRemaining > MaxTime )
            TimeRemaining = MaxTime;

        if ( !WinConditionAchieved && Health > 0 )
        {
            UpdateWinCondition();
            UpdateScoreText();
        }
        else if ( WinConditionAchieved && Level_Type != 2 && !AlreadyWonOrLost )
        {
            StartCoroutine("DoFinalCalculations");
            Score_End_Screen.SetActive(true);
            ScoreTextEndScreen.text = Score.ToString();
        }
        else if ( WinConditionAchieved && Level_Type == 2 && !AlreadyWonOrLost )
        {
            StartCoroutine("DoFinalCalculations");
            Timed_End_Screen.SetActive(true);
            TimeAddedText.text = ($"{TimeAdded} seconds");
        }
        
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

    public IEnumerator WaitAtBeginningOfLevel()
    {
        IntroduceLevelCanvas.enabled = true;
        TimerCanDecrease = false;

        if ( SceneManager.GetActiveScene().name == "Score_Based_Level" )
            LevelText.text = ($"This is the Score-based level. Reach {ScoreMaximumValue} points to win the level and progress to the next level!");
        else if ( SceneManager.GetActiveScene().name == "Time_Based_Level" )
            LevelText.text = ($"This is the Time-based level. Survive for {MaxTime} seconds to win the level and progress to the next level!");
        else if ( SceneManager.GetActiveScene().name == "Endless_Level" )
            LevelText.text = ($"This is the Endless level. There is no end to this level, and the notes will continuously spawn until you run out of Health.");

        for (int i = 5; i > 0; i--)
        {
            CountdownText.text = ($"{i}...");
            yield return new WaitForSeconds(1);
        }

        TimerCanDecrease = true;

        IntroduceLevelCanvas.enabled = false;

        StartCoroutine("SpawnNotes");

        yield return null;
    }

    public IEnumerator SpawnNotes()
    {
        if ( Health > 0 && !WinConditionAchieved )
        {
            
        yield return new WaitForSeconds(60 / BPM);

        randomNumber = Random.Range(1, 8);
        
        int randomNumber2 = Random.Range(1, 6);

        GameObject SteamScreenClone = GameObject.Find("Steamscreen(Clone)");

        
        if ( ( GameObject.FindWithTag("HealthNote") != null && randomNumber == 7 ) || Health == HealthMax )
        {
            randomNumber = Random.Range(1, 7);
        }

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

            case 6:
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

            default:
                Debug.Log("Spawning health note");
                switch ( randomNumber2 )
                {
                    case 1:
                        Instantiate(Notes[5], HealthNoteSpawn1);
                        break;
                    case 2:
                        Instantiate(Notes[5], HealthNoteSpawn2);
                        break;
                    case 3:
                        Instantiate(Notes[5], HealthNoteSpawn3);
                        break;
                    case 4:
                        Instantiate(Notes[5], HealthNoteSpawn4);
                        break;
                    default:
                        Instantiate(Notes[5], HealthNoteSpawn5);
                        break;
                }
                break;
                
        }

        if ( Health > 0 && !WinConditionAchieved )
            StartCoroutine("SpawnNotes");
        }
    }

    void LateUpdate()
    {
        Accuracy = Mathf.Round(( 100 * ( (float) SuccessfulNotes / (float) (NotesMissed + SuccessfulNotes) ) ));
                
    }

    public IEnumerator HealthParticleEffect()
    {
        if (!HealthGoDownParticleSystem.activeInHierarchy)
        {
            if (HealthGoDownParticleSystem != null)
                HealthGoDownParticleSystem?.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            if (HealthGoDownParticleSystem != null)
                HealthGoDownParticleSystem?.SetActive(false);
        }
        yield return null;
    }

    public IEnumerator ChangeHealth(float value)
    {
        StartCoroutine("HealthParticleEffect");
        
        for (int i = 0; i < Mathf.Abs(value * 30 ); i++)
        {
            Health += value / 30;
            HealthBarFill.fillAmount = (float) Health / (float) HealthMax;
            yield return null;
        }
        Health = Mathf.Round(Health);
                    
        if ( Health <= 0 && Level_Type != 2 )
        {
            StartCoroutine("DoFinalCalculations");
            ScoreTextEndScreen.text = Score.ToString();
            Score_End_Screen.SetActive(true);
            yield return null;
        }
        else if ( Health <= 0 && Level_Type == 2 )
        {
            StartCoroutine("DoFinalCalculations");
            TimeAddedText.text = TimeAddedText.text = ($"{TimeAdded} seconds");
            Timed_End_Screen.SetActive(true);
            yield return null;
        }
        yield return null;
    }

    public IEnumerator DoFinalCalculations()
    {
        yield return new WaitForSeconds(0.001f);
        AlreadyWonOrLost = true;

        NotesMissedGameObject.SetActive(true);
        AccuracyGameObject.SetActive(true);
        RestartButton.SetActive(true);
        QuitButton.SetActive(true);
        BackButton.SetActive(true);
        if ( Health <= 0 )
            HealthBarFill.fillAmount = 0;
        AccuracyText.text = ($"{Accuracy}%");
        NotesMissedText.text = ($"{NotesMissed} / {NotesMissed + SuccessfulNotes}");
        CalculateRating();

        yield return null;
    }

    public void CalculateRating()
    {
        if ( Accuracy >= 100 )
        {
            GameObject.Find("Superior").GetComponent<AudioSource>().Play();
            Superior_Rating.SetActive(true);
        }
        else if ( Accuracy >= 90 )
        {
            GameObject.Find("Excellent").GetComponent<AudioSource>().Play();
            Excellent_Rating.SetActive(true);
        }
        else if ( Accuracy >= 80 )
        {
            GameObject.Find("Good").GetComponent<AudioSource>().Play();
            Good_Rating.SetActive(true);
        }
        else if ( Accuracy >= 70 )
        {
            GameObject.Find("Fair").GetComponent<AudioSource>().Play();
            Fair_Rating.SetActive(true);
        }
        else
        {
            GameObject.Find("Poor").GetComponent<AudioSource>().Play();
            Poor_Rating.SetActive(true);
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
        SceneManager.LoadScene("Map_Scene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
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
