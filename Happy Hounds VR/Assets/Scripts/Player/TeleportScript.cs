//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TeleportScript : MonoBehaviour {

//    private SteamVR_TrackedObject steamVRTrackedObj;
//    private SteamVR_Controller.Device viveCont
//    {
//        get { return SteamVR_Controller.Input((int)steamVRTrackedObj.index); }
//    }

//    public Transform headsetTransPos;
//    public Transform cameraRigPos;
//    private Transform teleportReticlePos;
//    private Transform laserPos;

//    private GameObject laser;
//    private GameObject teleportPad;
//    public GameObject laserPrefab;
//    public GameObject teleportPadPrefab;

//    private Vector3 hitPoint;
//    public Vector3 teleportReticleOffset;
    

//    public LayerMask teleportingMask;
  
//    private bool teleport;

   
//    void Awake()
//    {
//        steamVRTrackedObj = GetComponent<SteamVR_TrackedObject>();
//    }

//    private void SpawnLaser(RaycastHit hitPosition)
//    {        
//        laser.SetActive(true);
//        laserPos.position = Vector3.Lerp(steamVRTrackedObj.transform.position, hitPoint, .5f);
//        laserPos.LookAt(hitPoint);
//    }

//    // Use this for initialization
//    void Start ()
//    {
//        laser = Instantiate(laserPrefab);
//        laserPos = laser.transform;
//        teleportPad = Instantiate(teleportPadPrefab);
//        teleportReticlePos = teleportPad.transform;
//    }

//    // Update is called once per frame
//    void Update ()
//    {
//        if (viveCont.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
//        {
//            RaycastHit hitPosition;
//            if (Physics.Raycast(steamVRTrackedObj.transform.position, transform.forward, out hitPosition, 100, teleportingMask))
//            {
//                hitPoint = hitPosition.point;
//                SpawnLaser(hitPosition);
//                teleportPad.SetActive(true);
//                teleportReticlePos.position = hitPoint + teleportReticleOffset;
//                teleport = true;
//            }
//        }
//        else 
//        {
//            laser.SetActive(false);
//        }
//        if (viveCont.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && teleport)
//        {
//            TeleportToPoint();
//        }
//    }

//    private void TeleportToPoint()
//    {
//        teleport = false;
//        teleportPad.SetActive(false);
//        Vector3 difference = cameraRigPos.position - headsetTransPos.position;
//        difference.y = 0;
//        cameraRigPos.position = hitPoint + difference;
//    }
//}
