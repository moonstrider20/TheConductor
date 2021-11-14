using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map_Gears : MonoBehaviour
{
    public GameObject levelPopup;
    //public Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
    public int completedLevelsRequired;
    public bool isAvailable = false;

    private Map_Controller MapController;

    void Start()
    {
        MapController = GameObject.Find("Conductor").GetComponent<Map_Controller>();
    }

    void Update()
    {
        if (MapController.levelsCompleted >= completedLevelsRequired)
        {
            isAvailable = true;
        }
        if (!isAvailable)
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        else
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //if (col!= null && col.gameObject.name == "Conductor" && !MapController.isMoving)
            //ActivateLevelPopup();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col?.gameObject.name == "Conductor")
        {
            levelPopup.SetActive(false);
        }
    }

    public void ActivateLevelPopup()
    {
        StartCoroutine("Wait");
        levelPopup.SetActive(true);
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
    }
}
