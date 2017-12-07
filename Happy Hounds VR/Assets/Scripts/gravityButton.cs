using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityButton : MonoBehaviour
{

    public bool grav = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "GameController")
        {

            if (grav)
            {

                grav = false;

            }
            else
            {

                grav = true;

            }
        }

    }

}
