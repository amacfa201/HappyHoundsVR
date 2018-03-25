//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class platformBarrier : MonoBehaviour
//{
//    public GameObject player;
//    public Rigidbody playerRB;
//    platformScript platformScript;
//    bool doOnce;
//    void Start()
//    {
//        platformScript = GameObject.FindGameObjectWithTag("Platform").GetComponent<platformScript>();
//    }

//    void OnTriggerEnter(Collider other)
//    {
//        if (other.tag == "GameController")
//        {
//            //if (!doOnce)
//            //{
//            print("Headset Entered");
//            platformScript.playerCollider.enabled = true;

//            playerRB.isKinematic = false;
//            playerRB.useGravity = false;
//            // doOnce = true;
//            //}

//        }
//    }

//    void OnTriggerExit(Collider other)
//    {
//        if (other.tag == "GameController")
//        {
//            print("Headset Exited");
//            platformScript.playerCollider.enabled = false;
//            playerRB.isKinematic = true;
//            // Destroy(playerRB);
//            //doOnce = false;
//        }
//    }
//}
