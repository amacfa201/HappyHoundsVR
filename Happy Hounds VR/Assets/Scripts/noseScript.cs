using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noseScript : MonoBehaviour
{

    public corgiScript testScript;
    // Use this for initialization
    void Start()
    {
        testScript = GameObject.FindGameObjectWithTag("Corgi").GetComponent<corgiScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "foodPellet")
        {
            Destroy(other.gameObject, Random.Range(4.5f, 12.0f));
            if (testScript.animState == corgiScript.dogState.Eating)
            {
                testScript.currentlyEating = true;
                
            }
            if (testScript.inBowl > 0)
            {
                testScript.inBowl--;
            }

        }
    }
}
