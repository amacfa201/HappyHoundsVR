using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterDoorScript : MonoBehaviour
{
    public GameObject doors;
    public GameObject doors2;
    Animator doorAnim;
    public CreateGrid Grid;
    public bool domeDoor;
    public AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        doorAnim = GameObject.FindGameObjectWithTag("OuterDoorScript").GetComponent<Animator>();
        Grid = GameObject.FindGameObjectWithTag("GridGenerator").GetComponent<CreateGrid>();
        //Grid.CreateTheGrid();
    }

    void Update()
    {

    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "GameController")
    //    {
    //        if (domeDoor)
    //        {
    //            doorAnim.SetBool("OuterDoorOpen", false);
    //            audioManager.PlayOnce("DoorOpening");
    //            print("DC door to false");
    //            domeDoor = false;
    //            StartCoroutine(OneTimeUpdate());
    //        }
    //        else
    //        {
    //            doorAnim.SetBool("OuterDoorOpen", true);
    //            audioManager.PlayOnce("DoorOpening");
    //            print("DC door to false");
    //            domeDoor = true;
    //            StartCoroutine(OneTimeUpdate());
    //        }
    //    }
    //}

    IEnumerator OneTimeUpdate() // Called a few seconds after game start to regenerate grid with the dome doors shut
    {
        yield return new WaitForSeconds(1.4f);
        Grid.CreateTheGrid();
    }


    public void AffectDoor()
    {
        if (domeDoor)
        {
            doorAnim.SetBool("OuterDoorOpen", false);
            audioManager.PlayOnce("DoorOpening");
            domeDoor = false;
            StartCoroutine(OneTimeUpdate());
        }
        else
        {
            doorAnim.SetBool("OuterDoorOpen", true);
            audioManager.PlayOnce("DoorOpening");
            domeDoor = true;
            StartCoroutine(OneTimeUpdate());
        }
    }
}