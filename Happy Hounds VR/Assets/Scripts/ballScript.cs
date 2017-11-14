//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Valve.VR;

//public class ballScript : MonoBehaviour
//{


//    private Valve.VR.EVRButtonId triggerButton = EVRButtonId.k_EButton_SteamVR_Trigger;
   

//    //private SteamVR_Controller.Device controller;
//    private SteamVR_TrackedObject trackedObj;

//    [SerializeField]
//    private GameObject obj;
//    private FixedJoint fixedJoint;
//    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }


    

//    private bool thrown;
//    private Rigidbody rigid;

//    // Use this for initialization
//    void Start()
//    {
//        trackedObj = GetComponent<SteamVR_TrackedObject>();
//        //controller = SteamVR_Controller.Input((int)trackedObj.index);
//        fixedJoint = GetComponent<FixedJoint>();
       

//    }

//    // Update is called once per frame
//    void Update()
//    {

      
//        if (controller == null)
//        {
//            Debug.Log("Controller Not Initilalised");
//            return;
//        }

//        //var device = SteamVR

//        if (controller.GetPressDown(triggerButton))
//        {
//            PickupObj();
//            //SpawnFood();
//        }


//        if (controller.GetPressUp(triggerButton))
//        {
//            DropObj();
//        }

       
//    }



//    void FixedUpdate()
//    {
//        if (thrown)
//        {
//            Transform origin;
//            if (trackedObj.origin != null)
//            {
//                origin = trackedObj.origin;
//            }
//            else
//            {
//                origin = trackedObj.transform.parent;
//            }

//            if (origin != null)
//            {
//                rigid.velocity = origin.TransformVector(controller.velocity);
//                rigid.angularVelocity = origin.TransformVector(controller.angularVelocity * 0.25f);
//            }
//            else
//            {
//                rigid.velocity = controller.velocity;
//                rigid.angularVelocity = controller.angularVelocity * 0.25f;
//            }

//            rigid.maxAngularVelocity = rigid.angularVelocity.magnitude;
//            thrown = false;

//        }
//    }

//    void SpawnFood()
//    {
//    }


   


//    void OnTriggerStay(Collider other)
//    {
//        print("TRIGGER");
//        if (other.tag == "Pickupable")
//        {
//            obj = other.gameObject;
           
//        }
//    }

//    void OnTriggerExit(Collider other)
//    {
//        obj = null;
      

//    }


//    void PickupObj()
//    {

//        if (obj != null)
//        {
//            fixedJoint.connectedBody = obj.GetComponent<Rigidbody>();
//            thrown = false;
//            rigid = null;
//        }
//        else
//        {
//            fixedJoint.connectedBody = null;
//        }
//    }

//    void DropObj()
//    {
//        if (fixedJoint.connectedBody != null)
//        {
//            rigid = fixedJoint.connectedBody;
//            fixedJoint.connectedBody = null;
//            thrown = true;
//        }

//    }



   

//}

