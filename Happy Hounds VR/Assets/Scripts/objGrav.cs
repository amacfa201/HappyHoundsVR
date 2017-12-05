using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objGrav : MonoBehaviour {

    Rigidbody rigid;
    gravityButton gravScript;
    bool localGrav;
    //public bool onDog = false;

    // Use this for initialization
    void Start() {
        gravScript = GameObject.FindGameObjectWithTag("GravityButton").GetComponent<gravityButton>();
        //if(!onDog)
        //{
        //    rigid = GetComponent<Rigidbody>();
        //}
        rigid = GetComponent<Rigidbody>();
        print("start");
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!gravScript.grav)// Gravity is turned off
        {
            LoseGrav();
            print("grav = false");
        }
        else
        {
            ResetRigid();
            //localGrav = true;
        }
        //print("OnDOG= " + onDog);
    }



    public void ResetRigid()
    {
        if (localGrav)
        {
           // if (rigid != null)
            //{

               // if (onDog)
              //  {
                    //Destroy(this.gameObject.GetComponent<Rigidbody>());
               // }

                //rigid.mass = rigid.mass * 6f;

                //if (!onDog)
                //{
                    rigid.useGravity = true;
                //}
                
                localGrav = false;
           // }
        }

    }

    public void LoseGrav()
    {
        if (!localGrav)
        {
            // rigid.mass = rigid.mass / 6f;

            //if (onDog)
           // {
                //if (gravScript.grav)
                // {

                // }
               // if (rigid == null)
                //{ 
                    //this.gameObject.AddComponent<Rigidbody>();
                    //rigid = GetComponent<Rigidbody>();
                //}
                
            //}

            //if (rigid != null)
            //{
               // print("2");
                rigid.useGravity = false;
                rigid.AddForce(Vector3.up * 15f);
                localGrav = true;
            //}

        }
    
    }
}

