using System.Collections;
using UnityEngine;

public class Hitter_Controller : MonoBehaviour
{
    [HideInInspector]
    public GameObject NextNote = null;
    
    // This is the game object's Sprite Renderer component.
    private SpriteRenderer Sprite_Renderer;

    [Tooltip("This is all of the possible states of the hitter, e.g. \"inactive\" and \"active\".")]
    public Sprite[] Hitter_States;

    private Game_Controller Game_Controller;

    void Start()
    {
        Sprite_Renderer = GetComponent<SpriteRenderer>();
        Game_Controller = GameObject.Find("GameController").GetComponent<Game_Controller>();
    }

    void Update()
    {
        Vector3 worldPosition;
        float x = gameObject.transform.position.x, y = gameObject.transform.position.y, z = gameObject.transform.position.z;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 position = touch.position;

            worldPosition = new Vector3(0, 0, 0);

            for (int i = 0; i < Input.touchCount; i++)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    worldPosition = Camera.main.ScreenToWorldPoint(position);
                }
            }

            if ( ( (worldPosition.x > x - 1f) && (worldPosition.x < x + 1f) ) && ( (worldPosition.y > y - 1f) && (worldPosition.y < y + 1f) ) )
            {
                SwitchStates(true);
                DestroyOther();
            }
        }
        else
        {
            SwitchStates(false);
            if (Input.GetMouseButtonDown(0))
            {
                worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                worldPosition = new Vector3(0, 0, 0);
            }
            
            if ( ( (worldPosition.x > x - 1f) && (worldPosition.x < x + 1f) ) && ( (worldPosition.y > y - 1f) && (worldPosition.y < y + 1f) ) )
            {
                DestroyOther();
            }
        }
    }

    private void SwitchStates(bool value)
    {
        switch(value)
        {
            case false:
                Sprite_Renderer.sprite = Hitter_States[0];
                break;
            default:
                Sprite_Renderer.sprite = Hitter_States[1];
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.gameObject.tag != "Steam")
            NextNote = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null)
            NextNote = null;
    }

    public void DestroyOther()
    {
        if (NextNote != null && NextNote.gameObject.tag != "Steam" && NextNote.GetComponent<Note_Controller>().GetCanBeDestroyed())
        {
            NextNote.GetComponent<Note_Controller>().DestroyThisObject();
        }
        else if ( NextNote == null )
        {
            switch ( Game_Controller.Level_Type )
            {
                case 1:
                case 3:
                    Game_Controller.ChangeScore(-1 * ( Game_Controller.NoteScoreValue / 2 ) );
                    Game_Controller.PlayMissSFX();
                    break;
                default:
                    if (Game_Controller.TimeRemaining <= Game_Controller.MaxTime)
                        Game_Controller.TimeRemaining += 1f;
                    Game_Controller.PlayMissSFX();
                    break;
            }
        }
    }
}
