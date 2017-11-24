using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pettingScript : MonoBehaviour {
    public float timeSinceChange;
    public string currentAnim = null;
    // Use this for initialization
    void Start () {
        RandomizePettingAnims();
	}
	
	// Update is called once per frame
	void Update () {
        currentAnim = "corgipettingstand1";

    }

    void RandomizePettingAnims()
    {

        timeSinceChange = 0;
        int animNum = Random.Range(1, 3);
  
        if (animNum == 1)
        {
            currentAnim = "corgibackscratch";
        }
        if (animNum == 2)
        {
            currentAnim = "corgipettingstand1";
        }


        if (animNum == 3)
        {
            currentAnim = "Take 001";
        }

    }


    void RandAnimTime() {
        timeSinceChange += Time.deltaTime;
        if (timeSinceChange > Random.Range(4.5f, 10))
        {
            RandomizePettingAnims();
        }
    }

}
