using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hitter_Controller : MonoBehaviour
{
    public GameObject NextNote = null;

    public TextMeshProUGUI DebugText;

    void Start()
    {
        DebugText.text = "";
    }

    void Update()
    {
        GameObject temp = GameObject.FindWithTag("Note");

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            DebugText.text = "Touching the screen";

            Vector2 position = touch.position;

            // Cast a ray straight down.
            RaycastHit2D hit = Physics2D.Raycast(position, -Vector2.up);
            
            // If it hits something...
            if (hit.collider != null)
            
            {
                //Debug.Log("Hit a 2D object");
                DebugText.text = "Raycast hit a 2D object";

                // Calculate the distance from the surface and the "error" relative
                // to the floating height.
                //float distance = Mathf.Abs(hit.point.y - transform.position.y);
                //float heightError = floatHeight - distance;
                
                // The force is proportional to the height error, but we remove a part of it
                // according to the object's speed.
                //float force = liftForce * heightError - rb2D.velocity.y * damping;
                
                // Apply the force to the rigidbody.
                //rb2D.AddForce(Vector3.up * force);
            }

            /*
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 position = touch.position;
            }
            */

            

        }
        else
        {
            DebugText.text = "Not touching the screen";
        }

        // Debug.Log($"Is temp null? {temp == null}");

        if (temp != null)
        {
            NextNote = temp;
            // Debug.Log($"Is NextNote null? {NextNote == null}");
            // Debug.Log($"This's Game Object: {this.gameObject}");
        }
        else
        {
            NextNote = null;
        }
    }

    public void DestroyOther()
    {
        Debug.Log(NextNote.GetComponent<Note_Controller>().GetCanBeDestroyed());
        if (NextNote.GetComponent<Note_Controller>().GetCanBeDestroyed())
        {
            NextNote.GetComponent<Note_Controller>().DestroyThisObject();
        }
    }
}
