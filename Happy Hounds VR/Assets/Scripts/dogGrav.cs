using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dogGrav : MonoBehaviour
{

    Rigidbody rigid;
    gravityButton gravScript;
    bool localGrav;
    //public bool onDog = false;

    // Use this for initialization
    void Start()
    {
        gravScript = GameObject.FindGameObjectWithTag("GravityButton").GetComponent<gravityButton>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gravScript.grav)// Gravity is turned off
        {
            
            
            LoseGrav();
            //print("grav = false");
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
            
            rigid.useGravity = true;
            Destroy(rigid, 3f);
            localGrav = false;
         
        }

    }

    public void LoseGrav()
    {
        if (!localGrav)
        {
            this.gameObject.AddComponent<Rigidbody>();
            rigid = GetComponent<Rigidbody>();
            rigid.useGravity = false;
            rigid.AddForce(Vector3.up * 15f);
            localGrav = true;

        }

    }
}