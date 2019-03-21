using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ActionScript : MonoBehaviour
{

    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public GameObject cube;
    public SteamVR_Action_Boolean grabAction;
    public GameObject location;
    public GameObject laserPrefab;
    public GameObject[] objects;
    public int count;
    public GameObject Holo;
    public Manager manager;
    public SteamVR_Action_Vector2 nextItem;
    public SteamVR_Action_Boolean delItem;

    private bool firstTouchNextItem = true;
    private bool firstTouchDelItem = true;
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
                if (0 <= x && x < 10 && 0 <= y && y < 10 && manager.map[x, y] == -1)
                {
                    ActiveCell = location.transform.Find("SelectedCells").GetChild(x * 10 + y).gameObject;
                    ActiveCell.SetActive(true);
                }
            }
        }
        if (grabAction.GetState(handType))
        {
            if (firstTouchGrab)
            {
                firstTouchGrab = false;
                if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100))
                {
                    if (hit.transform.tag == "Terrain")
                    {
                        int x = (int)hit.point.x;
                        int y = (int)hit.point.z;
                        if (0 <= x && x < 10 && 0 <= y && y < 10 && manager.map[x, y] == -1)
                        {
                            if (pointer >= 4 && pointer <= 7) // Конвейеры
                            {
                                Instantiate(objects[pointer], new Vector3(x + 0.5f, 0, y + 0.5f), objects[pointer].transform.rotation);
                                manager.addCell(x, y, pointer - 4);
                            }
                            else if (pointer <= 3) // Ресурсы
                            {
                                manager.addFactory(x, y, pointer, Instantiate(objects[pointer], new Vector3(x + 0.5f, 0, y + 0.5f), Quaternion.identity));
                                manager.addCell(x, y, pointer + 5);
                            }
                            else if (pointer == 8) // Склад
                            {
                                Instantiate(objects[pointer], new Vector3(x + 0.5f, 0.5f, y + 0.5f), Quaternion.identity);
                                manager.addCell(x, y, 10);
                            }
                            else if (pointer >= 9) // Фабрика
                            {
                                manager.addPot(x, y, Instantiate(objects[pointer], new Vector3(x + 0.5f, 0.5f, y + 0.5f), Quaternion.identity), pointer - 9);
                                manager.addCell(x, y, 100);
                            }
                        }
                    }
                }
            }
        }
        else
        {
            firstTouchGrab = true;
        }
        if (nextItem.GetAxis(handType).x < 0)
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
        else if (nextItem.GetAxis(handType).x > 0)
        {
            if (firstTouchNextItem)
            {
                int next = (pointer - 1 + count) % count;
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

        if (delItem.GetState(handType))
        {
            if (firstTouchDelItem)
            {
                firstTouchDelItem = false;
                if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100))
                {
                    int x = (int)hit.point.x;
                    int y = (int)hit.point.z;
                    if (0 <= x && x < 10 && 0 <= y && y < 10 && manager.map[x, y] != -1)
                    {
                        Debug.Log(hit.transform.name);
                        if (hit.transform.tag == "Item")
                        {
                            manager.map[x, y] = -1;
                            Destroy(hit.transform.gameObject);
                        }
                    }
                }
            }
        }
        else firstTouchDelItem = true;
    }
}