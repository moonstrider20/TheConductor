using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_Controller : MonoBehaviour
{
    private bool canBeDestroyed = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( other != null )
        {
            canBeDestroyed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ( other != null )
        {
            canBeDestroyed = false;
        }
    }

    public void DestroyThisObject()
    {
        if (canBeDestroyed)
            Destroy(gameObject);
    }

    public bool GetCanBeDestroyed()
    {
        return canBeDestroyed;
    }
}
