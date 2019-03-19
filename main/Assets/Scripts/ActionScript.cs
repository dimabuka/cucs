using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ActionScript : MonoBehaviour {

    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public GameObject cube;
    public SteamVR_Action_Boolean grabAction;
    public GameObject location;
    public GameObject laserPrefab;

    private bool firstTouchGrab = true;
    private GameObject ActiveCell = null;
    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;

    private void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
    }

    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(controllerPose.transform.position, hitPoint, .5f);
        laserTransform.LookAt(hitPoint);
    }

    void Update()
    {
        if (ActiveCell != null)
        {
            ActiveCell.SetActive(false);
            ActiveCell = null;
        }
        RaycastHit hit;
        if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100))
        {
            hitPoint = hit.point;
            laser.transform.position = Vector3.Lerp(controllerPose.transform.position, hit.point, .5f);
            laser.transform.LookAt(hit.point);
            if (hit.transform.tag == "Terrain")
            { 
                int x = (int)hit.point.x;
                int y = (int)hit.point.z;
                if(0 <= x * 10 + y && x * 10 + y < 100)
                { 
                    ActiveCell = location.transform.Find("SelectedCells").GetChild(x * 10 + y).gameObject;
                    ActiveCell.SetActive(true);
                    ShowLaser(hit);
                }
            }
        }
        if (grabAction.GetState(handType))
        {
            if(firstTouchGrab)
            {
                firstTouchGrab = false;
                if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100))
                {
                    if (hit.transform.tag == "Terrain")
                    {
                        int x = (int)hit.point.x;
                        int y = (int)hit.point.z;
                        Instantiate(cube, new Vector3(x + 0.5f, 0.5f, y + 0.5f), Quaternion.identity);

                    }
                }
            }
        }
        else
        {
            firstTouchGrab = true;
        }
    }
}
