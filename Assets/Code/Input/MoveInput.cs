using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class MoveInput : MovementInputManager
{
    public float movementSpeed = 5f;
    private Vector2 Movement;

    public Vector2 moveDirection;
    public float jumpSpeed = 10f;
    public float gravity = -0.010f;
    public float maxAceletarion;

    public bool jump = false;
    public bool isGrounded = false;

    public float LastX;

    public void Update()
    {
        Debug.DrawRay(transform.position, Movement, Color.red);

        //Usando Fixed time (movimiento más suave)
        //transform.Translate(new Vector3(Movement.x, 0f, 0f) * movementSpeed * Time.fixedDeltaTime);
        //Sin fixed time (movimiento más incosistente)
        transform.Translate(new Vector3(Movement.x, 0f, 0f) * movementSpeed * Time.deltaTime);

        AuthenticBoxColider();

        sideCollitions();

        if (isGrounded == true)
        {
            if (jump == true)
            {
                gravity = -0.1f;
                //Usando Fixed time (movimiento más suave)
                //moveDirection.y = jumpSpeed * Time.fixedDeltaTime * 100;
                //Sin fixed time (movimiento más incosistente)
                moveDirection.y = jumpSpeed * Time.deltaTime * 100;
                if (moveDirection.y > maxAceletarion)
                {
                    isGrounded = false;
                }
            }
            else if(jump == false)
            {
                moveDirection.y = 0.0f;
                gravity = -0.3f;
            }
        }
        else if(isGrounded == false)
        {
            if(jump == false)
            {
                gravity = -0.3f;
            }
            if(jump == true)
            {
                gravity = -0.1f;
            }
            //Usando Fixed time (movimiento más suave)
            //moveDirection.y += gravity * Time.fixedDeltaTime;
            //Sin fixed time (movimiento más incosistente)
            moveDirection.y += gravity * Time.deltaTime;
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
        jump = false;
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

    public void sideCollitions()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Movement, 0.5f);

        if(hit.collider != null)
        {
            LastX = Movement.x;
            Movement.x += Movement.x * -2;
            StartCoroutine(StopMove());
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2(transform.position.x, transform.position.y), new Vector2(1f, 1f));
    }

    IEnumerator StopMove()
    {
        yield return new WaitForSeconds(0.15f);
        Movement.x = 0;
    }
}
