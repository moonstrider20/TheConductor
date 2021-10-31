using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Controller : MonoBehaviour
{
    [SerializeField]
    private Transform[] waypoints;

    [SerializeField]
    [Range(2, 12)]
    private float moveSpeed = 4;

    public Animator animator;

    private int waypointIndex = 0;

    private Vector3 targetPosition;
    private bool isMoving = false;
    RaycastHit2D hit;

    public GameObject levelPopup1, levelPopup2, levelPopup3;

    private GameObject CurrentButton;

    private void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 touchPosition2D = new Vector2(touchPosition.x, touchPosition.y);
            hit = Physics2D.Raycast(touchPosition2D, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Level" && CurrentButton != hit.collider.gameObject)
                {
                    SetTargetPosition();
                }
            }
        }

        if (isMoving)
        {
            Move();
        }
    }

    void SetTargetPosition()
    {
        targetPosition = hit.collider.gameObject.transform.position;
        isMoving = true;
        animator.SetInteger("State", 1);
    }

    private void Move()
    {
        if (transform.position.y < targetPosition.y)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;

            if (waypointIndex <= waypoints.Length - 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
                
                if (transform.position == waypoints[waypointIndex].transform.position)
                {
                    if (waypoints[waypointIndex].transform.position == targetPosition)
                    {
                        isMoving = false;
                        switch ( CurrentButton.name )
                        {
                            case "Level1":
                                levelPopup1.SetActive(true);
                                break;
                            case "Level2":
                                levelPopup2.SetActive(true);
                                break;
                            default:
                                levelPopup3.SetActive(true);
                                break;
                        }
                        animator.SetInteger("State", 0);
                        return;
                    }
                    waypointIndex++;
                }
                
            }
        }        

        if (transform.position.y > targetPosition.y)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;

            if (waypointIndex > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

                if (transform.position == waypoints[waypointIndex].transform.position)
                {
                    if (waypoints[waypointIndex].transform.position == targetPosition)
                    {
                        isMoving = false;
                        switch ( CurrentButton.name )
                        {
                            case "Level1":
                                levelPopup1.SetActive(true);
                                break;
                            case "Level2":
                                levelPopup2.SetActive(true);
                                break;
                            default:
                                levelPopup3.SetActive(true);
                                break;
                        }
                        animator.SetInteger("State", 0);
                        return;
                    }
                    waypointIndex--;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( other != null )
            CurrentButton = other.gameObject;  
    }

    public bool GetIsMoving() => isMoving;
}
