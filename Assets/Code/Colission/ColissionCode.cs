using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColissionCode : MonoBehaviour
{
    private List<GameObject> allObjects = new List<GameObject>();
    private float distance;
    private float oneX, twoX;
    private float oneY, twoY;
    public float massOne, massTwo;
    public Vector2 velocityOne, velocityTwo;

    private Vector2 aVectorOne, aVectorTwo, finalVOne, finalVTwo;
    private float e = 0;

    private Vector2 distanceVector, vPrimeOne, vPrimeTwo;

    public float pProductOne, pProductTwo, vPrime, forceOne, forceTwo, acelerationOne, acelerationTwo, pProductD;

    void Start()
    {
        allObjects = GameObject.FindGameObjectsWithTag("Ball").ToList();
    }

    private void FixedUpdate()
    {
        MyCollsion();
    }

    void MyCollsion()
    {
        for(int i = 0; i < allObjects.Count; i++)
        {
            for(int j = i + 1; j < allObjects.Count; j++)
            {
                if (BolleanColision(allObjects[i], allObjects[j]))
                {
                    Operation(allObjects[i], allObjects[j]);
                    
                }
            }
        }
    }

    bool BolleanColision(GameObject objectOne, GameObject objectTwo)
    {
        oneX = objectOne.transform.position.x;
        twoX = objectTwo.transform.position.x;
        oneY = objectOne.transform.position.y;
        twoY = objectTwo.transform.position.y;
        distance = Mathf.Sqrt(((twoX - oneX) * (twoX - oneX)) + ((twoY - oneY) * (twoY - oneY)));
        //return distance < objectOne.transform.localScale.x / 2 + objectTwo.transform.localScale.x / 2;
        if (distance < objectOne.transform.localScale.x / 2 + objectTwo.transform.localScale.x / 2)
        {
            massOne = objectOne.GetComponent<ObjectValue>().Mass;
            massTwo = objectTwo.GetComponent<ObjectValue>().Mass;
            velocityOne = objectOne.GetComponent<ObjectValue>().Velocity;
            velocityTwo = objectTwo.GetComponent<ObjectValue>().Velocity;
            distanceVector = (Vector2)objectOne.transform.position - (Vector2)objectTwo.transform.position;

            //Debug.Log("Colsion de " + objectOne.name + " con " + objectTwo.name);
            return true;
        }
        return false;
    }
    
    void Operation(GameObject objectOne, GameObject objectTwo)
    {
        pProductOne = (velocityOne.x * distanceVector.x) + (velocityOne.y * distanceVector.y);
        pProductTwo = (velocityTwo.x * distanceVector.x) + (velocityTwo.y * distanceVector.y);

        vPrime = (((massOne * pProductOne) + (massTwo * pProductTwo)) - ((massTwo * e) * (pProductOne - pProductTwo))) / (massOne + massTwo);
        //forceOne = massOne * (vPrime - pProductOne);
        //forceTwo = -forceOne;
        acelerationOne = vPrime - pProductOne;
        acelerationTwo = (massOne / massTwo) * acelerationOne;

        vPrimeOne = pProductOne * distanceVector.normalized;
        vPrimeTwo = pProductTwo * distanceVector.normalized;

        aVectorOne = acelerationOne * distanceVector.normalized;
        aVectorTwo = acelerationTwo * distanceVector.normalized;

        finalVTwo = vPrimeTwo + aVectorTwo;
        finalVOne = vPrimeOne + aVectorOne;

        objectTwo.GetComponent<ObjectValue>().Velocity = -finalVTwo;
        objectOne.GetComponent<ObjectValue>().Velocity = finalVOne;
    }

}
