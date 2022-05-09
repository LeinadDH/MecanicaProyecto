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

    float TempAngle;

    public float LastX;

    public List<GameObject> planet;// = new List<GameObject>();

    public void Update()
    {
        foreach (GameObject p in planet)
        {
            Vector2 distanceVector = new Vector2(0, 0);
           
            if (isGrounded == true)
            {
                distanceVector = new Vector2(0, 0);

                if (jump == true)
                {
                    moveDirection.y += gVector * jumpSpeed * Time.fixedDeltaTime;
                    isGrounded = false;
                }
                else if (jump == false)
                {
                    distanceVector = new Vector2(0, 0);
                }
            }
            else if (isGrounded == false)
            {
                distanceVector = (Vector2)p.transform.position - (Vector2)this.transform.position;
                float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
                TempAngle = angle + 90;

                foreach (float g in Gravity)
                {
                    moveDirection += g * distanceVector * Time.fixedDeltaTime;


                    if (jump == false)
                    {
                        moveDirection += g * distanceVector * Time.fixedDeltaTime;
                    }
                    if (jump == true)
                    {
                        StartCoroutine(StopJump());
                    }
                    StartCoroutine(StopJump());
                }
            }
        }
        transform.localRotation = Quaternion.AngleAxis(TempAngle, Vector3.forward);    

        transform.Translate(moveDirection);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        go.Add(collision.gameObject);
        planet.Add(collision.gameObject.GetComponentInChildren<GetGO>().planet);
        Gravity.Add(collision.gameObject.GetComponent<WorldGravity>().gravity);
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        Gravity.Remove(collision.gameObject.GetComponent<WorldGravity>().gravity);
        planet.Remove(collision.gameObject.GetComponentInChildren<GetGO>().planet);
        go.Remove(collision.gameObject);
        transform.localRotation = Quaternion.AngleAxis(0, Vector3.forward);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
            jump = false;
        }
    }

    /*
    public void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            isGrounded = false;
            //jump = false;
        }
    }
    */

    IEnumerator StopJump()
    {
        yield return new WaitForSeconds(0.1f);
        jump = false;
        isGrounded = false;
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
