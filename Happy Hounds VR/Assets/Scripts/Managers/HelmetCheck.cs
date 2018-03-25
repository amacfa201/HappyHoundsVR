using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelmetCheck : MonoBehaviour
{
    public bool HelmOn = false;
    public string tagName;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == tagName)
        {
            HelmOn = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == tagName)
        {
            HelmOn = false;
        }
    }
}
