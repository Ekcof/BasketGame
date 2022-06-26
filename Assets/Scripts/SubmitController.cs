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
    private DrawTrajectory drawTrajectory;
    private Rigidbody rb;

    private void Awake()
    {
        strength = minStrength;
        handledObject = holdPosition.GetComponent<HandledObject>();
        drawTrajectory = holdPosition.GetComponent<DrawTrajectory>();
    }

    private void Update()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Physics.Raycast(ray, out hit, takeBallDistance);
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
            Physics.Raycast(ray, out hit, takeBallDistance);
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


        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            timeElapsed += (Input.GetAxis("Mouse ScrollWheel") * speed);
            if (timeElapsed < 0) { timeElapsed = 0; }
        }
        Debug.Log(Input.GetAxis("Mouse ScrollWheel"));

        if (Input.GetKeyDown(KeyCode.F))
        {
            handledObject.ReleaseObject();
        }

        /*       if (Input.GetMouseButton(1))
               {
                   timeElapsed += (Time.deltaTime*speed);
               }
               */
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
