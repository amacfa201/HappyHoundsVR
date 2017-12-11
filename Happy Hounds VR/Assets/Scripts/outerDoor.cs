using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outerDoor : MonoBehaviour
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
        doorAnim = GameObject.FindGameObjectWithTag("OuterDoorScript").GetComponent<Animator>();
    }

    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "GameController")
        {
            if (doorAnim.GetBool("OuterDoorOpen") == true)
            {
                doorAnim.SetBool("OuterDoorOpen", false);
                audioManager.PlayOnce("DoorOpening");
            }
            else
            {
                doorAnim.SetBool("OuterDoorOpen", true);

                audioManager.PlayOnce("DoorOpening");
            }
        }

    }
}