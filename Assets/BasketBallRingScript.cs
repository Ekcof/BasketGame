using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketBallRingScript : MonoBehaviour
{
    [SerializeField] private GameObject firstRing;
    [SerializeField] private GameObject secondRing;
    [SerializeField] private Text countText;
    private int ballCount;
    private bool passedRing1;
    private bool passedRing2;
    private RingTriggerScript ringOneScript;
    private RingTriggerScript ringTwoScript;

    private void Awake()
    {
        ringOneScript = firstRing.GetComponent<RingTriggerScript>();
        ringTwoScript = secondRing.GetComponent<RingTriggerScript>();
    }

    private void Update()
    {
        passedRing1 = ringOneScript.IsPassed;
        passedRing2 = ringTwoScript.IsPassed;
        if (passedRing1)
        {
            if (passedRing1 && passedRing2)
            {
                ++ballCount;
                SetCountText();
                ringOneScript.IsPassed = false;
                ringTwoScript.IsPassed = false;
            }
        }
        else
        {
            if (passedRing2)
            {
                ringTwoScript.IsPassed = false;
            }
        }
    }

    private void SetCountText()
    {
        countText.text = ballCount.ToString();
    }
}
