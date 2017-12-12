using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pitchingScript : MonoBehaviour {
    public GameObject spawn;
    public GameObject prefab;
    pitchingButton _pitchingButton;
    public float timer;
    public float timeLimited = 2f;
	// Use this for initialization
	void Start () {
        _pitchingButton = GameObject.FindGameObjectWithTag("pitchingButton").GetComponent<pitchingButton>();
    }
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
		if(_pitchingButton.on && timer > timeLimited)
        {
            timer = 0;
            Instantiate(prefab, spawn.transform.position, transform.rotation);
        }


	}
}
