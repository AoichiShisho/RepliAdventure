using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float turnSpeed = 720.0f; // 1秒間にどれだけ旋回するか（度数）

    private Rigidbody rb;
    private Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        Vector3 camForward = cam.transform.forward;
        Vector3 camRight = cam.transform.right;

        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 inputDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) inputDirection += camForward;
        if (Input.GetKey(KeyCode.S)) inputDirection -= camForward;
        if (Input.GetKey(KeyCode.A)) inputDirection -= camRight;
        if (Input.GetKey(KeyCode.D)) inputDirection += camRight;

        Vector3 movement = inputDirection.normalized * speed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // プレイヤーの向きを変更
        if (inputDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDirection, Vector3.up);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
        }
    }
}
