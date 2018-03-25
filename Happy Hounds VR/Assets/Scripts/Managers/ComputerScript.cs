using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerScript : MonoBehaviour {
    public HelmetCheck dogHelm;
    public HelmetCheck playerHelm;
    AudioManager audioManager;
    public bool outerDoorAcc;
    public bool innerDoorAcc;
    DoorControl innerDoor;
    OuterDoorScript outerDoor;

    public GameObject outerInnerButton;

	// Use this for initialization
	void Start () {
        //dogHelm = GameObject.FindGameObjectWithTag("CorgiHelmZone").GetComponent<HelmetCheck>();
       // playerHelm = GameObject.FindGameObjectWithTag("PlayerHelmZone").GetComponent<HelmetCheck>();
        outerDoorAcc = false;
        innerDoorAcc = true;
        audioManager = FindObjectOfType<AudioManager>();
        innerDoor = GameObject.FindGameObjectWithTag("InnerButton").GetComponent<DoorControl>();
        outerDoor = GameObject.FindGameObjectWithTag("OuterButton").GetComponent<OuterDoorScript>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (dogHelm.HelmOn && playerHelm.HelmOn ) {
            if (other.gameObject.tag == "GameController")
            {
                //Renderer rend = outerInnerButton.GetComponent<Renderer>();
                //rend.material.SetColor("_Color", Color.green);
                //outerDoorAcc = true;

                //innerDoorAcc = false;
                innerDoor.AffectDoor();
                outerDoor.AffectDoor();
                audioManager.PlayOnce("Vacuum");
            }
        }
    }
}
