using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingTriggerScript : MonoBehaviour
{
    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            if (other.transform.position.y >= transform.position.y)
            {
                IsPassed = true;
            }
            else
            {
                StopFromBelow(other.transform);
            }
        }
    }

    /// <summary>
    /// Stop ball which is going from below
    /// </summary>
    /// <param name="ball"></param>
    private void StopFromBelow(Transform ball)
    {
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    public bool IsPassed { get; set; }
}
