using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class testCorgiScript : MonoBehaviour {

    // Valve.VR.EVRButtonId dPadDown = EVRButtonId.k_EButton_DPad_Down;
    private Animator anim;
    private string animatorName;
    private float crossfadeVal = 0.001f;

    public GameObject headSetTarget;
    public GameObject dogBowlWaypoint;
    public GameObject agent;
    public GameObject nose;
    public GameObject rayPoint;
    public GameObject bowlWaypoint;
    public float arrivalRadius;
    public float MaxSpeed = 3.5f;
    public bool called;
    public float callRadius = 1.4f;
    public float stopRadius = 1.25f; //0.375f;
    public Vector3 desiredVelocity = Vector3.zero;
    public Vector3 rayHitPos = Vector3.zero;
    Rigidbody rigidbody;
    public int numPellets;
    public int inBowl;
    Vector3 targetPosition;
    bool inMotion;
    public bool currentlyEating;

    public GameObject dogParent;
    public GameObject foodBox;

    public GameObject ball;

    public bool dogEat;
    public bool calledDog;

    public float bowlNum = 0.75f;
    public float eatNum = 0.38f;
    public AudioSource dogAudioSource;
    public AudioClip bark;
    public AudioClip dogEatingSound;
    public AudioClip dogPanting;
    public AudioClip dogFootsteps;
    public AudioClip dogWhistle;

    public AudioManager audioManager;

    //private SteamVR_TrackedObject trackedObj;
    //private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    public enum dogState
    {
        Walking,
        Eating,
        Idle, 
        Sitting,
        Drinking
    }
    public dogState animState;

    // Use this for initialization
    void Start() {
        
        anim = GetComponent<Animator>();
        animatorName = anim.name;
        animState = dogState.Idle;
        dogAudioSource = GetComponent<AudioSource>();
        rigidbody = GetComponent<Rigidbody>();
        audioManager = FindObjectOfType<AudioManager>();
        //dogAudioSource.PlayOneShot(dogWhistle);

    }

    // Update is called once per frame
    void Update()
    {

       //print("thing = " + Vector3.Distance(new Vector3(headSetTarget.transform.position.x, 0.0f, headSetTarget.transform.position.z), transform.position));

        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);

        //Animation ENums
        if (animState == dogState.Idle)
        {
            //audioManager.StopAllSFX();
            audioManager.PlayOnce("DogPanting");
            anim.SetFloat("Move", 0.0f);
            anim.SetBool("eating", false);
            anim.SetBool("drinking", false);
            
        }
        if (animState == dogState.Walking)
        {
            //audioManager.StopAllSFX();
            audioManager.PlayOnce("DogFootsteps");
            anim.SetFloat("Move", 2.5f);
           
        }
        if (animState == dogState.Eating)
        {
            //audioManager.StopAllSFX();
            audioManager.PlayOnce("DogEating");
            anim.CrossFade("Corgi@CorgiEatV2", crossfadeVal);
            anim.SetFloat("Move", 0.0f);
            anim.SetBool("eating", true);
           
        }

        if (animState == dogState.Drinking)
        {
           
            anim.SetBool("drinking", true);

        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            print("Mouse0");
        }
       
        if (GameObject.FindGameObjectWithTag("foodPellet") != null)
        {
            StartCoroutine(WaitForPellets());
        }
        else if (Vector3.Distance(transform.position, bowlWaypoint.transform.position) < bowlNum && currentlyEating == true)
        {
            print("test2");
            currentlyEating = false;
            animState = dogState.Idle;
            inBowl = 0;

        }

        if (inBowl >= 1 && currentlyEating == false)
        {

            dogEat = true;

        }

        if (calledDog && !dogEat && Vector3.Distance(new Vector3(headSetTarget.transform.position.x, 0.0f, headSetTarget.transform.position.z), transform.position) > 1f)
        {
            
            inMotion = true;
            //dogAudioSource.PlayOneShot(dogWhistle);

            Vector3 steeringVelocity = Vector3.zero;
            DogMovement(transform.position, new Vector3(headSetTarget.transform.position.x, 0.0f, headSetTarget.transform.position.z));
            transform.position += desiredVelocity * Time.deltaTime;
        }

        if (dogEat && !calledDog)
        {
            stopRadius = eatNum;
            DogMovement(transform.position, bowlWaypoint.transform.position);
            transform.position += desiredVelocity * Time.deltaTime;
        }
    }
    //void CallDog()
    //{
    //    called = true;
    //    inMotion = true;
    //    targetPosition = target.transform.position;
    //    dogAudioSource.PlayOneShot(dogWhistle);
    //    Vector3 steeringVelocity = Vector3.zero;
    //}

    //void DogEat()
    //{
    //    called = true;
    //    targetPosition = bowlWaypoint.transform.position;
    //    stopRadius = 0.38f;
    //    Vector3 agentPosition = eatPoint.transform.postion;
    //}

    private void DogMovement(Vector3 agent, Vector3 target)
    {
        //print("bool =  " + (Vector3.Distance(agent, target) < stopRadius));
        //print("float = " + Vector3.Distance(agent, target));
        if (Vector3.Distance(agent, target) > arrivalRadius)
            {
                animState = dogState.Walking;
                if (inMotion)
                {
                    target = new Vector3(target.x, 0f, target.z);
                }
                desiredVelocity = Vector3.Normalize(target - agent) * MaxSpeed;
            }
            else
            {
                if (inMotion)
                {
                    target = new Vector3(target.x, 0f, target.z);
                }
                desiredVelocity = Vector3.Normalize((target - agent) * (MaxSpeed * ((Vector3.Distance(agent, target)) / arrivalRadius)));
                animState = dogState.Walking;

            }
        if (Vector3.Distance(agent, target) < stopRadius)
        {
            
            if (inMotion)
            {
                animState = dogState.Idle;
            }
            if (desiredVelocity.x > 0 || desiredVelocity.y > 0)
            {
                desiredVelocity = desiredVelocity / 2;
            }
            else
                desiredVelocity.x -= 0.0001f;
            desiredVelocity.y -= 0.0001f;
            desiredVelocity.z -= 0.0001f;
            calledDog = false;
            dogEat = false;
            inMotion = false;
        }
        else
        {
            //print("agent/target =  " + Vector3.Distance(agent, target));
        }

            if (desiredVelocity.sqrMagnitude > 0.0f)
            {
                transform.forward = Vector3.Normalize(new Vector3(desiredVelocity.x, desiredVelocity.y, desiredVelocity.z));
            }

        
    }

    IEnumerator WaitForPellets()
    {
        yield return new WaitForSeconds(1.75f);
        if (Vector3.Distance(transform.position, bowlWaypoint.transform.position) < eatNum)
        {
            currentlyEating = true;
            animState = dogState.Eating;
            //transform.forward = -1 * (Vector3.Normalize(bowlWaypoint.transform.position));
            //transform.LookAt(bowlWaypoint.transform.localPosition);
        }
        
    }

}



