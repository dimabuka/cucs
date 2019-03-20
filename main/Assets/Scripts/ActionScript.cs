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
    public GameObject[] objects;
    public SteamVR_Action_Boolean nextItem;
    public int count;
    public GameObject Holo;
    public Manager manager;

    private bool firstTouchNextItem = true;
    private bool firstTouchGrab = true;
    private GameObject ActiveCell = null;
    private int pointer = 0;

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
            if (hit.transform.tag == "Terrain")
            { 
                int x = (int)hit.point.x;
                int y = (int)hit.point.z;
                if(0 <= x * 10 + y && x * 10 + y < 100)
                { 
                    ActiveCell = location.transform.Find("SelectedCells").GetChild(x * 10 + y).gameObject;
                    ActiveCell.SetActive(true);
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
                        if (pointer >= 1 && pointer <= 4) // Конвейеры
                        {
                            Instantiate(objects[pointer], new Vector3(x + 0.5f, 0, y + 0.5f), objects[pointer].transform.rotation);
                            manager.addCell(x, y, pointer - 1);
                        }
                        else if(pointer == 0) // Ресурсы
                        {
                            Instantiate(objects[pointer], new Vector3(x + 0.5f, 0.5f, y + 0.5f), Quaternion.identity);
                            manager.addFactory(x, y, 0);
                            manager.addCell(x, y, 5);
                        }
                        else if(pointer == 5) // Склад
                        {
                            Instantiate(objects[pointer], new Vector3(x + 0.5f, 0.5f, y + 0.5f), Quaternion.identity);
                            manager.addCell(x, y, 6);
                        }
                    }
                }
            }
        }
        else
        {
            firstTouchGrab = true;
        }
        if (nextItem.GetState(handType))
        {
            if (firstTouchNextItem)
            {
                int next = (pointer + 1) % count;
                Holo.transform.GetChild(pointer).gameObject.SetActive(false);
                Holo.transform.GetChild(next).gameObject.SetActive(true);
                pointer = next;
                firstTouchNextItem = false;
            }
        }
        else
        {
            firstTouchNextItem = true;
        }
    }
}
