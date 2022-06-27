using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingTriggerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            IsPassed = true;
        }
    }

    public bool IsPassed { get; set; }
}
