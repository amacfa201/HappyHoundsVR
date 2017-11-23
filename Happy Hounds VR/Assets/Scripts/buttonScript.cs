using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonScript : MonoBehaviour {
    public GameObject backgroundSource;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "button") {
            if (backgroundSource.activeInHierarchy)
            {
                backgroundSource.SetActive(false);
            }
            else
            {
                backgroundSource.SetActive(true);
            }
        }
    }
}
