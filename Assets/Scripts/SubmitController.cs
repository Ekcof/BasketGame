using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitController : MonoBehaviour
{
    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject holdPosition;
    [SerializeField] private float maxStrength = 15f;
    [SerializeField] private float speed = 0.5f;
    private float timeElapsed;
    private Ray ray;
    private HandledObject handledObject;
    private float strength = 0;
    private DrawTrajectory drawTrajectory;

    private void Awake()
    {
        handledObject = holdPosition.GetComponent<HandledObject>();
        drawTrajectory = holdPosition.GetComponent<DrawTrajectory>();
    }

    private void Update()
    {
        //Debug.DrawLine(Camera.main.ScreenToViewportPoint , Camera.main.transform.forward);
        //Debug.DrawRay()
        //Ray ray2 = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        //Debug.DrawRay(ray2.origin, ray2.direction * 4, Color.red, 1f, false);
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Input.GetButton("Submit") || Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out hit, 100.0f) && hit.transform.tag == "Ball")
            {
                handledObject.TakeObject(hit.transform.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            handledObject.ReleaseObject();
        }

        if (Input.GetMouseButton(1))
        {
            timeElapsed += Time.deltaTime;
            Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
            strength = Mathf.Lerp(strength, maxStrength, timeElapsed);
            Vector3 forceV = Camera.main.transform.forward * strength;
            DrawTrajectory.Instance.UpdateTrajectory(forceV, rb, transform.position);
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (handledObject.GetHandled())
            {
                handledObject.ThrowObject(strength);
            }
            timeElapsed = 0;
            strength = 0;
        }
    }
}
