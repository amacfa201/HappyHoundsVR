//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class doorScript : MonoBehaviour
//{
//    public bool doorState = false;
//    public bool opening;
//    public bool closing;
//    float currentTime;
//    float lerpTime;
///conall
///
//    public GameObject doorTargetOpen;
//    public GameObject doorTargetClose;
//    // Use this for initialization
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//        if (opening)
//        {
//            //StartCoroutine(Lerp(doorTargetClose));
//            doorState = true;
//            print(opening);
//        }

//        if (closing)
//        {
//            //StartCoroutine(Lerp(doorTargetOpen));
//            doorState = false;
//            print(closing);
//        }


//    }

    //IEnumerator Lerp(GameObject target)
    //{
    //    currentTime = 0.0f;
    //    while (currentTime < lerpTime)
    //    {
    //        currentTime += Time.deltaTime;
    //        target.transform.position = Vector3.Lerp(transform.position, target.transform.position, currentTime / lerpTime);
    //        print("lerping");
    //        yield return 0;
    //    }

    //    //if (currentTime == lerpTime)
    //    //{
    //    //    print("CT=LT");
    //    //    opening = false;
    //    //    closing = false;
    //    //}
    //}


    //public void OpenDoor()
    //{
    //    opening = true;
    //}

    //public void CloseDoor()
    //{
    //    closing = true;
    //}


//}
