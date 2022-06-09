using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public bool Continue;
    public GameObject[] puffle;
    public GameObject Canvas;
    public GameObject Canon;

    private void Start()
    {
        Canvas.SetActive(false);
        Canon.SetActive(true);
    }

    void Update()
    {
        puffle = GameObject.FindGameObjectsWithTag("Puffle");

        foreach (var Bool in puffle)
        {
            Continue = Bool.GetComponent<MInputV2>().Continue;
        }

        if(Continue)
        {
           Canon.SetActive(false);
           Canvas.SetActive(true);
        }
    }
}
