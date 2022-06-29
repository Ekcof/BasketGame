using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRespawnScript : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    private IEnumerator coroutine;


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ball")
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            coroutine = RespawnTheBall(2f, rb);
            StartCoroutine(coroutine);
        }
    }

    IEnumerator RespawnTheBall(float seconds, Rigidbody rb)
    {
        yield return new WaitForSeconds(seconds);
        rb.velocity = Vector3.zero;
        rb.transform.position = respawnPoint.position;
    }
}
