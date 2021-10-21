using UnityEngine;

public class Note_Movement : MonoBehaviour
{
    private AudioSource IncorrectSFX;

    private Game_Controller GameController = null;

    void Awake()
    {
        IncorrectSFX = gameObject.GetComponent<AudioSource>();
        GameController = GameObject.Find("GameController").GetComponent<Game_Controller>();
    }
    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - GameController.NoteSpeed);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!gameObject.GetComponent<Note_Controller>().GetJustDestroyed())
        {
            GameObject.Find("GameController").GetComponent<Game_Controller>().PlayMissSFX();
            GameObject.Find("GameController").GetComponent<Game_Controller>().ChangeHealth(-1);
        }

        Destroy(gameObject);
    }
}
