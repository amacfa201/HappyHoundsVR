using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foodPelletScript : MonoBehaviour {

    //private float deleteTime = 5f; 
    public testCorgiScript testScript;

    // Use this for initialization
    void Start () {
        testScript = GameObject.FindGameObjectWithTag("Corgi").GetComponent<testCorgiScript>();
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -1.5f)
        {
            Destroy(gameObject);
        }
	}


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "plane"  || other.gameObject.tag == "corgi")
        {
            Destroy(gameObject, 0.05f);
            if (testScript.inBowl > 0)
            {
                testScript.inBowl--;
            }
        }

        if (other.gameObject.tag == "dogBowl")
        {
            testScript.inBowl++;
            FindObjectOfType<AudioManager>().PlaySound("FoodHitBowl");
        }

        //if (other.gameObject.tag == "noseCollider")
        //{

        //    Destroy(gameObject, 0.4f);
        //    if (testScript.inBowl > 0)
        //    {
        //        testScript.inBowl--;
        //    }
        //}

    }
}
