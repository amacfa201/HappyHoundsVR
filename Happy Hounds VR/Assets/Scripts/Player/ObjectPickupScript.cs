using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ObjectPickupScript: MonoBehaviour {
    private Valve.VR.EVRButtonId triggerButton = EVRButtonId.k_EButton_SteamVR_Trigger;
    private Valve.VR.EVRButtonId squeezePads = EVRButtonId.k_EButton_Grip;
    private SteamVR_TrackedObject trackedObj;

    [SerializeField]
    private FixedJoint fixedJoint;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

    private GameObject obj;
    public GameObject foodPellet;
    public GameObject SpawnPoint;
    public GameObject foodBox;

    public CorgiScript testScript;
    List<GameObject> foodList;

    private Rigidbody rigid;

    public int maxPellets = 20;
    public int DisNumPellets;

    public float pourTime = 0.5f;
    //public AudioSource foodSource;
    //public AudioClip thud; // when box is dropped
    //conall
    //public AudioClip boxShake;
    //public AudioClip foodinBowl;
    public bool pouring = false;
    public bool holdingBox;
    private bool thrown;
    public bool petting;
    public bool aggro;
    public bool holdingBall;

    // Use this for initialization
    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        //controller = SteamVR_Controller.Input((int)trackedObj.index);
        fixedJoint = GetComponent<FixedJoint>();
        //testScript = GameObject.FindGameObjectWithTag("corgi").GetComponent<testCorgiScript>();
    }

    // Update is called once per frame
    void Update()
    {
        print("eating = " + testScript.currentlyEating + " box = " +  holdingBox);
        //DisNumPellets = testScript.numPellets;
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
            //print("button down");
            testScript.stopRadius = 1.25f;
            testScript.calledDog = true;
            FindObjectOfType<AudioManager>().PlayOnce("DogWhistle");
        }
        pourTime -= Time.deltaTime;
        if (pourTime <= 0)
        {
            if (testScript.currentlyEating == false && holdingBox == true)
            {
                if ((Mathf.Abs(foodBox.transform.rotation.x) > 0.60f))
                {
                    FindObjectOfType<AudioManager>().PlaySound("FoodLeaveBox");
                    pouring = true;
                    createFood();
                    pourTime = 0.5f;
                    testScript.numPellets++;
                }
            }
        }
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
        if (other.tag == "FoodBox")
        {
            obj = other.gameObject;
            holdingBox = true;
        }
        if (other.tag == "Pickupable")
        {
            obj = other.gameObject;
        }

        if(other.tag =="Ball")
        {
            obj = other.gameObject;
            holdingBall = true;
        }
        if (other.gameObject.tag == "Corgi" || other.gameObject.tag == "corgi")
        {
            print("CorgiCollision");
            if (!testScript.currentlyEating)
            {
                petting = true;
                controller.TriggerHapticPulse(3999);
                testScript.animState = CorgiScript.dogState.Petting;
            }
            else
            {
                aggro = true;
                controller.TriggerHapticPulse(3999);
                testScript.animState = CorgiScript.dogState.Aggro;
            }
            testScript.lastInteraction = 0f;
        }
    }

    void PickupObj()
    {

        if (obj != null)
        {
            fixedJoint.connectedBody = obj.GetComponent<Rigidbody>();
            thrown = false;
            rigid = null;
        }
        else
        {
            fixedJoint.connectedBody = null;
        }
    }

    void DropObj()
    {
        if (fixedJoint.connectedBody != null && !holdingBall)
        {
            rigid = fixedJoint.connectedBody;
            fixedJoint.connectedBody = null;
            thrown = true;
        }

        if (fixedJoint.connectedBody != null && holdingBall)
        {
            rigid = fixedJoint.connectedBody;
            fixedJoint.connectedBody = null;
            thrown = true;
            holdingBall = false;
            testScript.ballThrown = true;
        }

    }

    IEnumerator spawnFood()
    {
        yield return new WaitForSeconds(2f);
        createFood();
        
    }

    void OnTriggerExit(Collider other)
    {
        obj = null;
        holdingBox = false;
        petting = false;
        aggro = false;

        if (other.gameObject.tag == "Corgi" || other.gameObject.tag == "corgi")
        {
            aggro = false;
            petting = false;
            testScript.animState = CorgiScript.dogState.Idle;
            print("state back to idle");
        }
    }
}
