using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Code created by Christian Babcock and modified by Seth Grimes for DIG4715C Casual Game Production. October 23rd, 2021.

*/

public class Steam_Screen : MonoBehaviour
{
    private Game_Controller GameController;

    public float SteamScreenDuration;

    void Start()
    {
        GameController = GameObject.Find("GameController").GetComponent<Game_Controller>();
    }

    private void Update()
    {
        if (GameController.WinConditionAchieved || GameController.Health <= 0)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) //Make sure to put this out of Voids
    {
        if ( other != null && other.gameObject.tag == "Steam" )
        {
            Destroy(other.gameObject);
            Destroy(Instantiate(GameController.SteamScreen), SteamScreenDuration);
        }      
    }
}