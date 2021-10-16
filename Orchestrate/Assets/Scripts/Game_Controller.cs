using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Game_Controller : MonoBehaviour
{
    public Slider HealthBar;

    public Image HealthBarFill;

    public Gradient HealthBarGradient;

    [Tooltip("Health is how many times the player can miss a note and ScoreMaximumValue is the amount of points needed to win the level.")]
    public int Health, ScoreMaximumValue;

    [HideInInspector]
    public int Score = 0;

    public GameObject Note;

    public Transform NoteSpawn1, NoteSpawn2, NoteSpawn3, NoteSpawn4, NoteSpawn5;

    public GameObject RestartButton, QuitButton;

    public TextMeshProUGUI SuccessfulText, ScoreText;

    private AudioSource MissSFX;

    [Tooltip("This is \"Beats Per Minute\" (or BPM), essentially this is how many notes are spawned per minute.")]
    public int BPM;

    void Awake()
    {
        HealthBar.maxValue = Health;
        HealthBar.value = Health;
        HealthBarFill.color = HealthBarGradient.Evaluate(1f);
        MissSFX = gameObject.GetComponent<AudioSource>();
        StartCoroutine("SpawnNotes");
    }

    public IEnumerator SpawnNotes()
    {
        if ( Health > 0 && Score < ScoreMaximumValue)
        {
            
        yield return new WaitForSeconds(60 / BPM);

        int randomNumber = Random.Range(1, 6);

        switch(randomNumber)
        {
            case 1:
                Instantiate(Note, NoteSpawn1);
                break;

            case 2:
                Instantiate(Note, NoteSpawn2);
                break;

            case 3:
                Instantiate(Note, NoteSpawn3);
                break;

            case 4:
                Instantiate(Note, NoteSpawn4);
                break;


            default:
                Instantiate(Note, NoteSpawn5);
                break;
        }

        if (Health > 0 && Score < ScoreMaximumValue)
            StartCoroutine("SpawnNotes");
        }
    }

    public void ChangeHealth(int value)
    {
        Health += value;

        if ( Health > 0 )
        {
            HealthBar.value = Health;
            HealthBarFill.color = HealthBarGradient.Evaluate(HealthBar.normalizedValue);
        }
        else if ( Health == 0 )
        {
            HealthBar.value = 0;
            HealthBarFill.color = HealthBarGradient.Evaluate(HealthBar.normalizedValue);
            SuccessfulText.text = "You lost. Restart or quit?";
            RestartButton.SetActive(true);
            QuitButton.SetActive(true);
        }
    }

    public void ChangeScore(int value)
    {
        Score += value;
        ScoreText.text = ($"Score: {Score}");
        if (Score >= 1000)
        {
            SuccessfulText.text = "You won! Restart or quit?";
            RestartButton.SetActive(true);
            QuitButton.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
