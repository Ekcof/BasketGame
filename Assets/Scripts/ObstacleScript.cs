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
        if (collision.gameObject.tag == "Ball")
        {
            if (handledObjectScript != null)
            {
                DropObject(collision.contacts[0].point);
            }
        }
    }

    private void DropObject(Vector3 contactPoint)
    {
        if (handledObjectScript.IsHandled || handledObjectScript.IsDraging)
        {
            handledObjectScript.UnfreezeObject();
            Rigidbody rb = handledObjectScript.GetRigidBody();
            Vector3 dir = pivotPoint.position - contactPoint;
            dir = -dir.normalized;
            rb.AddForce(dir*force);
        }
    }
}
