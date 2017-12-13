using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public GameObject doors;
    public GameObject doors2;

    Animator doorAnim;

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