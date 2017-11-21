using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterScript : MonoBehaviour {

    private Rigidbody rigid;
    testCorgiScript corgiScript;
    float timeSinceHit;
    float timeAlive;
    private GameObject spawnPoint;

    // Use this for initialization
    void Start () {
        spawnPoint = GameObject.FindGameObjectWithTag("waterSpawn");
        rigid = GetComponent<Rigidbody>();
        corgiScript = GameObject.FindGameObjectWithTag("corgi").GetComponent<testCorgiScript>();
    }
	
	// Update is called once per frame
	void Update () {

        //rigid.AddForce(new Vector3(0,spawnPoint.transform.position.y ,0) * 250.0f);
        //rigid.AddForceAtPosition(Vector3.forward, spawnPoint.transform.position) ;
        timeSinceHit += Time.deltaTime;
        GameObject go = GameObject.Find("waterSpawn");
        rigid.AddForce(go.transform.up * -120.0f);

        timeAlive += Time.deltaTime;

        if (timeAlive > 3f)
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 6f);
        }

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

       

        if(timeSinceHit > 0.75f)
        {
            corgiScript.animState = testCorgiScript.dogState.Idle;
        }

        

    }


}
