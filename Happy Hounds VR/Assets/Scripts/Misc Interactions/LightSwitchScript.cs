using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchScript : MonoBehaviour
{
    public GameObject lightTest;
    bool lightStatus;

    // Use this for initialization
    void Start()
    {
        lightStatus = lightTest.activeInHierarchy;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GameController")
        {
            LightSwitch();
        }
    }
    void LightSwitch()
    {
        if (lightTest.activeInHierarchy == true)
        {
            lightTest.SetActive(false);
        }
        else
        {
            lightTest.SetActive(true);
        }
    }
}
