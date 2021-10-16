using UnityEngine;

public class Hitter_Controller : MonoBehaviour
{
    public GameObject NextNote = null;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            Vector2 position = touch.position;

            Vector3 worldPosition = new Vector3(0, 0, 0);

            for (int i = 0; i < Input.touchCount; i++)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    worldPosition = Camera.main.ScreenToWorldPoint(position);
                }
            }

            float x = gameObject.transform.position.x, y = gameObject.transform.position.y, z = gameObject.transform.position.z;

            if ( ( (worldPosition.x > x - 1f) && (worldPosition.x < x + 1f) ) && ( (worldPosition.y > y - 1f) && (worldPosition.y < y + 1f) ) )
            {
                DestroyOther();
            }
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
