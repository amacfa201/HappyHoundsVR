using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviours : MonoBehaviour {
    int actionNum;
    wanderScript _wander;
    public testCorgiScript _CorgiScript;
    public float timeSinceChange;
    public float timeLimit;
    float limiter = 0;
    float limit;
    int animNum;
    gravityButton gravScript;
    
   

// Use this for initialization
    void Start () {
        gravScript = GameObject.FindGameObjectWithTag("GravityButton").GetComponent<gravityButton>();
        _wander = GetComponent<wanderScript>();
        timeLimit = Random.Range(3.5f, 7);
        limit = Random.Range(1, 5);
    }
	
	// Update is called once per frame
	void Update () {
        //print("Grav = " + gravScript.grav);
        timeSinceChange += Time.deltaTime;
        if (timeSinceChange > timeLimit && gravScript.grav)
        {
           timeSinceChange = 0;
           actionNum = Random.Range(1, 3);
        }
        //StartCoroutine(pickAction());
        //print("AN = " + actionNum);
        if (actionNum == 1) // wander 
        {
            _CorgiScript.ResetAnimVal();
            _wander.enabled = true;
            _CorgiScript.animState = testCorgiScript.dogState.Walking;
        }

        if (actionNum == 2)// RandomAnimation;
        {
            limiter += Time.deltaTime;
            _wander.enabled = false;
            if (limiter > limit)
            {
              limiter = 0;
              animNum = Random.Range(1, 3);
            }
            
            _CorgiScript.animState = testCorgiScript.dogState.Idle;
            _CorgiScript.IdleAnimations(animNum);
        }
	}

    //IEnumerator pickAction()
    //{
    //    yield return new WaitForSeconds(8f);
    //    actionNum = Random.Range(1, 3);
    //}
}
