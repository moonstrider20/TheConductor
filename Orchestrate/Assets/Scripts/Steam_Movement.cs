using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam_Movement : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - GameObject.Find("GameController").GetComponent<Game_Controller>().NoteSpeed);
    }
}
