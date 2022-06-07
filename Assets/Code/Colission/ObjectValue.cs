using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectValue : MonoBehaviour
{
    public float Mass;
    public Vector2 Velocity;

    private void FixedUpdate()
    {
        this.gameObject.transform.Translate(Velocity * Time.deltaTime);
    }
}
