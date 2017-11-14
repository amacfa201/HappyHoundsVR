using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class objectPickup : MonoBehaviour {


    private Valve.VR.EVRButtonId triggerButton = EVRButtonId.k_EButton_SteamVR_Trigger;
    private Valve.VR.EVRButtonId squeezePads = EVRButtonId.k_EButton_Grip;
    
    //private SteamVR_Controller.Device controller;
    private SteamVR_TrackedObject trackedObj;

    [SerializeField]
    private GameObject obj;
    private FixedJoint fixedJoint;

    public GameObject foodPellet;
    public GameObject SpawnPoint;
    public GameObject foodBox;
    //public int numPellets;
    public int maxPellets = 20;
    public int DisNumPellets;
    public bool pouring = false;
    public float pourTime = 0.5f;
    public testCorgiScript testScript;
    List<GameObject> foodList;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }


    public bool holdingBox;
    public AudioSource foodSource;
    public AudioClip thud; // when box is dropped
    public AudioClip boxShake;
    public AudioClip foodinBowl;

    private bool thrown;
    private Rigidbody rigid;

    // Use this for initialization
    void Start() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        //controller = SteamVR_Controller.Input((int)trackedObj.index);
        fixedJoint = GetComponent<FixedJoint>();
        testScript = GameObject.FindGameObjectWithTag("corgi").GetComponent<testCorgiScript>();
        
    }

    // Update is called once per frame
    void Update() {

        DisNumPellets = testScript.numPellets;
        if (controller == null)
        {
            Debug.Log("Controller Not Initilalised");
            return;
        }
        //print(Vector3.Distance(new Vector3(testScript.headSetTarget.transform.position.x, 0.0f, testScript.headSetTarget.transform.position.z), testScript.transform.position) );
        //var device = SteamVR

        if (controller.GetPressDown(triggerButton))
        {
            PickupObj();
            //SpawnFood();
        }


        if (controller.GetPressUp(triggerButton))
        {
            DropObj();
        }

        if (controller.GetPressDown(squeezePads) && testScript.currentlyEating == false && Vector3.Distance(new Vector3(testScript.headSetTarget.transform.position.x, 0.0f, testScript.headSetTarget.transform.position.z), testScript.transform.position) > testScript.callRadius)
        {
            print("button down");
            testScript.stopRadius = 1.25f;
            testScript.calledDog = true;
        }


        pourTime -= Time.deltaTime;
        if (pourTime <= 0)
        {
            if (testScript.currentlyEating == false && holdingBox == true)
            {
                if ((Mathf.Abs(foodBox.transform.rotation.x) > 0.5f || Mathf.Abs(foodBox.transform.rotation.z) > 0.5f))
                {
                    pouring = true;
                    createFood();
                    pourTime = 0.5f;
                    testScript.numPellets++;
                }
            }
        }
        //spawnFood();
       
        //print("x = " + Mathf.Abs(foodBox.transform.rotation.x) + "z = " + Mathf.Abs(foodBox.transform.rotation.z));
    }



    void FixedUpdate()
    {
        if (thrown)
        {
            Transform origin;
            if (trackedObj.origin != null)
            {
                origin = trackedObj.origin;
            }
            else
            {
                origin = trackedObj.transform.parent;
            }

            if (origin != null)
            {
                rigid.velocity = origin.TransformVector(controller.velocity);
                rigid.angularVelocity = origin.TransformVector(controller.angularVelocity * 0.25f);
            }
            else
            {
                rigid.velocity = controller.velocity;
                rigid.angularVelocity = controller.angularVelocity * 0.25f;
            }

            rigid.maxAngularVelocity = rigid.angularVelocity.magnitude;
            thrown = false;

        }
    }

   

    void createFood()
   {
        GameObject food = (GameObject)Instantiate(foodPellet, SpawnPoint.transform.position, transform.rotation);
    }


    void OnTriggerStay(Collider other)
    {
        
        if (other.tag == "Pickupable") {
            obj = other.gameObject;
            holdingBox = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        obj = null;
        holdingBox = false;

    }


    void PickupObj() {

        if (obj != null)
        {
            fixedJoint.connectedBody = obj.GetComponent<Rigidbody>();
            thrown = false;
            rigid = null;
        }
        else {
            fixedJoint.connectedBody = null;
        }
    }

    void DropObj()
    {
        if (fixedJoint.connectedBody != null)
        {
            rigid = fixedJoint.connectedBody;
            fixedJoint.connectedBody = null;
            thrown = true;
        }
            
    }



    IEnumerator spawnFood() {
        yield return new WaitForSeconds(2f);
        createFood();
        
    }

}
