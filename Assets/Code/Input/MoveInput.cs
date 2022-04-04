using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class MoveInput : MovementInputManager
{
    public float movementSpeed = 5f;
    private Vector2 Movement;

    public Vector2 moveDirection;
    public float jumpSpeed = 10f;
    public float Gravity = 0;
    public float maxAceletarion;

    public bool jump = false;
    public bool isGrounded = false;

    public float LastX;
    public Vector2 vectorD;
    public Vector2 gravityV;

    public GameObject planet;

    public void Update()
    {
        Debug.DrawRay(transform.position, Movement, Color.red);

        moveDirection.x = Movement.x * movementSpeed * Time.fixedDeltaTime;

        vectorD = this.transform.position - planet.transform.position;

        gravityV = vectorD.normalized - moveDirection;

        AuthenticBoxColider();

        //sideCollitions();

        Vector2 distanceVector = (Vector2)planet.transform.position - (Vector2)transform.position;
        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        if (isGrounded == true)
        {
            if (jump == true)
            {
                moveDirection.y = jumpSpeed * Time.fixedDeltaTime * 100;
                isGrounded = false; 
            }
            else if (jump == false)
            {
                moveDirection.y = 0f;
            }

        }
        else if (isGrounded == false)
        {
            Gravity = Mathf.Abs(0.2f);
            moveDirection.y -= Mathf.Abs(gravityV.y * Gravity * Time.fixedDeltaTime);

            if (jump == false)
            {
                moveDirection.y -= Mathf.Abs(gravityV.y * Gravity * Time.fixedDeltaTime);
            }
            if (jump == true)
            {
                StartCoroutine(StopJump());
            }
            StartCoroutine(StopJump());

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
        if (Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y), new Vector2(1f, 1f), 0f))
        {
            isGrounded = true;
            moveDirection.y = 0f;
        }
        if (Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y), new Vector2(1f, 1f), 0f) == null)
        {
            isGrounded = false;
        }

    }
    
    public void sideCollitions()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Movement, 0.5f);

        if (hit.collider != null)
        {
            LastX = Movement.x;
            Movement.x += Movement.x * -2;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
    }


    IEnumerator StopJump()
    {
        yield return new WaitForSeconds(0.1f);
        jump = false;
        isGrounded = false;
    }
}
