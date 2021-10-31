using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Touch_Player : MonoBehaviour
{
    private SpriteRenderer Sprite_Renderer;

    [Tooltip("These are all of the states of the button, e.g. inactive and active.")]
    public Sprite[] Button_States;

    void Start()
    {
        Sprite_Renderer = gameObject.GetComponent<SpriteRenderer>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Conductor")
        {
            SwitchStates(true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Conductor")
        {
            SwitchStates(false);
        }
    }

    private void SwitchStates(bool value)
    {
        switch(value)
        {
            case false:
                Sprite_Renderer.sprite = Button_States[0];
                break;
            default:
                Sprite_Renderer.sprite = Button_States[1];
                break;
        }
    }
}
