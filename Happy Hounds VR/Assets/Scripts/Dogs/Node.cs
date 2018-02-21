using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node  {

    public bool traversable;
    public Vector3 nodePos;

    public int gCost;
    public int hCost;
    public int gridX;
    public int gridY;
    public Node parent;

    public Node(bool _traversable, Vector3 _nodePos, int _gridX, int _gridY)
    {
        traversable = _traversable;
        nodePos = _nodePos;
        gridX = _gridX;
        gridY = _gridY;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int fCost
    { 
      get{
            return gCost + hCost;

      }
    }
}
