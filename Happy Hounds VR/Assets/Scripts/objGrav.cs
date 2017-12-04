using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objGrav : MonoBehaviour {

    Rigidbody rigid;
    gravityButton gravScript;
    bool localGrav;

    // Use this for initialization
    void Start() {
        rigid = GetComponent<Rigidbody>();
        gravScript = GameObject.FindGameObjectWithTag("GravityButton").GetComponent<gravityButton>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!gravScript.grav)
        {
            LoseGrav();
        }
        else
        {
            ResetRigid();
        }
    }



    public void ResetRigid()
    {
        if (localGrav)
        {
            //rigid.mass = rigid.mass * 6f;
            rigid.useGravity = true;
            localGrav = false;
        }

    }


    public void LoseGrav()
    {
        if (!localGrav)
        {
            //rigid.mass = rigid.mass / 6f;
     
            rigid.useGravity = false;
            rigid.AddForce(Vector3.up * 15f);
            localGrav = true;
        }
    
    }
}

