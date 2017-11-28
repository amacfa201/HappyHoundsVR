using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviours : MonoBehaviour {
    int actionNum;
    wanderScript _wander;
    testCorgiScript _CorgiScript;

    // Use this for initialization
    void Start () {
        _wander = GetComponent<wanderScript>();
    }
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(pickAction());
        if (actionNum == 1) // wander 
        {
            _CorgiScript.ResetAnimVal();
            _wander.enabled = true;
            _CorgiScript.animState = testCorgiScript.dogState.Walking;
        }

        if (actionNum == 2)// RandomAnimation;
        {
            _wander.enabled = false;
            int animNum = Random.Range(1, 2);
            _CorgiScript.animState = testCorgiScript.dogState.Idle;
            _CorgiScript.IdleAnimations(animNum);
        }
	}






    IEnumerator pickAction()
    {
        yield return new WaitForSeconds(8f);
        actionNum = Random.Range(1, 2);
    }
}
