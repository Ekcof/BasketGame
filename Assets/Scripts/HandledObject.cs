using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandledObject : MonoBehaviour
{
    [SerializeField] private float handleSpeed = 2.0f;
    [SerializeField] private float epsilon = 0.005f;
    [SerializeField] private GameObject handledObject;
    private Rigidbody rigidbody;
    private bool isHandled;

    private void Update()
    {
        if (!isHandled && handledObject!=null)
        {
            Debug.Log("korabl");
            handledObject.transform.position = Vector3.Lerp(handledObject.transform.position, transform.position, handleSpeed * Time.deltaTime);
            if (Vector3.Distance(handledObject.transform.position, transform.position) < epsilon) isHandled = true; 
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
        rigidbody = newHandledObject.GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    /// <summary>
    /// Release the taken object
    /// </summary>
    public void ReleaseObject()
    {
        if(isHandled && handledObject != null && rigidbody != null)
        {
            isHandled = false;
            UnfreezeObject();
            rigidbody = null;
        }
    }

    /// <summary>
    /// Throw the handled object
    /// </summary>
    /// <param name="strength">strength of a throw</param>
    public void ThrowObject(float strength)
    {
        isHandled = false;
        UnfreezeObject();
        rigidbody.AddForce(Camera.main.transform.forward * strength, ForceMode.Impulse);
        rigidbody = null;
    }
    
    /// <summary>
    /// Get if the object is handled
    /// </summary>
    /// <returns></returns>
    public bool GetHandled()
    {
        return isHandled;
    }

    /// <summary>
    /// Unfreeze constraints and unparent of rigidbody of the object
    /// </summary>
    private void UnfreezeObject()
    {
        handledObject.transform.parent = null;
        rigidbody.constraints = RigidbodyConstraints.None;
        handledObject = null;
    }
}
