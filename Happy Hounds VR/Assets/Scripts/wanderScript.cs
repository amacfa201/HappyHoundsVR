using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wanderScript : MonoBehaviour
{
    public float circleRadius = 5.0f;
    public float circleDistance = 5.0f;
    Vector3 desiredVelocity = Vector3.zero;
    public float maxSpeed = 0.75f;
    public float pointOffset = 2.2f;
    bool waiting;
    public Vector3 testVec3;
    float overlapRadius = 0.5f;
    public LayerMask untraversableMask;
    Vector3[] points = null;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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

        //Vector3[] points = null;
        //RaycastHit hit;
        //points[0] = randPoint;
        //points[1] = new Vector3(randPoint.x += pointOffset, randPoint.y, randPoint.z);
        //points[2] = new Vector3(randPoint.x -= pointOffset, randPoint.y, randPoint.z);
        //points[3] = new Vector3(randPoint.x, randPoint.y, randPoint.z += pointOffset);
        //points[4] = new Vector3(randPoint.x, randPoint.y, randPoint.z += pointOffset);
        //int clearPoints = 0;

        //if (clearPoints != points.Length)
        //{
        //    for (int i = 0; i < points.Length; i++)
        //    {

        //        Physics.Raycast(points[i], Vector3.down, out hit, 15);
        //        if (hit.collider.gameObject.tag == "floor")
        //        {
        //            clearPoints++;
        //        }
        //    }

        //}








        while (!CheckPoints(randPoint))
        {
            randPoint = Random.insideUnitCircle * circleRadius;
            randPoint += transform.position + new Vector3((transform.forward.x * circleDistance), 0.0f, (transform.forward.z * circleDistance));
            //CheckPoints(randPoint);
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

    bool CheckPoints(Vector3 randPoint)
    {
       
        RaycastHit hit = new RaycastHit();
        points[0] = new Vector3(randPoint.x, randPoint.y, randPoint.z);
        points[1] = new Vector3(randPoint.x += pointOffset, randPoint.y, randPoint.z);
        points[2] = new Vector3(randPoint.x -= pointOffset, randPoint.y, randPoint.z);
        points[3] = new Vector3(randPoint.x, randPoint.y, randPoint.z += pointOffset);
        points[4] = new Vector3(randPoint.x, randPoint.y, randPoint.z -= pointOffset);
        int clearPoints = 0;


        for (int i = 0; i < points.Length; i++)
        {
            Physics.Raycast(new Vector3(points[i].x, points[i].y += 10, points[i].z), Vector3.down, out hit, 15);
            if (hit.collider.gameObject.tag == "plane")
            {
                clearPoints++;
            }
        }

        if (clearPoints == points.Length)
        {
            return true;
        }
        else
        {
            return false;
        }




    }

}