using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour {
    float speed = 2;
    public Transform target;
    public CreateGrid gridScript;
    // Use this for initialization
    void Start() {
        gridScript = GameObject.FindGameObjectWithTag("GridGenerator").GetComponent<CreateGrid>();
    }

    // Update is called once per frame
    void Update() {
        StartCoroutine(MoveAlongPath());
    }


    IEnumerator MoveAlongPath()
    {
        gridScript.CreateTheGrid();
        Vector3 currentWaypoint = gridScript.path[0].nodePos;
        int counter = 0;
        while (true) {
            if (transform.position == currentWaypoint)
            {
                counter++;
                if (counter >= gridScript.path.Count)
                {
                    yield break;
                }
                currentWaypoint = gridScript.path[counter].nodePos;
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed);
            yield return null;
        }


    }

}
