using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ballScript : MonoBehaviour
{


    private Valve.VR.EVRButtonId triggerButton = EVRButtonId.k_EButton_SteamVR_Trigger;


    //private SteamVR_Controller.Device controller;
    private SteamVR_TrackedObject trackedObj;

    [SerializeField]
    private GameObject ball;
    private FixedJoint fixedJoint;
    //private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    public bool holdingBall;
    public bool ballThrown;
    public bool ballCollected;
    public bool ballDropped;
    private Rigidbody rigid;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        //controller = SteamVR_Controller.Input((int)trackedObj.index);
        fixedJoint = GetComponent<FixedJoint>();


    }

    // Update is called once per frame
    void Update()
    {


        //if (controller == null)
        //{
        //    Debug.Log("Controller Not Initilalised");
        //    return;
        //}

        ////var device = SteamVR

        //if (controller.GetPressDown(triggerButton))
        //{
        //    PickupObj();
        //    //SpawnFood();
        //}

        //if (controller.GetPressUp(triggerButton))
        //{
        //    DropObj();
        //}


    }

    void FixedUpdate()
    {
        if (ballThrown)
        {
            //Transform origin;
            //if (trackedObj.origin != null)
            //{
            //    origin = trackedObj.origin;
            //}
            //else
            //{
            //    origin = trackedObj.transform.parent;
            //}

            //if (origin != null)
            //{
            //    rigid.velocity = origin.TransformVector(controller.velocity);
            //    rigid.angularVelocity = origin.TransformVector(controller.angularVelocity * 0.25f);
            //}
            //else
            //{
            //    rigid.velocity = controller.velocity;
            //    rigid.angularVelocity = controller.angularVelocity * 0.25f;
            //}

            //rigid.maxAngularVelocity = rigid.angularVelocity.magnitude;
            //ballThrown = false;

        }
    }

    void OnTriggerStay(Collider other)
    {
        print("TRIGGER");
        if (other.tag == "Ball")
        {
            ball = other.gameObject;

        }
    }

    void OnTriggerExit(Collider other)
    {
        ball = null;


    }
    void PickupObj()
    {
        if (ball != null)
        {
            fixedJoint.connectedBody = ball.GetComponent<Rigidbody>();
            ballThrown = false;
            rigid = null;
        }
        else
        {
            fixedJoint.connectedBody = null;
        }
    }

    void DropObj()
    {
        if (fixedJoint.connectedBody != null)
        {
            rigid = fixedJoint.connectedBody;
            fixedJoint.connectedBody = null;
            ballThrown = true;
        }

    }





}

