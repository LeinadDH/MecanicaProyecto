using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MInputV2 : MonoBehaviour
{
    public List<GameObject> go;
    public List<GameObject> planet;
    public List<float> Gravity;

    public Vector2 moveDirection;
    public bool isGrounded = false;

    private float density;
    private float Velocity;
    private float coeficientDrag = 1;
    private float dragArea = 1;

    private bool Drag = false;
    private bool Friction = false;

    public float coeficientFriction = 10;
    private float normalForce;
    public float mass;

    public void Update()
    {
        foreach (GameObject p in planet)
        {
            Vector2 distanceVector = new Vector2(0, 0);

            if (isGrounded == true)
            {
                distanceVector = new Vector2(0, 0);
                moveDirection = Vector2.zero;
            }

            //Friction formula
            if (Friction == true)
            {
                //distanceVector = new Vector2(moveDirection.x += 1 * Time.fixedDeltaTime, 0);
                //moveDirection = new Vector2(moveDirection.x, 0);
                Vector2 frictionVect = -coeficientFriction * normalForce * moveDirection.normalized;

                if(frictionVect.magnitude >= moveDirection.magnitude)
                {
                    moveDirection = Vector2.zero;
                }
                else
                {
                    moveDirection += frictionVect;
                }
            }
            else if (isGrounded == false)
            {

                distanceVector = (Vector2)p.transform.position - (Vector2)this.transform.position;

                //Gravity formula
                foreach (float g in Gravity)
                {
                    moveDirection.y += g * distanceVector.y;

                    normalForce = g * mass;
                }

                //DragForce formula
                if(Drag == true)
                {
                    Velocity = moveDirection.magnitude; //Mathf.Sqrt((moveDirection.x * moveDirection.x) + (moveDirection.y  * moveDirection.y));
                    Vector2 DragVect = -0.5f * density * Velocity * coeficientDrag * dragArea * moveDirection;

                    if (DragVect.magnitude >= moveDirection.magnitude)
                    {
                        moveDirection = Vector2.zero;
                    }
                    else
                    { 
                        moveDirection += DragVect;
                    }
                }
            }
        }

        AddForce(moveDirection);
        transform.Translate(moveDirection);
        StartCoroutine(DeletePuffle());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Atmos")
        {
            go.Add(collision.gameObject);
            planet.Add(collision.gameObject.GetComponentInChildren<GetGO>().planet);
            Gravity.Add(collision.gameObject.GetComponent<WorldGravity>().gravity);
        }

        if(collision.tag == "DragZone" )
        {
            Drag = true;
            density = collision.gameObject.GetComponent<GetDensity>().density;
        }
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Atmos")
        {
            Gravity.Remove(collision.gameObject.GetComponent<WorldGravity>().gravity);
            planet.Remove(collision.gameObject.GetComponentInChildren<GetGO>().planet);
            go.Remove(collision.gameObject);
            transform.localRotation = Quaternion.AngleAxis(0, Vector3.forward);
            isGrounded = false;
        }
        if(collision.tag == "DragZone")
        {
            Drag = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "FrictionFloor")
        {
            Friction = true;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = false;
        }
        if (collision.gameObject.tag == "FrictionFloor")
        {
            Friction = false;
        }
    }

    public void AddForce(Vector2 force)
    {
        moveDirection += force * Time.fixedDeltaTime;
    }

    IEnumerator DeletePuffle()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
