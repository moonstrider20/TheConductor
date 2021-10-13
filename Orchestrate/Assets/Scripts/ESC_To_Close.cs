using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESC_To_Close : MonoBehaviour
{    
    void Update()
    {
        if (Input.GetButtonDown("ESC"))
        {
            Application.Quit();
        }
    }
}
