using UnityEngine;

/*

    Code created by Seth Grimes for DIG4715 Casual Game Production, 

*/

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
        if (!gameObject.GetComponent<Note_Controller>().GetJustDestroyed() && other.gameObject.tag == "Hitter" && this.gameObject.tag != "HealthNote")
        {
            Handheld.Vibrate();
            GameController.PlayMissSFX();
            GameController.StartCoroutine("ChangeHealth", -1);
            GameController.NotesMissed++;
            Destroy(gameObject);
        }
        else if ( other.gameObject.tag == "SmokeBoundry")
            Destroy(gameObject);
    }
}
