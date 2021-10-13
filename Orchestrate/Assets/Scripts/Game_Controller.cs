using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Game_Controller : MonoBehaviour
{
    public Slider HealthBar;

    public Image HealthBarFill;

    public Gradient HealthBarGradient;
    public int Health;

    public GameObject Note;

    public Transform NoteSpawn1, NoteSpawn2, NoteSpawn3, NoteSpawn4, NoteSpawn5;

    public GameObject RestartButton;

    public TextMeshProUGUI SuccessfulText;

    [Tooltip("This is \"Beats Per Minute\" (or BPM), essentially this is how many notes are spawned per minute.")]
    public int BPM;

    void Awake()
    {
        HealthBar.maxValue = Health;
        HealthBar.value = Health;
        HealthBarFill.color = HealthBarGradient.Evaluate(1f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnNotes()
    {
        yield return new WaitForSeconds(BPM / 60);

        int randomNumber = Random.Range(1, 5);

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

        SpawnNotes();
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
            SuccessfulText.text = "You lost. Restart?";
            RestartButton.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("PC_Version");
    }
}
