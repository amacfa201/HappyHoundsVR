using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class objectPickup : MonoBehaviour {


    private Valve.VR.EVRButtonId triggerButton = EVRButtonId.k_EButton_SteamVR_Trigger;

    //private SteamVR_Controller.Device controller;
    private SteamVR_TrackedObject trackedObj;

    [SerializeField]
    private GameObject obj;
    private FixedJoint fixedJoint;


    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    
    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
       // controller = SteamVR_Controller.Input((int)trackedObj.index);
        fixedJoint = GetComponent<FixedJoint>();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (controller == null)
        {
            Debug.Log("Controller Not Initilalised");
            return;
        }

        //var device = SteamVR

        if (controller.GetPressDown(triggerButton))
        {
            PickupObj();
        }


        if (controller.GetPressUp(triggerButton))
        {
            DropObj();
        }



	}


    void OnTriggerStay(Collider other)
    {
        print("TRIGGER");
        if (other.tag == "Pickupable") {
            obj = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        obj = null;
    }


    void PickupObj() {

        if (obj != null)
        {
            fixedJoint.connectedBody = obj.GetComponent<Rigidbody>();
        }
        else {
            fixedJoint.connectedBody = null;
        }
    }

    void DropObj()
    {
        if (fixedJoint.connectedBody != null)
            fixedJoint.connectedBody = null;
    }


}
