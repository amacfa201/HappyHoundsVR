using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButtonScript : MonoBehaviour {

    platformScript _platformScript;
    public string Direction;
    
	// Use this for initialization
	void Start ()
    {
        _platformScript = GameObject.FindGameObjectWithTag("Platform").GetComponent<platformScript>();
	}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "GameController")
        {
            //_platformScript.playerCollider.enabled = true;
            _platformScript.playerRB.isKinematic = false;
            _platformScript.dogRB.isKinematic = false;
            _platformScript.playerRB.useGravity = false;
            if (Direction == "Up")
            {
                _platformScript.MoveUp();
            }
            else
            {
               _platformScript.MoveDown();
            }
        }
    }

     void OnTriggerExit(Collider other)
    {
        if (other.tag == "GameController")
        {
            //_platformScript.playerCollider.enabled = false;
            //_platformScript.playerRB.isKinematic = true;
            _platformScript.StopLift();
            _platformScript.playerRB.isKinematic = true;
            _platformScript.dogRB.isKinematic = true;
        }
    }
    // Update is called once per frame
    void Update ()
    {
		
	}
}
