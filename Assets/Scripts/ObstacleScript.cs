using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField] private HandledObject handledObjectScript;
    [SerializeField] private Transform pivotPoint;
    [SerializeField] private float force = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        CheckCollision(collision.gameObject, collision.contacts[0].point);
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckCollision(other.gameObject, other.transform.position);
    }


    /// <summary>
    /// Check if there is a collision with a ball
    /// </summary>
    /// <param name="collision"></param>
    /// <param name="collisionPoint"></param>
    private void CheckCollision(GameObject collision, Vector3 collisionPoint)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (handledObjectScript != null)
            {
                DropObject(collisionPoint);
            }
        }
    }
 
    /// <summary>
    /// Drop the object which has been taken by player
    /// </summary>
    /// <param name="contactPoint"></param>
    private void DropObject(Vector3 contactPoint)
    {
        if (handledObjectScript.IsHandled || handledObjectScript.IsDraging)
        {
            AudioSource sound = handledObjectScript.GetComponent<AudioSource>();
            sound.Play();
            handledObjectScript.UnfreezeObject();
            Rigidbody rb = handledObjectScript.GetRigidBody();
            Vector3 dir = pivotPoint.position - contactPoint;
            dir = -dir.normalized;
            rb.AddForce(dir * force);
        }
    }
}
