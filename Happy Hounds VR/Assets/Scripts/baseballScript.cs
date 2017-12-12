using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseballScript : MonoBehaviour {
    Rigidbody rigid;
	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        rigid.AddForce(Vector3.left * 1200f);
        Destroy(this.gameObject, 15f);
    }
	
	// Update is called once per frame
	void Update () {
       
	}
}
