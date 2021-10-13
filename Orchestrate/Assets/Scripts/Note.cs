using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public static float Speed {get; set;}

    public static float TrackNumber {get; set;}
    
    public Note(float speed, float trackNum)
    {
        Speed = speed;
        TrackNumber = trackNum;
    }
}
