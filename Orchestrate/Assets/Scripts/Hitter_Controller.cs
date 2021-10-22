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

    void Start()
    {
        Sprite_Renderer = GetComponent<SpriteRenderer>();
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
        if (other != null)
            NextNote = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null)
            NextNote = null;
    }

    public void DestroyOther()
    {
        if (NextNote != null && NextNote.GetComponent<Note_Controller>().GetCanBeDestroyed())
        {
            NextNote.GetComponent<Note_Controller>().DestroyThisObject();
        }
    }
}
