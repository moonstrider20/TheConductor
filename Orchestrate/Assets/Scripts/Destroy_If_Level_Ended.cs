using UnityEngine;

public class Destroy_If_Level_Ended : MonoBehaviour
{
    private Game_Controller Game_Controller;
    void Awake()
    {
        Game_Controller = GameObject.Find("GameController").GetComponent<Game_Controller>();
    }
    void Update()
    {
        if ( Game_Controller.Health <= 0 || Game_Controller.WinConditionAchieved )
            Destroy(gameObject);
    }
}
