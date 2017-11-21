using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoseScript : MonoBehaviour {

    public GameObject waterPrefab;
    public GameObject spawnPoint;

    public bool holdingHose;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (holdingHose)
        {
            SpawnWaterDrops();
        }
	}

    void SpawnWaterDrops()
    {
        GameObject water = (GameObject)Instantiate(waterPrefab, spawnPoint.transform.position, Quaternion.identity);
    }


}
