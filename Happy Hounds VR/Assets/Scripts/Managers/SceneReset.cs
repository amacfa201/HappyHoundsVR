//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class SceneReset : MonoBehaviour {
//    public GameObject dog;
//    public GameObject bunker;
//    public GameObject reset;
//	// Use this for initialization
//	void Start ()
//    {
		
//	}
	
//	// Update is called once per frame
//	void Update () {
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            SceneManager.LoadScene("TestScene2");
//        }
//        if (dog.transform.position.x > 40 || dog.transform.position.x < -400)
//        {
//            dog.transform.position = reset.transform.position;
//        }
//        if (dog.transform.position.y > 20 || dog.transform.position.y < -10)
//        {
//            dog.transform.position = reset.transform.position;
//        }
//        if (dog.transform.position.z > 40 || dog.transform.position.z < -40)
//        {
//            dog.transform.position = reset.transform.position;
//        }

//        if (Input.GetKeyDown(KeyCode.DownArrow))
//        {
//            bunker.SetActive(false);
//        }

//    }
//}
