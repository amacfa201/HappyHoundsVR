using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pettingScript : MonoBehaviour {
    public float timeSinceChange;
    public string currentAnim = null;
    // Use this for initialization
    void Start () {
		//currentAnim = "corgipettingstand1";
		RandomizePettingAnims();
	}
	
	// Update is called once per frame
	void Update () {
        
		//RandAnimTime ();
    }

    void RandomizePettingAnims()
    {

        timeSinceChange = 0;
        int animNum = Random.Range(1, 4);
		print ("RandomNum: " + animNum);
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


   public void RandAnimTime() {
        timeSinceChange += Time.deltaTime;
		float randTime = Random.Range (4.5f, 10);
        if (timeSinceChange > randTime)
        {
            RandomizePettingAnims();
        }
    }

}
