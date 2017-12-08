using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityButton : MonoBehaviour
    
{
    private Animator anim;
    public testCorgiScript _testScript;

    public bool grav = true;

    // Use this for initialization
    void Start()
    {
        _testScript = GameObject.FindGameObjectWithTag("Corgi").GetComponent<testCorgiScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "GameController")
        {

            if (grav)
            {

                grav = false;
                _testScript.animState = testCorgiScript.dogState.Floating;
                

            }
            else
            {
                grav = true;
                _testScript.animState = testCorgiScript.dogState.Idle;
            }
        }

    }

}
