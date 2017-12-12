using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravityButton : MonoBehaviour
    
{
    private Animator anim;
    public corgiScript _testScript;
    public AudioManager audioManager;
    public bool grav = true;

    // Use this for initialization
    void Start()
    {
        _testScript = GameObject.FindGameObjectWithTag("Corgi").GetComponent<corgiScript>();
        audioManager = FindObjectOfType<AudioManager>();
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
                audioManager.PlayOnce("ButtonSound");
                grav = false;
                _testScript.animState = corgiScript.dogState.Floating;
                

            }
            else
            {
                audioManager.PlayOnce("ButtonSound");
                grav = true;
                _testScript.animState = corgiScript.dogState.Idle;
            }
        }

    }

}
