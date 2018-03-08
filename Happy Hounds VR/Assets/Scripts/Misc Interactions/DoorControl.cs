using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public GameObject doors;
    public GameObject doors2;
    public CreateGrid Grid;


    public bool RoomDoor; // False = Shut

    Animator doorAnim;

    public AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        doorAnim = GameObject.FindGameObjectWithTag("InnerDoorScript").GetComponent<Animator>();
        Grid = GameObject.FindGameObjectWithTag("GridGenerator").GetComponent<CreateGrid>();
        //Grid.CreateTheGrid();
        StartCoroutine(FixDoorOnStart());

    }

    void Update()
    {
        print("DC  = " + doorAnim.GetBool("InnerDoorOpen"));
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "GameController")
        {
            if (RoomDoor)
            {
                doorAnim.SetBool("InnerDoorOpen", false);
                audioManager.PlayOnce("DoorOpening");
                print("DC door to false");
                RoomDoor = false;
                StartCoroutine(OneTimeUpdate());
            }
            else
            {
                doorAnim.SetBool("InnerDoorOpen", true);
                audioManager.PlayOnce("DoorOpening");
                print("DC door to false");
                RoomDoor = true;
                StartCoroutine(OneTimeUpdate());
            }



        }

    }

    IEnumerator OneTimeUpdate() // Called a few seconds after game start to regenerate grid with the dome doors shut
    {
        yield return new WaitForSeconds(1.4f);
        Grid.CreateTheGrid();
    }

    IEnumerator FixDoorOnStart() // Called a few seconds after game start to regenerate grid with the dome doors shut
    {
        yield return new WaitForSeconds(1f);
        Grid.CreateTheGrid();
    }
}