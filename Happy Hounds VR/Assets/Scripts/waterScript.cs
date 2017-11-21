using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterScript : MonoBehaviour {

    private Rigidbody rigid;
    testCorgiScript corgiScript;
    float timeSinceHit;

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody>();
        corgiScript = GameObject.FindGameObjectWithTag("corgi").GetComponent<testCorgiScript>();
    }
	
	// Update is called once per frame
	void Update () {
        rigid.AddForce(transform.forward * 120.0f);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "plane" || other.gameObject.tag == "corgi")
        {
            Destroy(gameObject, 0.5f);
            if (corgiScript.animState == testCorgiScript.dogState.Walking || corgiScript.animState == testCorgiScript.dogState.Idle)
            {
                corgiScript.animState = testCorgiScript.dogState.Drinking;
                timeSinceHit = 0;
            }
        }

        timeSinceHit += Time.deltaTime;

        if(timeSinceHit > 2f)
        {
            corgiScript.animState = testCorgiScript.dogState.Idle;
        }

        

    }


}
