using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Controller : MonoBehaviour
{
    public int levelsCompleted;
    public bool levelOneCompleted;
    public bool levelTwoCompleted;

    [SerializeField]
    private Transform[] waypoints;

    [SerializeField]
    [Range(2, 12)]
    private float moveSpeed = 4;

    public Animator animator;

    private int waypointIndex = 0;

    private Vector3 targetPosition;
    public bool isMoving = false;
    RaycastHit2D hit;

    [HideInInspector]
    public GameObject CurrentButton;

    private Map_Gears MapGearChecker;

    private void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
        levelOneCompleted = GameObject.Find("HighScoreTracker").GetComponent<HighScoreTracker>().Level1Completed;
        levelTwoCompleted = GameObject.Find("HighScoreTracker").GetComponent<HighScoreTracker>().Level2Completed;
        if (levelOneCompleted)
            levelsCompleted++;
        if (levelTwoCompleted)
            levelsCompleted++;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 touchPosition2D = new Vector2(touchPosition.x, touchPosition.y);
            hit = Physics2D.Raycast(touchPosition2D, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.tag == "Level" && !isMoving)
            {
                MapGearChecker = hit.collider.gameObject.GetComponent<Map_Gears>();

                if (CurrentButton != hit.collider.gameObject)
                {
                    if (MapGearChecker.isAvailable)
                    {
                        SetTargetPosition();
                    }
                }
                else if (CurrentButton == hit.collider.gameObject && !isMoving)
                {
                    MapGearChecker.ActivateLevelPopup();
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
                        MapGearChecker?.ActivateLevelPopup();
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
                        MapGearChecker?.ActivateLevelPopup();
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
        CurrentButton = other?.gameObject;            
    }
}
