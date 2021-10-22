using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

Code created by Seth Grimes for DIG4715C Casual Game Production. October 22nd, 2021.

*/

public class Note_Rotator : MonoBehaviour
{
    public bool Clockwise;

    [Tooltip("This is how fast the cog on the note spins.")]
    public float RotationSpeed;
    void FixedUpdate()
    {
        switch ( Clockwise )
        {
            case true:
                gameObject.transform.Rotate(0, 0, -RotationSpeed);
                break;
            default:
                gameObject.transform.Rotate(0, 0, RotationSpeed);
                break;
        }
        
    }
}
