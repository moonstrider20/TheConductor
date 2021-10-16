using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu_Controller : MonoBehaviour
{

    // "MenuSelector" is an int that selects which menu the player is on.
    private int MenuSelector = 0;

    [Tooltip("'MainMenuCanvas' is the Canvas for the Main Menu. 'CreditsMenuCanvas' is the Canvas for the Credits Menu inside of the Main Menu.")]
    public Canvas MainMenuCanvas, CreditsMenuCanvas, SettingsMenuCanvas;

    void Start()
    {
        // DO NOT REMOVE THESE TWO LINES OF CODE UNDER ANY CIRCUMSTANCE, THEY APPEAR TO BE THE ONLY WAY TO HAVE THE MAIN MENU BUTTONS SHOW ON APP START FOR SOME REASON. I DO NOT KNOW WHY.
        MainMenuCanvas.enabled = false;
        MainMenuCanvas.enabled = true;
        SwitchCanvas();
    }

    // The StartGame() function loads the "Game" scene, which should be the actual game content.
    public void StartGame()
    {
        SceneManager.LoadScene("Fight_Scene_1_Android_Version");
    }

    // The Credits() function loads the "Credits" scene, which should contain the instructions for how to play the game.
    public void Credits()
    {
        MenuSelector = 1;
        SwitchCanvas();
    }

    // The MainMenu() function returns the user to the main menu.
    public void MainMenu()
    {
        MenuSelector = 0;
        SwitchCanvas();
    }

    // The Settings() function opens the "Settings" canvas.
    public void Settings()
    {
        MenuSelector = 2;
        SwitchCanvas();
    }

    public void SwitchCanvas()
    {
        switch( MenuSelector )
        {
            case 1:
                MainMenuCanvas.enabled = false;
                SettingsMenuCanvas.enabled = false;
                CreditsMenuCanvas.enabled = true;
                break;

            case 2:
                MainMenuCanvas.enabled = false;
                SettingsMenuCanvas.enabled = true;
                CreditsMenuCanvas.enabled = false;
                break;

            default:
                MainMenuCanvas.enabled = true;
                SettingsMenuCanvas.enabled = false;
                CreditsMenuCanvas.enabled = false;
                break;
        }
    }
}