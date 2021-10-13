using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Note_Movement : MonoBehaviour
{
    public bool canBeDestroyed = false;

    //public Collider2D OtherObject = null;

    void Start()
    {
    }
    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x - 0.05f, transform.position.y);
    }

    void Update()
    {
        if (Input.GetButtonDown("Tap") && canBeDestroyed)
        {
            Destroy(gameObject, 0.0000001f);
        }
        if (transform.position.x <= -8.74)
        {
            GameObject.Find("GameController").GetComponent<Game_Controller>().ChangeHealth(-1);
            Destroy(gameObject, 0.000001f);

        }
    }
/*
    private void OnTriggerEnter2D(Collider2D other)
    {
        canBeDestroyed = true;
        if ( other != null )
        {
            OtherObject = other;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canBeDestroyed = false;
        if ( other != null )
        {
            OtherObject = null;
        }
    }

    public void DestroyOther()
    {
        if (OtherObject != null)
        {
            Destroy(OtherObject);
        }

    }
    */
}
