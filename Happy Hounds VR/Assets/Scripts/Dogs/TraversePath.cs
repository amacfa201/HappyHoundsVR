﻿using UnityEngine;
using System.Collections;
using System;

public class TraversePath : MonoBehaviour
{
             //values that will be set in the Inspector
     public Transform Target;
    public float RotationSpeed = 2f;

    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;

    public Transform target;
    float speed = 1f;
    float slerpTime = 5f;
    Vector3[] path;
    int targetIndex;
 

<<<<<<< HEAD
     void Update()
    {
        print(path.Length);
    }
=======
    
>>>>>>> parent of 8da3bf0... Fixed wander

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    public void MoveDog(Vector3 DogPos, Vector3 Target)
    {
        PathRequestManager.RequestPath(DogPos, Target, OnPathFound);
    }


    IEnumerator FollowPath()
    {
        
        Vector3 currentWaypoint = path[0];
        while (true)
        {
            print(targetIndex);
            if (transform.position == currentWaypoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            

            LookAt(currentWaypoint);

            yield return null;

        }
    }


    public void LookAt(Vector3 Target)
    {

        //find the vector pointing from our position to the target
        _direction = (Target - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
<<<<<<< HEAD
    }

=======
}
>>>>>>> parent of 8da3bf0... Fixed wander
    public void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = targetIndex; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);

                if (i == targetIndex)
                {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else
                {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}