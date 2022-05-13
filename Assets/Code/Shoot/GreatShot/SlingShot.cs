using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    Camera cam;

    private bool PressMouse;

    public GameObject PufflePrefab;

    public Transform FirePoint;

    private Vector3 initialVelocity;

    // Start is called before the first frame update
    void Start()
    {
        if(target == null)
        {
            target = transform;
        }
        if(cam == null)
        {
            cam = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            PressMouse = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            PressMouse = false;

            Shot();
        }

        if(PressMouse)
        {
            Vector3 mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;

            Vector3 lookatDirection = mouseWorldPos - target.position;
            target.right = lookatDirection;

            initialVelocity = mouseWorldPos - FirePoint.position;
        }
    }

    private void Shot()
    {
        GameObject canonball = Instantiate(PufflePrefab, FirePoint.position, Quaternion.identity);
        canonball.GetComponent<MInputV2>().AddForce(initialVelocity);
    }
}
