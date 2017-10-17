using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testCorgiScript : MonoBehaviour {
    private Animator anim;
    private string animatorName;
    private float crossfadeVal;

    public GameObject target;
    public GameObject agent;
    public float arrivalRadius;
    public float MaxSpeed = 400f;
    public bool called;
    Vector3 desiredVelocity = Vector3.zero;
    

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        animatorName = anim.name;
        print("name " + animatorName);

        
    }
	
	// Update is called once per frame
	void Update () {
       // print("Update");
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("T Pressed");
            anim.CrossFade(animatorName+ "Idle", crossfadeVal);
            anim.SetFloat("Move", 2.5f);
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R Pressed");
            anim.CrossFade(animatorName + "Idle", crossfadeVal);
            anim.SetFloat("Move", 0f);
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C Pressed");
            anim.CrossFade(animatorName + "Idle", crossfadeVal);
            anim.SetFloat("Move", 2.5f);
            CallDog();
        }

        if (called)
        {
            agent.transform.position += desiredVelocity * Time.deltaTime;
        }


        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            print("Mouse0");
        }

    }


    void CallDog()
    {
        called = true;
        Vector3 targetPosition = target.transform.position;
       

        Vector3 steeringVelocity = Vector3.zero;

        if (Vector3.Distance(agent.transform.position, transform.position) > arrivalRadius)
        {
            desiredVelocity = Vector3.Normalize(transform.position - agent.transform.position) * MaxSpeed;
        }
        else
        {
            
            desiredVelocity = Vector3.Normalize(targetPosition - agent.transform.position) * (MaxSpeed * ((Vector3.Distance(transform.position,agent.transform.position)) / arrivalRadius));
        }

        // target.transform.position += desiredVelocity * Time.deltaTime;
     
       // steeringVelocity = desiredVelocity - agent.CurrentVelocity;
    }
}
