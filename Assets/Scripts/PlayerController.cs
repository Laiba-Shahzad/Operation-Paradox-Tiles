using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public float jumpForce = 5f;
    public Transform cameraTransform;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = cameraTransform.forward * v + cameraTransform.right * h;
        move.y = 0;

        rb.linearVelocity = new Vector3(move.x * speed, rb.linearVelocity.y, move.z * speed);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionStay()
    {
        isGrounded = true;
    }

    private void OnCollisionExit()
    {
        isGrounded = false;
    }
}