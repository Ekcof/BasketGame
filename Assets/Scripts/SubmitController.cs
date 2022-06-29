using UnityEngine;

public class SubmitController : MonoBehaviour
{
    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject holdPosition;
    [SerializeField] private float minStrength = 1.5f;
    [SerializeField] private float maxStrength = 15f;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float takeBallDistance = 10f;
    private Vector3 vectorThrow;
    private float timeElapsed;
    private Ray ray;
    private HandledObject handledObject;
    private float strength;
    private Rigidbody rb;

    private void Awake()
    {
        strength = minStrength;
        handledObject = holdPosition.GetComponent<HandledObject>();
    }

    private void Update()
    {
        // Check if there is an existing object has been taken
        if (handledObject.IsHandled || handledObject.IsDraging)
        {
            if (handledObject.IsHandled && rb!=null)
            {
                strength = Mathf.Lerp(minStrength, maxStrength, timeElapsed);
                vectorThrow = new Vector3(Camera.main.transform.forward.x, Camera.main.transform.forward.y + 0.5f, Camera.main.transform.forward.z) * strength;
                Vector3 forceV = vectorThrow * 50;

                DrawTrajectory.Instance.UpdateTrajectory(forceV, rb, holdPosition.transform.position);
            }
        }
        else
        {
            ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            bool sphereCast = Physics.SphereCast(ray.origin, 0.3f, ray.direction, out hit, takeBallDistance, layerMask);
            if (hit.transform != null) Debug.Log(hit.transform.name);
            rb = null;
            if (hit.transform != null) rb = hit.transform.GetComponent<Rigidbody>();

            if (Input.GetButton("Submit") || Input.GetKeyDown(KeyCode.E))
            {
                if (rb != null && hit.transform.tag == "Ball")
                {
                    handledObject.IsDraging = true;
                    handledObject.TakeObject(hit.transform.gameObject);
                }
            }
        }

        // Get the scroll to change the trajectory of throw
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            timeElapsed += (Input.GetAxis("Mouse ScrollWheel") * speed);
            if (timeElapsed < 0) { timeElapsed = 0; }
            if (timeElapsed > 1) { timeElapsed = 1; }
        }

        //Get the F button to drop the object
        if (Input.GetKeyDown(KeyCode.F))
        {
            handledObject.ReleaseObject();
        }

        //Get the right click to throw the object
        if (Input.GetMouseButtonUp(1))
        {
            if (handledObject.IsHandled)
            {
                handledObject.ThrowObject(vectorThrow);
            }
            timeElapsed = 0;
            strength = minStrength;
        }
    }
}
