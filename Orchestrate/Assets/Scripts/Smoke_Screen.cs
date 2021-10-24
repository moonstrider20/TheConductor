using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Code created by Christian Babcock and modified by Seth Grimes for DIG4715C Casual Game Production. October 23rd, 2021.

*/

public class Smoke_Screen : MonoBehaviour
{
    private Game_Controller GameController;
    public float smokeScreenTimer = 0;
    public float maxSmokeScreenTime = 5;

    public float smokeScreenDuration = 3f;

    public bool smokeScreenState = false;

    // Start is called before the first frame update
    void Start()
    {
        GameController = GameObject.Find("GameController").GetComponent<Game_Controller>();
    }

    private void Update()
    {
        if (smokeScreenState == true)
            smokeScreenTimer += Time.deltaTime;

        if(smokeScreenTimer > smokeScreenDuration)
        {
            GameController.SmokeScreen.SetActive(false);
            smokeScreenTimer = 0;
            smokeScreenState = false;
        }
            
    }

    void OnTriggerEnter2D(Collider2D other) //Make sure to put this out of Voids
    {
        if (other.gameObject.tag == "Smoke")
        {
            Debug.Log("Collided");
            Destroy(other.gameObject);
            GameController.SmokeScreen.SetActive(true);
            smokeScreenState = true;
        }      
    }

}
