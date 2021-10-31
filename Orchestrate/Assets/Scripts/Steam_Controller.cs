using UnityEngine;

/*

Code created by Christian Babcock for DIG4715C Casual Game Production. October 22nd, 2021.

*/

public class Steam_Controller : MonoBehaviour
{
    Vector2 startPos, endPos, direction;

    Vector3 worldPosition;

    float touchTimeStart, touchTimeFinish;
    public float throwForce = 1f;

    void Update()
    {
        if (GameObject.Find("GameController").GetComponent<Game_Controller>().WinConditionAchieved || GameObject.Find("GameController").GetComponent<Game_Controller>().Health <= 0)
        {
            Destroy(gameObject);
        }
        if (Input.GetMouseButtonDown(0))
        {
            worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            worldPosition = new Vector3(0, 0, 0);
        }
        
        if ( Input.touchCount > 0 )
        
        {
            Touch touch = Input.GetTouch(0);

            Vector2 position = touch.position;

            for (int i = 0; i < Input.touchCount; i++)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    worldPosition = Camera.main.ScreenToWorldPoint(position);
                }
            }
        }

        float gameObjectX = Mathf.Abs(gameObject.transform.position.x), gameObjectY = Mathf.Abs(gameObject.transform.position.y), worldX = Mathf.Abs(worldPosition.x), worldY = Mathf.Abs(worldPosition.y);

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began  )
        {
            touchTimeStart = Time.time;
            startPos = Input.GetTouch(0).position;
        }        

        // Failed code for trying to make the Steam only swipable from near the Steam object
        // && ( (worldX >= ( gameObjectX - 0.7f ) ) && (worldX <= ( gameObjectX + 0.7f ) ) ) && ( (worldY >= ( gameObjectY - 16f ) ) && (worldY <= ( gameObjectY + 1f ) ) )
        
        if (Time.timeScale == 1 && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            touchTimeFinish = Time.time;

            endPos = Input.GetTouch(0).position;

            direction = startPos - endPos;

            GetComponent<Rigidbody2D>().AddForce(-direction * throwForce);
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("GameBoundry") || other.gameObject.CompareTag("SmokeBoundry"))
        {
            Destroy(gameObject);
        }
    }
}
