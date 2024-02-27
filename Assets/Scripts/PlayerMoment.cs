using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMoment : MonoBehaviour
{
    CharacterController controller;
    public Transform cam;

    public float gravity = -9.81f;
    public float speed = 6.0f;
    public float jumpHeight = 4f;

    private Vector3 velocity;
    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundLayerMask;
    private Vector3 spherePos;

    [SerializeField] float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Gravity();
        Jump();
        GetDirectionAndMove();
    }

    private void GetDirectionAndMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);
        }
    }

    private bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundLayerMask)) return true;
        return false;
    }

    private void Gravity()
    {
        if (!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -0.5f;

        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            velocity.y += Mathf.Sqrt(jumpHeight * 2 * (-gravity));
            controller.Move(velocity * Time.deltaTime);
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    }*/
}
