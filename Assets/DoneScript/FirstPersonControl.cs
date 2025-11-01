using UnityEngine;

public class FirstPersonControl : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.0f;
    Rigidbody rb;
    float horizontalMovement;
    float verticalMovement;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        readInputs();
        writeInputs();
    }
    void readInputs()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
    }
    void writeInputs()
    {
        horizontalMovement = horizontalMovement * moveSpeed;
        verticalMovement = verticalMovement * moveSpeed;
        Vector3 moveDirection = horizontalMovement * transform.right + verticalMovement * transform.forward;

        rb.MovePosition(rb.position + moveDirection * Time.fixedDeltaTime);
    }
}
