using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetrieverScript : MonoBehaviour {

    public GameObject entireScreen;
    public GameObject mainMenu;
    public GameObject toys;
    public GameObject customiseDog;
    public GameObject inventory;
    

    public bool isOn;

    float theTime;
    public float toggleDelay = 0.5f;
    // Use this for initialization
    void Start()
    {
        //uiMenu = GameObject.FindGameObjectWithTag("UI");
    }

    // Update is called once per frame
    void Update()
    {
        theTime += Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameController" && !isOn && theTime > toggleDelay)
        {
            isOn = true;
            entireScreen.SetActive(true);
            theTime = 0.0f;
        }
        else if (theTime > toggleDelay && other.tag == "GameController")
        {

            isOn = false;
            entireScreen.SetActive(false);
            theTime = 0.0f;
        }
    }


    public void DisableAllScreens()
    {
        mainMenu.SetActive(false);
        toys.SetActive(false);
        customiseDog.SetActive(false);
        inventory.SetActive(false);
    }

    public void TurnOnScreen(GameObject name)
    {
        DisableAllScreens();
        name.SetActive(true);
    }
}
