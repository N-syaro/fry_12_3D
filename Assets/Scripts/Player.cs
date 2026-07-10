using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    PlayerInput playerInput;
    [SerializeField] float accel;
    Rigidbody rb;
    [SerializeField] float groundNormalYMin = 0.7f;
    bool isGrounded;
    [SerializeField] float jumpSpeed;
    [SerializeField] float groundDamping = 8f;
    [SerializeField] float airDamping = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        rb.sleepThreshold = -1;
    }

    // Update is called once per frame
    void Update()
    {
        var accelVec = playerInput.actions["Move"].ReadValue<Vector2>();

        var cameraDir = playerInput.camera.transform.forward;
        cameraDir.y = 0;
        cameraDir = cameraDir.normalized;

        var cameraRight = playerInput.camera.transform.right;

        var accelVec3D =
            cameraDir * accelVec.y * accel
            + cameraRight * accelVec.x * accel;
        rb.AddForce(accelVec3D, ForceMode.Acceleration);
        if (playerInput.actions["Jump"].WasPressedThisFrame()
&& isGrounded)
        {
            Vector3 jumpVec = new Vector3(0, jumpSpeed, 0);
            rb.AddForce(jumpVec, ForceMode.VelocityChange);
        }
    }
    private void FixedUpdate()
    {
        // 減衰を地上と空中で変える
        if (isGrounded)
        {
            rb.linearDamping = groundDamping;
        }
        else
        {
            rb.linearDamping = airDamping;
        }

        // 物理計算中に接地判定を行うため、一旦ここで false にしておく
        isGrounded = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (var contact in collision.contacts)
        {
            if (contact.normal.y >= groundNormalYMin)
            {
                isGrounded = true;
            }
        }
    }
}
