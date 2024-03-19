using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(1f, 10f)]
    private float speed = 5.0f;
    [SerializeField, Range(100f, 1000f)]
    private float turnSpeed = 720.0f;
    [SerializeField, Range(1f, 20f)]
    private float jumpHeight = 2.0f;
    [SerializeField, Range(0f, 5f)]
    private float fallMultiplier = 2.5f;  // 落下時の重力加速度を増加させるための乗数

    private Rigidbody rb;
    private Camera cam;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // 地面に接触していない時に追加の重力を適用
        if (!isGrounded && rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        Vector3 camForward = Vector3.Scale(cam.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = Vector3.Scale(cam.transform.right, new Vector3(1, 0, 1)).normalized;

        Vector3 inputDirection = (camForward * Input.GetAxisRaw("Vertical") + camRight * Input.GetAxisRaw("Horizontal")).normalized;
        Vector3 movement = inputDirection * speed;

        if (inputDirection == Vector3.zero)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        else
        {
            rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        }
    }

    private void Rotate()
    {
        Vector3 direction = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
}
