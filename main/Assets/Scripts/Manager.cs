using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Manager : MonoBehaviour
{
    public GameObject selectCell;
    public GameObject location;

    private void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                GameObject cell = Instantiate(selectCell, new Vector3(i + 0.5f, 0.01f, j + 0.5f), Quaternion.identity);
                cell.transform.parent = location.transform.Find("SelectedCells");
                cell.SetActive(false);
                cell.name = "SCell" + i.ToString() + j.ToString();
            }
        }
    }
}
