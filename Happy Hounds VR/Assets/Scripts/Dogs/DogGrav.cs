using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogGrav : MonoBehaviour
{
    Rigidbody rigid;
    GravityButton gravScript;
    bool localGrav;
    bool doOnce = true;
    public bool collected;
    public Transform target;
    public Transform player;
    public Transform dogNose;
    public Transform helmet;
    public Transform helmetPosition;
    public float RotationSpeed = 2f;
    private Quaternion _lookRotation;
    private Vector3 _direction;
    public Rigidbody boneRB;
    Rigidbody helmetRB;
    public MeshCollider boneCol;
    public bool clear;

    //public bool onDog = false;

    // Use this for initialization
    void Start()
    {
        gravScript = GameObject.FindGameObjectWithTag("GravityButton").GetComponent<GravityButton>();
        boneRB = GameObject.FindGameObjectWithTag("Bone").GetComponent<Rigidbody>();
        boneCol = GameObject.FindGameObjectWithTag("Bone").GetComponent<MeshCollider>();
        helmetRB = GameObject.FindGameObjectWithTag("CorgiHelmet").GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gravScript.grav)// Gravity is turned off
        { 
            LoseGrav();
            ClearBlockage();
        }
        else
        {
            ResetRigid();
            ResetDog();
        } 
    }

    public void ResetRigid()
    {
        if (localGrav)
        {
            rigid.useGravity = true;
            Destroy(rigid, 3f);
            localGrav = false;
        }
    }

    public void LoseGrav()
    {
        if (!localGrav)
        {
            doOnce = false;
            this.gameObject.AddComponent<Rigidbody>();
            rigid = GetComponent<Rigidbody>();
            rigid.useGravity = false;
            rigid.AddForce(Vector3.up * 15f);
            localGrav = true;

        }
    }

    public void ClearBlockage()
    {
        if (Vector3.Distance(dogNose.position, target.position) > 1 && !collected)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, 1.5f * Time.deltaTime);
           // helmet.position = Vector3.MoveTowards(transform.position, target.position, 1.5f * Time.deltaTime);
            helmet.transform.parent = helmetPosition.transform;
            helmet.localPosition = Vector3.zero;
           // helmetRB.constraints = RigidbodyConstraints.FreezeRotation;
            Lookat(target.position);
        }
        else if (Vector3.Distance(dogNose.position, player.position) > 1)
        {

            collected = true;
            target.position = dogNose.position;
            transform.position = Vector3.MoveTowards(transform.position, player.position, 1.5f * Time.deltaTime);
           // helmet.position = Vector3.MoveTowards(transform.position, player.position, 1.5f * Time.deltaTime);
            helmet.transform.parent = helmetPosition.transform;
            helmet.localPosition = Vector3.zero;
            //helmetRB.constraints = RigidbodyConstraints.FreezeRotation;
            Lookat(player.position);
            //clear = true;
        }
        else
        {
            ResetDogRot();
            helmetRB.constraints = RigidbodyConstraints.None;
        }
    }

    public void ResetDogRot()
    {
        
    }

    public void ResetDog()
    {
        if (!doOnce) {
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
            Quaternion target = Quaternion.Euler(0, transform.rotation.y, 0);
            transform.rotation = target;
            boneCol.enabled = true;
            boneRB.useGravity = true;
            doOnce = true;
        }
       
        //transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 2);
    }

    private void Lookat(Vector3 Target)
    {
        //find the vector pointing from our position to the target
        _direction = (Target - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
    }

}