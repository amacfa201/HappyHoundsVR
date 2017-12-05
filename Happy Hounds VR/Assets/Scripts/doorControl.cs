using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorControl : MonoBehaviour
{

    //public doorScript doorScript;
    //public doorScript doorScript2;
    public GameObject doors;

    public GameObject doors2;
    void Start()
    {

    }

    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "GameController")
        {
            if (doors.activeInHierarchy)
            {


                doors.SetActive(false);
            }
            else
            {
                doors.SetActive(true);

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
