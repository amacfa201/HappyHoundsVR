using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node  {

    public bool traversable;
    public Vector3 nodePos;

    public Node(bool _traversable, Vector3 _nodePos)
    {
        traversable = _traversable;
        nodePos = _nodePos;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
