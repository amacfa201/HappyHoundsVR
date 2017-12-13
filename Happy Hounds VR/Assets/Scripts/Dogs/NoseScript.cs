using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseScript : MonoBehaviour
{
    public CorgiScript testScript;
    // Use this for initialization
    void Start()
    {
        testScript = GameObject.FindGameObjectWithTag("Corgi").GetComponent<CorgiScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "foodPellet")
        {
            Destroy(other.gameObject, Random.Range(4.5f, 12.0f));
            if (testScript.animState == CorgiScript.dogState.Eating)
            {
                testScript.currentlyEating = true;
            }
            if (testScript.inBowl > 0)
            {
                testScript.inBowl--;
            }
        }
    }
}
