using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoseScript : MonoBehaviour {

    public GameObject waterPrefab;
    public GameObject spawnPoint;

    public bool holdingHose;
    public bool triggerDown;
    public float timeSinceSpawn;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timeSinceSpawn += Time.deltaTime;
        if (holdingHose && triggerDown)
        {
            SpawnWaterDrops();
        }
	}

    void SpawnWaterDrops()
    {
        if (timeSinceSpawn > 0.05f)
        {
            timeSinceSpawn = 0;
            GameObject water = (GameObject)Instantiate(waterPrefab, spawnPoint.transform.position, Quaternion.identity);
        }
    }


}
