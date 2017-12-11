using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorControl : MonoBehaviour
{

    //public doorScript doorScript;
    //public doorScript doorScript2;
    public GameObject doors;
    Animator doorAnim;
    public GameObject doors2;
    public AudioManager audioManager;

   
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        doorAnim = GameObject.FindGameObjectWithTag("InnerDoorScript").GetComponent<Animator>();
    }

    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "GameController")
        {
            if (doorAnim.GetBool("InnerDoorOpen") == true)
            {
                doorAnim.SetBool("InnerDoorOpen", false);
                audioManager.PlayOnce("DoorOpening");
            }
            else
            {
                doorAnim.SetBool("InnerDoorOpen", true);

                audioManager.PlayOnce("DoorOpening");
            }
        }

    }
}



//    void OpenDoor()
//    {



//    }

//    void CloseDoor()
//    {

//    }



//}
