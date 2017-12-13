using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityButton : MonoBehaviour
    
{
    private Animator anim;

    public CorgiScript _testScript;
    public AudioManager audioManager;

    public bool grav = true;

    // Use this for initialization
    void Start()
    {
        _testScript = GameObject.FindGameObjectWithTag("Corgi").GetComponent<CorgiScript>();
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
                _testScript.animState = CorgiScript.dogState.Floating;
            }
            else
            {
                audioManager.PlayOnce("ButtonSound");
                grav = true;
                _testScript.animState = CorgiScript.dogState.Idle;
            }
        }
    }
}
