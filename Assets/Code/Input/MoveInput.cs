using UnityEngine;
using UnityEngine.InputSystem;

public class MoveInput : MovementInputManager
{
    public GameObject Player;

    public float movementSpeed = 12f;
    private Vector2 Movement;

    public Vector2 moveDirection;
    public float jumpSpeed = 10f;
    public float gravity = -0.010f;
    public float maxAceletarion;

    public bool jump = false;
    public bool isGrounded = false;

    public void Update()
    {
        transform.Translate(new Vector3(Movement.x, 0f, 0f) * movementSpeed * Time.deltaTime);

        AuthenticBoxColider();

        if (isGrounded == true)
        {
            if (jump == true)
            {
                moveDirection.y = jumpSpeed * Time.deltaTime * 100;
                if(moveDirection.y > maxAceletarion)
                {
                    isGrounded = false;
                }
            }
            else if(jump == false)
            {
                moveDirection.y = 0.0f;
            }
        }
        else if(isGrounded == false)
        {
            moveDirection.y += gravity * Time.deltaTime;
            jump = false;
        }
        
        transform.Translate(moveDirection);
    }

    protected override void Move(InputAction.CallbackContext value)
    {
        Movement = value.ReadValue<Vector2>();
    }

    protected override void Jump(InputAction.CallbackContext value)
    {
        jump = true;
    }

    protected override void NotJump(InputAction.CallbackContext value)
    {
        if(moveDirection.y > 0f)
        {
            isGrounded = false;
        }
    }
    
    public void AuthenticBoxColider()
    {
        if(Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y), new Vector2(1f, 1f), 0f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y), new Vector2(1f, 1f));
    }
}
