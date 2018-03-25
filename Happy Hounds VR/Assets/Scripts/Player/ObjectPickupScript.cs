using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

public class ObjectPickupScript: MonoBehaviour {
    private Valve.VR.EVRButtonId controllerTrigger = EVRButtonId.k_EButton_SteamVR_Trigger;
    private Valve.VR.EVRButtonId squeezePads = EVRButtonId.k_EButton_Grip;
    private SteamVR_TrackedObject steamVRTrackedObject;

    [SerializeField]
    private FixedJoint viveJoint;
    private SteamVR_Controller.Device viveCont { get { return SteamVR_Controller.Input((int)steamVRTrackedObject.index); } }

    private GameObject interactObject;
    public GameObject foodPellet;
    public GameObject SpawnPoint;
    public GameObject foodBox;
    public GameObject VR;
    public GameObject dog;
    DogGrav _dogGrav;

    public CorgiScript _corgiScript;
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
    private bool objThrown;
    public bool petting;
    public bool aggro;
    public bool holdingBall;
    public GameObject corgi;
    GravityButton _gravityButton;

    // Use this for initialization
    void Start()
    {
        steamVRTrackedObject = GetComponent<SteamVR_TrackedObject>();
        //controller = SteamVR_Controller.Input((int)trackedObj.index);
        viveJoint = GetComponent<FixedJoint>();
        _corgiScript = GameObject.FindGameObjectWithTag("Corgi").GetComponent<CorgiScript>();
        _dogGrav = GameObject.FindGameObjectWithTag("Corgi").GetComponent<DogGrav>();
        corgi = GameObject.FindGameObjectWithTag("Corgi");
        _gravityButton = GameObject.FindGameObjectWithTag("GravityButton").GetComponent<GravityButton>();
    }

    // Update is called once per frame
    void Update()
    {
        //print("eating = " + testScript.currentlyEating + " box = " +  holdingBox);
        //DisNumPellets = testScript.numPellets;
        
        if (viveCont == null)
        {
            Debug.Log("Controller Not Initilalised");
            return;
        }
        //print(Vector3.Distance(new Vector3(testScript.headSetTarget.transform.position.x, 0.0f, testScript.headSetTarget.transform.position.z), testScript.transform.position) );
        //var device = SteamVR
        if (viveCont.GetPressDown(controllerTrigger))
        {
            PickupObj();
        }
        if (viveCont.GetPressUp(controllerTrigger))
        {
            DropObj();
        }
        if (viveCont.GetPressDown(squeezePads) && _corgiScript.currentlyEating == false && Vector3.Distance(new Vector3(_corgiScript.headSetTarget.transform.position.x, 0.0f, _corgiScript.headSetTarget.transform.position.z), _corgiScript.transform.position) > _corgiScript.callRadius && _gravityButton.grav )
        {
            //print("button down");
            // _corgiScript.stopRadius = 1.25f;
            // _corgiScript.calledDog = true;
            _corgiScript.DogMovement(corgi.transform.position, new Vector3(_corgiScript.headSetTarget.transform.position.x, 0.0f, _corgiScript.headSetTarget.transform.position.z));
            _corgiScript.animState = CorgiScript.dogState.Walking;
            FindObjectOfType<AudioManager>().PlayOnce("DogWhistle");
        }
        pourTime -= Time.deltaTime;
        if (pourTime <= 0)
        {
            if (_corgiScript.currentlyEating == false)
            {
                if ((Mathf.Abs(foodBox.transform.rotation.x) > 0.60f))
                {
                    FindObjectOfType<AudioManager>().PlaySoundFX("FoodLeaveBox");
                    pouring = true;
                    createFood();
                    pourTime = 0.5f;
                    _corgiScript.numPellets++;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (objThrown)
        {
            Transform contTransform;
            if (steamVRTrackedObject.origin != null)
            {
                contTransform = steamVRTrackedObject.origin;
            }
            else
            {
                contTransform = steamVRTrackedObject.transform.parent;
            }

            if (contTransform != null)
            {
                rigid.velocity = contTransform.TransformVector(viveCont.velocity);
                rigid.angularVelocity = contTransform.TransformVector(viveCont.angularVelocity * 0.25f);
            }
            else
            {
                rigid.velocity = viveCont.velocity;
                rigid.angularVelocity = viveCont.angularVelocity * 0.25f;
            }

            rigid.maxAngularVelocity = rigid.angularVelocity.magnitude;
            objThrown = false;
        }

    }
    void createFood()
   {
        GameObject food = (GameObject)Instantiate(foodPellet, SpawnPoint.transform.position, transform.rotation);
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RocketDoor")
        {
            VR.transform.position = new Vector3(-12.91f, -7.72f, 6.63f);
            dog.transform.position = new Vector3(8.5f, 5.325f, -0.8f);
        }
        //if (other.tag == "Lever")
        //{
        //    SceneManager.LoadScene("TheBeachPlanet");
        //}
    }


    void OnTriggerStay(Collider other)
    {
        if (other.tag == "FoodBox")
        {
            interactObject = other.gameObject;
            holdingBox = true;
        }
        if (other.tag == "Pickupable")
        {
            interactObject = other.gameObject;
        }

        if(other.tag =="Ball")
        {
            interactObject = other.gameObject;
            holdingBall = true;
        }
        if (other.gameObject.tag == "Corgi" || other.gameObject.tag == "corgi")
        {
            print("CorgiCollision");
            if (!_corgiScript.currentlyEating)
            {
                petting = true;
                viveCont.TriggerHapticPulse(3999);
                _corgiScript.animState = CorgiScript.dogState.Petting;
            }
            else
            {
                aggro = true;
                viveCont.TriggerHapticPulse(3999);
                _corgiScript.animState = CorgiScript.dogState.Aggro;
            }
            _corgiScript.lastInteraction = 0f;
        }
    }

    void PickupObj()
    {

        if (interactObject != null)
        {
            viveJoint.connectedBody = interactObject.GetComponent<Rigidbody>();
            objThrown = false;
            rigid = null;
        }
        else
        {
            viveJoint.connectedBody = null;
        }
    }

    void DropObj()
    {
        if (viveJoint.connectedBody != null && !holdingBall)
        {
            rigid = viveJoint.connectedBody;
            viveJoint.connectedBody = null;
            objThrown = true;
        }

        if (viveJoint.connectedBody != null && holdingBall)
        {
            rigid = viveJoint.connectedBody;
            viveJoint.connectedBody = null;
            objThrown = true;
            holdingBall = false;
            _corgiScript.ballThrown = true;
        }

    }


    void OnTriggerExit(Collider other)
    {
        interactObject = null;

        if (other.gameObject.tag == "FoodBox")
        {
            holdingBox = false;
        }
       
        petting = false;
        aggro = false;

        if (other.gameObject.tag == "Corgi" || other.gameObject.tag == "corgi")
        {
            aggro = false;
            petting = false;
            _corgiScript.animState = CorgiScript.dogState.Idle;
            print("state back to idle");
        }
    }
}
