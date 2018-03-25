using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WanderScript : MonoBehaviour
{
    public GameObject resetPoint;
    RaycastHit hit = new RaycastHit();
    public LayerMask untraversableMask;

    Vector3[] points = new Vector3[5];
    Vector3 desiredVelocity = Vector3.zero;
    public Vector3 testVec3;

    public float circleRadius = 5.0f;
    public float circleDistance = 5.0f;
    public float maxSpeed = 0.75f;
    public float pointOffset = 0.5f;
    float overlapRadius = 0.5f;

    bool waiting;
    bool checking;
    bool useWhile; //testing only
    bool doOnce;
    public bool foundPath;

    public CreateGrid grid;
    public CorgiScript corgi;
    public TraversePath path;

    Node StartNode;
    Node Target;

    void Awake()
    {
        grid = GameObject.FindGameObjectWithTag("GridGenerator").GetComponent<CreateGrid>();
        corgi = GameObject.FindGameObjectWithTag("Corgi").GetComponent<CorgiScript>();
        path = GetComponent<TraversePath>();
        
    }

    void Update()
    {
        
        Wander();
        //print("TI = " + path.targetIndex);
    }

    public void Wander()
    {
        if (!doOnce)
        {
            Target = grid.PickRandNode();
            path.MoveTo(transform.position, Target.nodePos);
            doOnce = true;
        }

        if (Vector3.Distance(transform.position, Target.nodePos) < 1 || path.path.Length == 0)
        {
            Array.Resize(ref path.path, 0);
            Target = grid.PickRandNode();
            path.MoveTo(transform.position, Target.nodePos);
        }
    }

    IEnumerator UpdateWander(Node StartNode, Node Target)
    {
        yield return new WaitForSeconds(1f);

        if (Vector3.Distance(transform.position, Target.nodePos) < 1 || path.path == null)
        {
            StartNode = grid.NodeFromWorldPoint(transform.position);
            Target = grid.PickRandNode();
            path.MoveTo(transform.position, Target.nodePos);
        }

    }


     void OnOnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawCube(Target.nodePos, Vector3.one * (0.4f - 0.1f));
    }

}