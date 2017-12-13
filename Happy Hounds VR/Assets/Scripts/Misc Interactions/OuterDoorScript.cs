using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterDoorScript : MonoBehaviour
{
    public GameObject doors;
    public GameObject doors2;
    Animator doorAnim;

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