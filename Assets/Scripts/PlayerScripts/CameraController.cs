using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    [SerializeField] private float distance = 10.0f;
    [SerializeField] private float height = 5.0f;
    [SerializeField, Range(35f, 100f)] private float rotationSpeed = 50.0f;
    private Vector3 offset;

    [SerializeField] private float cameraYMin = 30f;
    [SerializeField] private float cameraYMax = 80f;
    [SerializeField] private bool cameraInRange = true;

    void Start()
    {
        InitializeCameraRotation();
    }

    void LateUpdate()
    {
        if (transform.position.y <= cameraYMax && transform.position.y >= cameraYMin) {
            cameraInRange = true;
        } else {
            cameraInRange = false;
        }

        if (cameraInRange) {
            RotateCameraWithInput();
            AdjustCameraPositionToAvoidObstacles();
        }
    }

    void InitializeCameraRotation()
    {
        offset = new Vector3(0.5f * distance, height, -0.5f * distance);

        transform.rotation = Quaternion.Euler(0, 45, 0);

        offset = transform.rotation * offset;

        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }

    void RotateCameraWithInput()
    {
        Vector3 followPosition = player.position + offset;
        transform.position = followPosition;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(player.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(player.position, Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        offset = transform.position - player.position;
    }

    void AdjustCameraPositionToAvoidObstacles()
    {
        RaycastHit hit;
        if (Physics.Raycast(player.position, transform.position - player.position, out hit, offset.magnitude))
        {
            transform.position = hit.point;
            transform.LookAt(player.position);
        }
    }

    public void ResetPosition()
    {
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }
}
