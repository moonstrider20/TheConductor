using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Game_Controller : MonoBehaviour
{
    [Tooltip("This is the actual fill that will fill up the health bar, so to say.")]
    public Image HealthBarFill;

    [Tooltip("The amount of points needed to win the level.")]
    public int ScoreMaximumValue;

    [Tooltip("This is the maximum amount of how many hits the player can take. This number can be adjusted freely and the health bar should decrease / increase accordingly.")]
    public int HealthMax;

    // This is how many more hits the player can take currently.
    [HideInInspector]
    public int Health;

    // This is the player's current score.
    [HideInInspector]
    public int Score = 0;

    [Tooltip("These are the possible spawn locations for the notes.")]

    public Transform NoteSpawn1, NoteSpawn2, NoteSpawn3, NoteSpawn4, NoteSpawn5;

    [Tooltip("The Restart Button is the button that will allow the player to restart the level and the \"Quit\" button will allow the player to quit out of the application.")]
    public GameObject RestartButton, QuitButton;

    [Tooltip("This is an array of the Note prefabs. It MUST contain notes in the same order as the cogs. You can change the location of the cogs around all you like, but if the first cog is green, then the first object in this array must be the Green Note prefab. If the second cog is red, then the second object in this array must be the Red Note prefab. Etc.")]
    public GameObject[] Notes;

    [Tooltip("\"SuccessfulText\" is the text that appears when you successfully complete a level and \"ScoreText\" is the player's current score.")]
    public TextMeshProUGUI SuccessfulText, ScoreText;

    [Tooltip("This is the sound effect for the player missing a note.")]
    private AudioSource MissSFX;

    // This is the sound effect for the player tapping a note.
    private AudioSource TapSFX;

    [Tooltip("This is \"Beats Per Minute\" (or BPM), essentially this is how many notes are spawned per minute.")]
    public int BPM;

    void Awake()
    {
        HealthBarFill.fillAmount = 1;
        MissSFX = gameObject.GetComponent<AudioSource>();
        TapSFX = GameObject.Find("TapSFX").GetComponent<AudioSource>();
        Health = HealthMax;
        StartCoroutine("SpawnNotes");
    }

    public IEnumerator SpawnNotes()
    {
        if ( Health > 0 && Score < ScoreMaximumValue)
        {
            
        yield return new WaitForSeconds(60 / BPM);

        int randomNumber = Random.Range(1, 6);

        Vector3 tempVector;
        Quaternion tempQuaternion = new Quaternion (0f, 0f, 0f, 0f);

        switch(randomNumber)
        {
            case 1:
                tempVector = new Vector3(NoteSpawn1.position.x - 0.0625f, NoteSpawn1.position.y, NoteSpawn1.position.z);
                Instantiate(Notes[0], tempVector, tempQuaternion);
                break;

            case 2:
                tempVector = new Vector3(NoteSpawn2.position.x - 0.0625f, NoteSpawn2.position.y, NoteSpawn2.position.z);
                Instantiate(Notes[1], tempVector, tempQuaternion);
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

        if (Health > 0 && Score < ScoreMaximumValue)
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

    public void PlayHitSFX()
    {
        TapSFX.Play();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
