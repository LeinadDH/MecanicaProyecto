using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MInputV2 : MonoBehaviour
{
    public List<GameObject> go;

    public float movementSpeed;
    private Vector2 Movement;
    public Vector2 moveDirection;
    public float jumpSpeed;
    public List<float> Gravity;
    public float maxAceletarion;
    private float gVector = 1;
    public float publicAngle;

    public bool jump = false;
    public bool isGrounded = false;
    public bool enableGravity = false;

    float TempAngle;

    public float LastX;

    public List<GameObject> planet;

    public void Update()
    {

        if (planet != null)
        {

            foreach (GameObject p in planet)
            {

                Vector2 distanceVector = (Vector2)p.transform.position - (Vector2)this.transform.position;
                float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
                TempAngle = angle + 90;
            }
            transform.localRotation = Quaternion.AngleAxis(TempAngle, Vector3.forward);
        }

        if (enableGravity == true)
        {
            applyGravity();
        }

        transform.Translate(moveDirection);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        go.Add(collision.gameObject);
        GetGravity();
        enableGravity = true;
        /*
        if (collision.tag == "atmosCuad")
        {
            go.Add(collision.gameObject);
            GetGravity();
            enableGravity = true;
            NormalG = true;
            CircleG = false;
            InvertedG = false;
        }
        if (collision.tag == "atmosCirc")
        {
            go.Add(collision.gameObject);
            GetGravity();
            enableGravity = true;
            CircleG = true;
            NormalG = false;
            InvertedG = false;
        }
        if (collision.tag == "atmosInv")
        {
            go.Add(collision.gameObject);
            GetGravity();
            enableGravity = true;
            InvertedG = true;
            CircleG = false;
            NormalG = false;
        }
        */
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        GetComponents();
        /*
        if (collision.tag == "atmosCuad")
        {
            GetComponents();
        }
        if (collision.tag == "atmosCirc")
        {
            GetComponents();
        }
        if (collision.tag == "atmosInv")
        {
            GetComponents();
        }
        go.Remove(collision.gameObject);
        */
        enableGravity = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
            jump = false;
            moveDirection.y = 0;
        }
    }

    void GetComponents()
    {
        foreach (GameObject a in go)
        {
            Gravity.Remove(a.GetComponent<WorldGravity>().gravity);
        }
        foreach (GameObject g in go)
        {
            planet.Remove(g.GetComponentInChildren<GetGO>().planet);
        }
    }

    void GetGravity()
    {
        foreach (GameObject g in go)
        {
            planet.Add(g.GetComponentInChildren<GetGO>().planet);
        }
        foreach (GameObject a in go)
        {
            Gravity.Add(a.GetComponent<WorldGravity>().gravity);
        }
    }

    IEnumerator StopJump()
    {
        yield return new WaitForSeconds(0.1f);
        jump = false;
        isGrounded = false;
        enableGravity = false;
    }

    public void applyGravity()
    {

        if (isGrounded == true)
        {
            moveDirection.x = Movement.x * movementSpeed * Time.fixedDeltaTime;

            if (jump == true)
            {
                moveDirection.y += gVector * jumpSpeed * Time.fixedDeltaTime;
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
            foreach (float g in Gravity)
            {
                moveDirection.y -= gVector * g * Time.fixedDeltaTime;

                if (jump == false)
                {
                    moveDirection.y -= gVector * g * Time.fixedDeltaTime;
                }
                if (jump == true)
                {
                    StartCoroutine(StopJump());
                }
                StartCoroutine(StopJump());
            }
        }
    }

}
