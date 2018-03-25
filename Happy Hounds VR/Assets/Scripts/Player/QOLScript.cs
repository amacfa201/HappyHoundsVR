using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QOLScript : MonoBehaviour {
    public GameObject player;
    public GameObject dog;

    public Vector3  gardenP;
    public Vector3 gardenD;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetKey(KeyCode.G)))
        {
            player.transform.position = gardenP;
            dog.transform.position = gardenD;
        }
	}
}
