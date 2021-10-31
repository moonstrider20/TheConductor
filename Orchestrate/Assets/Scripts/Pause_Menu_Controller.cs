using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Code created by Seth Grimes for Casual Game Production. October 30th, 2021.

*/

public class Pause_Menu_Controller : MonoBehaviour
{
    public Canvas PauseMenuCanvasParent, MainCanvasParent;

    void Start()
    {
        PauseMenuCanvasParent.enabled = false;
    }

    public void HandlePausing()
    {
        switch ( Time.timeScale )
        {
            case 1:
                PauseMenuCanvasParent.enabled = true;
                MainCanvasParent.enabled = false;
                Time.timeScale = 0;
                break;
            default:
                PauseMenuCanvasParent.enabled = false;
                MainCanvasParent.enabled = true;
                Time.timeScale = 1;
                break;
        }
    }
}
