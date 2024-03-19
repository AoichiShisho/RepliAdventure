using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    [SerializeField] private float distance = 10.0f;
    [SerializeField] private float height = 5.0f;
    [SerializeField] private float rotationSpeed = 50.0f;
    [SerializeField] private Vector3 offset;

    void Start()
    {
        offset = new Vector3(0.5f * distance, height, -0.5f * distance);

        transform.rotation = Quaternion.Euler(0, 45, 0);

        offset = transform.rotation * offset;

        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }


    void LateUpdate()
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
}
