using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketBallRingScript : MonoBehaviour
{
    [SerializeField] private GameObject firstRing;
    [SerializeField] private GameObject secondRing;
    [SerializeField] private Transform allObstacles;
    [SerializeField] private Text countText;
    private int ballCount;
    private RingTriggerScript ringOneScript;
    private RingTriggerScript ringTwoScript;
    private int obstacles;
    private int revealedObstacles;

    private void Awake()
    {
        ringOneScript = firstRing.GetComponent<RingTriggerScript>();
        ringTwoScript = secondRing.GetComponent<RingTriggerScript>();
        obstacles = allObstacles.childCount;
    }

    private void Update()
    {
        if (ringOneScript.IsPassed)
        {
            if (ringOneScript.IsPassed && ringTwoScript.IsPassed)
            {
                ++ballCount;
                SetCountText();
                ringOneScript.IsPassed = false;
                ringTwoScript.IsPassed = false;
            }
        }
        else
        {
            if (ringTwoScript.IsPassed)
            {
                ringTwoScript.IsPassed = false;
            }
        }
    }

    /// <summary>
    /// Set the count text on canvas
    /// </summary>
    private void SetCountText()
    {
        if (ballCount % 2 == 0) NewObstacle();
        if (ballCount < 10)
        {
            countText.text = "0" + ballCount.ToString();
        }
        else
        {
            countText.text = ballCount.ToString();
        }
    }

    /// <summary>
    /// Reveal new obstacle which has been inactive before
    /// </summary>
    private void NewObstacle()
    {
        if (revealedObstacles < obstacles)
        {
            allObstacles.GetChild(revealedObstacles).gameObject.SetActive(true);
            revealedObstacles++;
        }
}
}
