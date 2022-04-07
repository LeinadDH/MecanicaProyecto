using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class MoveInput : MovementInputManager
{
    public float movementSpeed;
    private Vector2 Movement;
    public Vector2 moveDirection;
    public float jumpSpeed;
    public float Gravity = 0;
    public float maxAceletarion;
    private float gVector = 1;
    public float publicAngle;

    public bool jump = false;
    public bool isGrounded = false;
    public bool enableGravity = false;
    private bool CircleG = false;
    private bool NormalG = false;
    private bool InvertedG = false;

    WorldGravity gravity;

    float TempAngle;

    public float LastX;

    public GameObject planet;

    private Collider2D [] Mybox;

    public void Update()
    {
        Mybox = Physics2D.OverlapBoxAll(new Vector2(transform.position.x, transform.position.y), new Vector2(1f, 1f), 0f);

        if (planet != null)
        {
            //sideCollitions();   

            Vector2 distanceVector = (Vector2)planet.transform.position - (Vector2)transform.position;
            float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;

            if(CircleG == true)
            {
                TempAngle = angle + 90;
            }
            if (NormalG == true)
            {
                TempAngle = 0f;
            }
            if (InvertedG == true)
            {
                TempAngle = 180f;
            }

            transform.localRotation = Quaternion.AngleAxis(TempAngle, Vector3.forward);
        }

        if(planet == null)
        {
            CircleG = false;
            NormalG = false;
            InvertedG = false;
        }

        AuthenticBoxColider();

        if (enableGravity == true)
        {
            applyGravity();
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

        foreach (Collider2D Prueba in Mybox)
        {
            try
            {
                if (Prueba.tag == "Floor")
                {
                    
                    isGrounded = true;
                }
            }
            catch
            {
                Debug.Log("No hay suelo Suelo");
            }

            try
            {
                if (Prueba.tag == "atmosCirc")
                {
                    planet = Prueba.gameObject;
                    Gravity = Prueba.GetComponent<WorldGravity>().gravity;
                    enableGravity = true;
                    CircleG = true;
                }
                if (Prueba.tag == "atmosCuad")
                {
                    planet = Prueba.gameObject;
                    Gravity = Prueba.GetComponent<WorldGravity>().gravity;
                    enableGravity = true;
                    NormalG = true;
                }
                if (Prueba.tag == "atmosInv")
                {
                    planet = Prueba.gameObject;
                    Gravity = Prueba.GetComponent<WorldGravity>().gravity;
                    enableGravity = true;
                    InvertedG = true;
                }
            }
            catch
            {
                enableGravity = false;
            }

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

    IEnumerator StopJump()
    {
        yield return new WaitForSeconds(0.1f);
        jump = false;
        isGrounded = false;
        enableGravity = false;
        planet = null;
    }

    public void applyGravity()
    {    

        if (isGrounded == true)
        {
            moveDirection.x = Movement.x * movementSpeed * Time.fixedDeltaTime;

            if (jump == true)
            {
                moveDirection.y = jumpSpeed * Time.fixedDeltaTime * 100;
                isGrounded = false;
            }
            else if (jump == false)
            {
                moveDirection.y = 0f;
            }
            isGrounded = false;
        }
        else if (isGrounded == false)
        {
            moveDirection.y -= gVector * Gravity * Time.fixedDeltaTime;

            if (jump == false)
            {
                moveDirection.y -= gVector * Gravity * Time.fixedDeltaTime;
            }
            if (jump == true)
            {
                StartCoroutine(StopJump());
            }
            StartCoroutine(StopJump());

        }
    }
}
