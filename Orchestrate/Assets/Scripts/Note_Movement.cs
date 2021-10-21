using UnityEngine;

public class Note_Movement : MonoBehaviour
{
    [Tooltip("This is how fast the note moves. It is recommended to keep it at 0.05 or lower.")]
    public float speed;

    private AudioSource IncorrectSFX;

    void Awake()
    {
        IncorrectSFX = gameObject.GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - speed);
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
