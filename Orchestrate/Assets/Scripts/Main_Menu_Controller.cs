using UnityEngine;
using UnityEngine.SceneManagement;

/*

Code created by Seth Grimes for DIG4720C - Casual Game Production. October 9th, 2021.

*/

public class Main_Menu_Controller : MonoBehaviour
{
    // "MenuSelector" is an int that selects which menu the player is on.
    private int MenuSelector = 0;

    [Tooltip("'MainMenuCanvas' is the Canvas for the Main Menu. 'CreditsMenuCanvas' is the Canvas for the Credits Menu inside of the Main Menu.")]
    public Canvas MainMenuCanvas, CreditsMenuCanvas, SettingsMenuCanvas, StartMenuCanvas;
    private AudioSource ButtonClickSFX;
    private bool GameJustStarted = true;

    void Start()
    {
        // DO NOT REMOVE THESE TWO LINES OF CODE UNDER ANY CIRCUMSTANCE, THEY APPEAR TO BE THE ONLY WAY TO HAVE THE MAIN MENU BUTTONS SHOW ON APP START FOR SOME REASON. I DO NOT KNOW WHY.
        MainMenuCanvas.enabled = false;
        MainMenuCanvas.enabled = true;
        ButtonClickSFX = GameObject.Find("ButtonClickSFX").GetComponent<AudioSource>();
        SwitchCanvas();
        GameJustStarted = false;
    }

    // The Score() function loads the "Score" scene, which is the scored version of the game.
    public void Score()
    {
        ButtonClickSFX.Play();
        SceneManager.LoadScene(1);
    }

    // The Timed() function loads the "Timed" scene, which is the timed version of the game.
    public void Timed()
    {
        ButtonClickSFX.Play();
        SceneManager.LoadScene(2);
    }

    // The Endless() function loads the "Endless" scene, which is the endless version of the game.
    public void Endless()
    {
        ButtonClickSFX.Play();
        SceneManager.LoadScene(3);
    }

    // The MainMenu() function returns the user to the main menu.
    public void MainMenu()
    {
        MenuSelector = 0;
        SwitchCanvas();
    }

    // The Credits() function loads the "Credits" scene, which should contain the instructions for how to play the game.
    public void Credits()
    {
        MenuSelector = 1;
        SwitchCanvas();
    }

    // The Settings() function opens the "Settings" canvas.
    public void Settings()
    {
        MenuSelector = 2;
        SwitchCanvas();
    }
    public void StartMenu()
    {
        MenuSelector = 3;
        SwitchCanvas();
    }

    public void SwitchCanvas()
    {
        if (!GameJustStarted)
        {
            ButtonClickSFX.Play();
        }

        switch( MenuSelector )
        {
            case 1:
                MainMenuCanvas.enabled = false;
                SettingsMenuCanvas.enabled = false;
                CreditsMenuCanvas.enabled = true;
                StartMenuCanvas.enabled = false;
                break;

            case 2:
                MainMenuCanvas.enabled = false;
                SettingsMenuCanvas.enabled = true;
                CreditsMenuCanvas.enabled = false;
                StartMenuCanvas.enabled = false;
                break;

            case 3:
                MainMenuCanvas.enabled = false;
                SettingsMenuCanvas.enabled = false;
                CreditsMenuCanvas.enabled = false;
                StartMenuCanvas.enabled = true;
                break;

            default:
                MainMenuCanvas.enabled = true;
                SettingsMenuCanvas.enabled = false;
                CreditsMenuCanvas.enabled = false;
                StartMenuCanvas.enabled = false;
                break;
        }
    }
}