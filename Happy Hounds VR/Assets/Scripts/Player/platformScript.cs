using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformScript : MonoBehaviour {

    public GameObject target;
    public GameObject platform;


    public float maxHeight;
    public float minHeight;

    //public Rigidbody platformRB;
    public Rigidbody[] liftObjects;
    //public Rigidbody buttonRB;
   // public Rigidbody buttonRB2;
    public Rigidbody playerRB;
    public Rigidbody dogRB;
    //public GameObject platform;


    public BoxCollider playerCollider;
    public float speed;

    // Use this for initialization
    void LateStart()
    {
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider>();
        //platformRB = GameObject.FindGameObjectWithTag("Platform").GetComponent<Rigidbody>();
        //platformRB2 = GameObject.FindGameObjectWithTag("Platform").GetComponent<Rigidbody>();
        //buttonRB = GameObject.FindGameObjectWithTag("platformButton").GetComponent<Rigidbody>();
        playerRB = GetComponent<Rigidbody>();
        //dogRB = GameObject.FindGameObjectWithTag("Corgi").GetComponent<Rigidbody>();
        //playerCollider.enabled = false;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.P))
        {
            //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            MoveUp(); 
        }
	}
   public void MoveUp()
    {
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        //platform.transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if (platform.transform.position.y < maxHeight)
        {
            //platformRB.AddForce(Vector3.up * speed);
            //buttonRB.AddForce(Vector3.up * speed);
            playerRB.AddForce(Vector3.up * speed);
            //buttonRB2.AddForce(Vector3.up * speed);
            dogRB.AddForce(Vector3.up * speed);
            for (int i = 0; i < liftObjects.Length; i++)
            {
                liftObjects[i].AddForce(Vector3.up * speed);
            }
        }
        else
        {
            //Vector3 holdPos = transform.position;
            //transform.position = holdPos;
            //platformRB.velocity = Vector3.zero;
            //buttonRB.velocity = Vector3.zero;
            playerRB.velocity = Vector3.zero;
            //buttonRB2.velocity = Vector3.zero;
            dogRB.velocity = Vector3.zero;
            for (int i = 0; i < liftObjects.Length; i++)
            {
                liftObjects[i].velocity = Vector3.zero;
            }
        }
    }

    public void StopLift() {
        //platformRB.velocity = Vector3.zero;
        //buttonRB.velocity = Vector3.zero;
        playerRB.velocity = Vector3.zero;
        //buttonRB2.velocity = Vector3.zero;
        dogRB.velocity = Vector3.zero;
        for (int i = 0; i < liftObjects.Length; i++)
        {
            liftObjects[i].velocity = Vector3.zero;
        }
    }

    public void MoveDown()
    {

        if (platform.transform.position.y > minHeight)
        {
            //platformRB.AddForce(-Vector3.up * speed);
            //buttonRB.AddForce(-Vector3.up * speed);
           // buttonRB2.AddForce(-Vector3.up * speed);
            playerRB.AddForce(-Vector3.up * speed);
            dogRB.AddForce(-Vector3.up * speed);
            for (int i = 0; i < liftObjects.Length; i++)
            {
                liftObjects[i].AddForce(-Vector3.up * speed);
            }
        }
        else
        {
            //platformRB.velocity = Vector3.zero;
            //buttonRB.velocity = Vector3.zero;
            //buttonRB2.velocity = Vector3.zero;
            playerRB.velocity = Vector3.zero;
            dogRB.velocity = Vector3.zero;
            for (int i = 0; i < liftObjects.Length; i++)
            {
                liftObjects[i].velocity = Vector3.zero;
            }

        }
    }

    
}
