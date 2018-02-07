using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchingScript : MonoBehaviour {
    public GameObject spawn;
    public GameObject prefab;

    PitchingButtonScript _pitchingButton;

    public float timer;
    public float timeLimited = 0.002f;
	// Use this for initialization
	void Start ()
    {
        _pitchingButton = GameObject.FindGameObjectWithTag("pitchingButton").GetComponent<PitchingButtonScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
		if(_pitchingButton.on && timer > timeLimited)
        {
            timer = 0;
            Instantiate(prefab, spawn.transform.position, transform.rotation);
        }
	}
}
