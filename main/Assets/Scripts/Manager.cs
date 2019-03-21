using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Manager : MonoBehaviour
{
    public GameObject selectCell;
    public GameObject location;
    public GameObject[] resourses;
    public bool pause = false;

    private int[,] dd = {
        {-1, 0},
        {1, 0},
        {0, 1},
        {0, -1}
    };

    public int[,] map = new int[10,10];
    List<Vector3> factories = new List<Vector3>();
    public GameObject[,] pots = new GameObject[10, 10];
    Dictionary<Color, int> stock;

    private void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                map[i, j] = -1;
                GameObject cell = Instantiate(selectCell, new Vector3(i + 0.5f, 0.01f, j + 0.5f), Quaternion.identity);
                cell.transform.parent = location.transform.Find("SelectedCells");
                cell.SetActive(false);
                cell.name = "SCell" + i.ToString() + j.ToString();
            }
        }
    }

    public void addPot(int i, int j, GameObject a)
    {
        pots[i, j] = a;
    }

    public void addFactory(int i, int j, int x, GameObject fac)
    {
        factories.Add(new Vector3(i, j, x));
        fac.GetComponent<Supply>().type = x;
    }

    public void addCell(int i, int j, int x)
    {
        map[i, j] = x;
    }

    public bool isConv(int x, int y)
    {
        return 0 <= map[x, y] && map[x, y] <= 3;
    }

    public void createResourse(int type, float x, float y, int o)
    {
        GameObject newResorse = Instantiate(resourses[type], new Vector3(x, 0.32f, y), Quaternion.identity);
        newResorse.GetComponent<Motion>().last = o;
    }

    public void AddItem(Color clr)
    {

    }

    private void Update()
    {
        
    }
}
