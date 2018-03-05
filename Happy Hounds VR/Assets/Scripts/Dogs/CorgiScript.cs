using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CorgiScript : MonoBehaviour {

    // Valve.VR.EVRButtonId dPadDown = EVRButtonId.k_EButton_DPad_Down;
    private Animator anim;

    public GameObject headSetTarget;
    public GameObject agent;
    public GameObject nose;
    public GameObject exterior;
    public GameObject bowlWaypoint;
    public GameObject foodBox;
    public GameObject ball;

    public GameObject testTarget;

    public PettingScript _pettingScript;
    public ObjectPickupScript _objectPickup;

    public TraversePath moveScript;

    public AudioManager audioManager;

    GravityButton gravScript;
    public LayerMask corgiMask;

    public Vector3 desiredVelocity = Vector3.zero;
    public Vector3 rayHitPos = Vector3.zero;
    Vector3 targetPosition;

    Rigidbody myRigid;
    public int numPellets;
    public int inBowl;

    public float arrivalRadius;
    public float MaxSpeed = 3.5f;
    public float lastInteraction;
    private float crossfadeVal = 0.001f;
    public float callRadius = 1.4f;
    public float stopRadius = 1.25f; //0.375f;
    public float bowlNum = 0.75f;
    public float bowlRadius = 0.45f;
    public float ballRadius = 0.48f;
    public float randInteractionTime;
    public float fetchNum = 0.48f;

    public bool idling;
    public bool dogEat;
    public bool calledDog;
    public bool currentlyEating;
    public bool ballThrown;
    public bool ballCollected;
    public bool ballDropped;
    public bool fetching;
    public bool called;
    public bool activateIdleState;
    bool inMotion;

   

    private string animatorName;
    //private SteamVR_TrackedObject trackedObj;
    //private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    public enum dogState
    {
        Walking,
        Eating,
        Idle,
        Sitting,
        Drinking,
        Petting,
        Fetching,
        Floating,
        Aggro
    }
    public dogState animState;

    // Use this for initialization
    void Start() {
        exterior.SetActive(true);
        anim = GetComponent<Animator>();
        animatorName = anim.name;
        animState = dogState.Idle;
        //rigid = GetComponent<Rigidbody>();
        audioManager = FindObjectOfType<AudioManager>();
        //dogAudioSource.PlayOneShot(dogWhistle);
        _pettingScript = GetComponent<PettingScript>();
        randInteractionTime = Random.Range(5, 12);
        gravScript = GameObject.FindGameObjectWithTag("GravityButton").GetComponent<GravityButton>();
        moveScript = GetComponent<TraversePath>();
        moveScript.MoveTo(transform.position, testTarget.transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        lastInteraction += Time.deltaTime;
        //transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        //Animation ENums
        SetDogStates();
        CheckDogEat();
        //moveScript.Wander();

        //RAND ANIM STUFF
        if (activateIdleState)
        {
            if (lastInteraction > randInteractionTime)
            {
                GetComponent<IdleBehaviours>().enabled = true;
            }
            else
            {
                GetComponent<IdleBehaviours>().enabled = false;
            }

            if (!GetComponent<IdleBehaviours>().enabled)
            {
                anim.SetFloat("idle", 0f);
            }
        }

        ////////////////BALL//////////////////////
        #region attempt2
        if (ballThrown && !currentlyEating)
        {
            ball.AddComponent<Rigidbody>();
            fetching = true;
            //lastInteraction = 0;
            //ResetRand();
            inMotion = true;
            if (gravScript.grav)
            {
                DogMovement(transform.position, new Vector3(ball.transform.position.x, ball.transform.position.y, ball.transform.position.z));
            }
            else
            {
                ball.GetComponent<Rigidbody>().useGravity = false;
                DogMovement(transform.position, new Vector3(ball.transform.position.x, ball.transform.position.y, ball.transform.position.z));
            }
            transform.position += desiredVelocity * Time.deltaTime;
        }

        if (ballThrown && Vector3.Distance(transform.position, ball.transform.position) <= fetchNum)
        {
            ball.transform.position = nose.transform.position;
            ballRadius = 0.24f;
            lastInteraction = 0;
            ResetRand();
            ballThrown = false;
            ballCollected = true;
            fetching = false;
        }
        if (ballCollected)
        {
            fetching = false;
            //lastInteraction = 0;
            //ResetRand();
            Destroy(ball.GetComponent<Rigidbody>());
            ball.transform.position = nose.transform.position;
            inMotion = true;
            if (gravScript.grav)
            {
                DogMovement(transform.position, new Vector3(headSetTarget.transform.position.x, 0.0f, headSetTarget.transform.position.z));
            }
            else
            {
                DogMovement(transform.position, new Vector3(headSetTarget.transform.position.x, 0.0f, headSetTarget.transform.position.z));
            }
            transform.position += desiredVelocity * Time.deltaTime;
            ballDropped = true;
        }

        if (Vector3.Distance(new Vector3(headSetTarget.transform.position.x, 0.0f, headSetTarget.transform.position.z), transform.position) < ballRadius && ballDropped == true)
        {
            ball.AddComponent<Rigidbody>();
            ball.GetComponent<Rigidbody>().useGravity = true;
            ball.transform.position = new Vector3(headSetTarget.transform.position.x, 0.05f, headSetTarget.transform.position.z);
            ballRadius = 0;
            ballCollected = false;
            ballThrown = false;
            //lastInteraction = 0;
            //ResetRand();
            ballDropped = false;
        }

        ///////////////////////////////////////////////////
        if (calledDog && !dogEat && !fetching && Vector3.Distance(new Vector3(headSetTarget.transform.position.x, 0.0f, headSetTarget.transform.position.z), transform.position) > 1f)
        {
            lastInteraction = 0;
            ResetRand();
            inMotion = true;
            //dogAudioSource.PlayOneShot(dogWhistle);

            Vector3 steeringVelocity = Vector3.zero;
            DogMovement(transform.position, new Vector3(headSetTarget.transform.position.x, 0.0f, headSetTarget.transform.position.z));
           //transform.position += desiredVelocity * Time.deltaTime;
        }

        if (dogEat && !calledDog && !fetching)
        {
            lastInteraction = 0;
            ResetRand();
            DogMovement(transform.position, new Vector3 (bowlWaypoint.transform.position.x, bowlWaypoint.transform.position.y, bowlWaypoint.transform.position.z));
           
            //transform.position += desiredVelocity * Time.deltaTime;
        }
        #endregion
       
    }

    private void CheckDogEat()
    {
        if (GameObject.FindGameObjectWithTag("foodPellet") != null)
        {
            lastInteraction = 0;
            ResetRand();
            StartCoroutine(WaitForPellets());
        }
        else if (Vector3.Distance(transform.position, bowlWaypoint.transform.position) < bowlNum && currentlyEating == true)
        {
            moveScript.StopCoroutine("FollowPath");
            print("test2");
            currentlyEating = false;
            animState = dogState.Idle;
            inBowl = 0;
        }

        if (inBowl == 0 && !currentlyEating && !fetching)
        {
            lastInteraction = 0;
            ResetRand();
            dogEat = true;
        }
    }

    private void SetDogStates()
    {
        if (animState == dogState.Idle)
        {
            //audioManager.StopAllSFX();
            //audioManager.PlayOnce("DogPanting");
            anim.SetFloat("petting", 0f);
            anim.SetBool("Aggro", false);
            anim.SetFloat("Move", 0.0f);
            anim.SetBool("eating", false);
            anim.SetBool("drinking", false);
            anim.SetBool("isFloating", false);
        }
        if (animState == dogState.Walking)
        {
            //audioManager.StopAllSFX();
            // audioManager.PlayOnce("DogFootsteps");
            anim.SetFloat("Move", 2.5f);
            //  audioManager.PlayOnce("DogFootsteps");
        }
        if (animState != dogState.Floating)
        {
            anim.SetBool("isFloating", false);
        }
        if (animState == dogState.Eating)
        {
            lastInteraction = 0;
            ResetRand();
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
        if (animState == dogState.Floating)
        {
            anim.SetBool("isFloating", true);
        }
        if (animState == dogState.Petting)
        {
            _pettingScript.RandAnimTime();
            anim.SetFloat("Move", 0.0f);
            anim.SetBool("eating", false);
            anim.SetBool("drinking", false);
            //currentlyEating = false;
            audioManager.PlayOnce("DogPanting");
            if (_pettingScript.currentAnim == "corgipettingstand1")
            {
                anim.SetFloat("petting", 0.5f);
            }
            if (_pettingScript.currentAnim == "corgibackscratch")
            {
                anim.SetFloat("petting", 1f);
            }
            if (_pettingScript.currentAnim == "Take 001")
            {
                anim.SetFloat("petting", 1.5f);
            }
        }
        if (animState == dogState.Aggro)
        {
            audioManager.PlayOnce("DogGrowl");
            currentlyEating = false;
            anim.SetFloat("petting", 0f);
            anim.SetFloat("Move", 0.0f);
            anim.SetBool("eating", false);
            anim.SetBool("drinking", false);
            anim.SetBool("isFloating", false);
            anim.SetBool("Aggro", true);
        }
    }

    private void DogMovement(Vector3 agent, Vector3 target)
    {
        #region OGMovement
        //lastInteraction = 0;
        //if (Vector3.Distance(agent, target) > arrivalRadius)
        //{
        //    animState = dogState.Walking;
        //    if (inMotion)
        //    {
        //        target = new Vector3(target.x, 0f, target.z);
        //    }
        //    desiredVelocity = Vector3.Normalize(target - agent) * MaxSpeed;
        //}
        //else
        //{
        //    if (inMotion)
        //    {
        //        if (gravScript.grav)
        //        {
        //            target = new Vector3(target.x, 0f, target.z);
        //        }
        //        else
        //        {
        //            target = new Vector3(target.x, target.y, target.z);
        //        }
        //    }
        //    desiredVelocity = Vector3.Normalize((target - agent) * (MaxSpeed * ((Vector3.Distance(agent, target)) / arrivalRadius)));
        //    animState = dogState.Walking;

        //}
        //if (Vector3.Distance(agent, target) < stopRadius)
        //{
        //    if (inMotion)
        //    {
        //        animState = dogState.Idle;
        //    }
        //    if (desiredVelocity.x > 0 || desiredVelocity.y > 0)
        //    {
        //        desiredVelocity = desiredVelocity / 2;
        //    }
        //    else
        //        desiredVelocity.x -= 0.0001f;
        //    desiredVelocity.y -= 0.0001f;
        //    desiredVelocity.z -= 0.0001f;
        //    calledDog = false;
        //    dogEat = false;
        //    inMotion = false;
        //}
        //else
        //{
        //    //print("agent/target =  " + Vector3.Distance(agent, target));
        //}

        //if (desiredVelocity.sqrMagnitude > 0.0f)
        //{
        //    transform.forward = Vector3.Normalize(new Vector3(desiredVelocity.x, desiredVelocity.y, desiredVelocity.z));
        //}
        #endregion

        moveScript.MoveTo(agent, target);
        animState = dogState.Walking;

       
        //if (desiredVelocity.sqrMagnitude > 0.0f)
       // {
           // transform.forward = Vector3.Normalize(target);
       // }
    }

    public void IdleAnimations(int randNum)
    {
        if (randNum == 1)
        {
            anim.SetFloat("idle", 0.5f);
        }
        if (randNum == 2)
        {
            anim.SetFloat("idle", 1f);
        }
    }

     public void ResetAnimVal()
    {
        anim.SetFloat("idle", 0f);
    }

    IEnumerator WaitForPellets()
    {
        yield return new WaitForSeconds(1.75f);
        if (Vector3.Distance(transform.position, bowlWaypoint.transform.position) < bowlRadius)
        {
            currentlyEating = true;
            animState = dogState.Eating;
            //transform.forward = -1 * (Vector3.Normalize(bowlWaypoint.transform.position));
            //transform.LookAt(bowlWaypoint.transform.localPosition);
        }
        
    }

    public void ResetRand()
    {
        lastInteraction = 0f;
        GetComponent<IdleBehaviours>().enabled = false;
        GetComponent<WanderScript>().enabled = false;
    }











































































    void CorgiCheck()
    {
        Debug.Log("Conall");
    }
}



