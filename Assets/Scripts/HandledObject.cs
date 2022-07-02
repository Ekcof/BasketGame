using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandledObject : MonoBehaviour
{
    [SerializeField] private float handleSpeed = 2.0f;
    [SerializeField] private float epsilon = 0.005f;
    [SerializeField] private GameObject handledObject;
    [SerializeField] private GameObject line;
    private Rigidbody rigidBody;
    private bool isHandled;
    private bool isDraging;

    private void Update()
    {
        if (!isHandled && handledObject != null)
        {
            handledObject.transform.position = Vector3.Lerp(handledObject.transform.position, transform.position, handleSpeed * Time.deltaTime);
            if (Vector3.Distance(handledObject.transform.position, transform.position) < epsilon)
            {
                isHandled = true;
                isDraging = false;
                line.SetActive(true);
            }
        }
    }

    /// <summary>
    /// Take object in hands
    /// </summary>
    /// <param name="newHandledObject"></param>
    public void TakeObject(GameObject newHandledObject)
    {
        handledObject = newHandledObject;
        handledObject.transform.SetParent(transform);
        rigidBody = newHandledObject.GetComponent<Rigidbody>();
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
    }

    /// <summary>
    /// Release the taken object
    /// </summary>
    public void ReleaseObject()
    {
        if (isHandled && handledObject != null && rigidBody != null)
        {
            isHandled = false;
            UnfreezeObject();
            rigidBody = null;
        }
    }

    /// <summary>
    /// Throw the handled object
    /// </summary>
    /// <param name="strength">strength of a throw</param>
    public void ThrowObject(Vector3 vectorThrow)
    {
        UnfreezeObject();
        rigidBody.AddForce(vectorThrow, ForceMode.Impulse);
        rigidBody = null;

    }

    /// <summary>
    /// Get if the object is handled
    /// </summary>
    /// <returns></returns>
    public bool IsHandled
    {
        get { return isHandled; }
        set { isHandled = value; }
    }

    /// <summary>
    /// Get the rigidBody of handled object
    /// </summary>
    /// <returns></returns>
    public Rigidbody GetRigidBody()
    {
        return rigidBody;
    }

    /// <summary>
    /// Get the if the object is currently dragging by player
    /// </summary>
    public bool IsDraging
    {
        get { return isDraging; }
        set { isDraging = value; }
    }

    /// <summary>
    /// Unfreeze constraints and unparent of rigidbody of the object
    /// </summary>
    public void UnfreezeObject()
    {
        line.SetActive(false);
        isHandled = false;
        isDraging = false;
        handledObject.transform.parent = null;
        rigidBody.constraints = RigidbodyConstraints.None;
        handledObject = null;
    }

}
