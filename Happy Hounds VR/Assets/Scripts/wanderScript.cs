using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wanderScript : MonoBehaviour {
    public float circleRadius = 5.0f;
    public float circleDistance = 5.0f;
    Vector3 desiredVelocity = Vector3.zero;
    public float maxSpeed = 0.75f;
    bool waiting;
    public Vector3 testVec3;
    float overlapRadius = 0.5f;
    public LayerMask untraversableMask;
    // Use this for initialization
    void Start () {    
    }
	
	// Update is called once per frame
	void Update () {
        testVec3 = desiredVelocity;
        if (!waiting)
        {
            FindNewPoint();
            StartCoroutine(temp());
        }
        else
        {
            transform.position += desiredVelocity * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
        }
        if (desiredVelocity.sqrMagnitude > 0.0f)
        {
            transform.forward = Vector3.Normalize(new Vector3(desiredVelocity.x, 0.0f, desiredVelocity.z));
        }

        if (transform.position == testVec3)
        {
            waiting = false;
        }

    }


    public Vector3 FindNewPoint()
    {
        Vector3 randPoint = Random.insideUnitCircle * circleRadius;

        randPoint += transform.position + new Vector3((transform.forward.x * circleDistance), 0.0f, (transform.forward.z * circleDistance));
        //print("point = " + Physics.OverlapSphere(randPoint, overlapRadius, untraversableMask));

        //for(int i= 0; i < (Physics.OverlapSphere(randPoint, overlapRadius, untraversableMask)).Length; i++)

        //{

        //    if (Physics.OverlapSphere(randPoint, overlapRadius, untraversableMask)[i].)
        //    {

        //    }

        //}

        //if (Physics2D.OverlapCircle(randPoint, overlapRadius, untraversableMask) == false)
        //{
        //    desiredVelocity = Vector3.Normalize(randPoint - transform.position) * 1.25f;
        //}

        while (Physics2D.OverlapCircle(randPoint, overlapRadius, untraversableMask) == true)
        {
            print("1");
            randPoint += transform.position + new Vector3((transform.forward.x * circleDistance), 0.0f, (transform.forward.z * circleDistance));
        }
        
        desiredVelocity = Vector3.Normalize(randPoint - transform.position) * 1.25f;
        return desiredVelocity;
    }

    IEnumerator temp()
    {
        waiting = true;
        yield return new WaitForSeconds(2f);
        waiting = false;
    }
}
