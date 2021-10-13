using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tapper_Controller : MonoBehaviour
{    
    public GameObject ActualHitter;
    public void DestroyOther()
    {
        ActualHitter.GetComponent<Hitter_Controller>().DestroyOther();
    }
}
