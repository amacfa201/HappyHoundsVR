using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pitchingButton : MonoBehaviour {
    public bool on;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "GameController")
        {

            if (on)
            {
                on = false;
            }
            else
            {
                on = true;
            }
        }

    }

}
