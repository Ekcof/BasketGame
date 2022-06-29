using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRingPassScript : MonoBehaviour
{
    [SerializeField] private RingTriggerScript RingTriggerScriptOne;
    [SerializeField] private RingTriggerScript RingTriggerScriptTwo;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ball")
        {
            RingTriggerScriptOne.IsPassed = false;
            RingTriggerScriptTwo.IsPassed = false;
        }
    }

}
