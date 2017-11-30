using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorButton : MonoBehaviour {
    public GameObject lightTest;
    bool lightStatus;
	// Use this for initialization
	void Start () {
        lightStatus = lightTest.activeInHierarchy;
        //lightTest = GameObject.FindGameObjectWithTag("Light");
    }
	
	// Update is called once per frame
	void Update () {
        print("light = " + lightStatus);
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
