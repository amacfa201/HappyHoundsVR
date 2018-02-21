using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGravScript : MonoBehaviour
{
    Rigidbody rigid;
    GravityButton gravScript;
    [SerializeField]
    bool localGrav;
    //public bool onDog = false;

    // Use this for initialization
    void Start()
    {
        gravScript = GameObject.FindGameObjectWithTag("GravityButton").GetComponent<GravityButton>();
        //if(!onDog)
        //{
        //    rigid = GetComponent<Rigidbody>();
        //}
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gravScript.grav)// Gravity is turned off
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