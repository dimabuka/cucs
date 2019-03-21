using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Manager : MonoBehaviour
{
    public GameObject selectCell;
    public GameObject location;
    public GameObject[] resourses;

    private int[,] dd = {
        {-1, 0},
        {1, 0},
        {0, 1},
        {0, -1}
    };

    public int[,] map = new int[10,10];
    List<Vector3> factories = new List<Vector3>();
    public GameObject[,] pots = new GameObject[10, 10];

    IEnumerator createCube()
    {
        while (true)
        {
            for (int i = 0; i < factories.Count; i++)
            {
                for(int o = 0; o < 4; o++)
                {
                    int tx = dd[o, 0] + (int)factories[i].x;
                    int ty = dd[o, 1] + (int)factories[i].y;
                    if(tx >= 0 && ty >= 0 && tx < 10 && ty < 10 && 0 <= map[tx, ty] && map[tx, ty] <= 3)
                    {
                        GameObject newResorse = Instantiate(resourses[(int)factories[i].z], new Vector3(factories[i].x + 0.5f, 0.2f, factories[i].y + 0.5f), Quaternion.identity);
                        newResorse.GetComponent<Motion>().last = o;
                    }
                }
            }
            yield return new WaitForSeconds(2.0f);
        }
    }

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
        StartCoroutine(createCube());
    }

    public void addPot(int i, int j, GameObject a)
    {
        pots[i, j] = a;
    }

    public void addFactory(int i, int j, int x)
    {
        factories.Add(new Vector3(i, j, x));
    }

    public void addCell(int i, int j, int x)
    {
        map[i, j] = x;
    }

    public bool isConv(int x, int y)
    {
        return 0 <= map[x, y] && map[x, y] <= 3;
    }

    private void Update()
    {
        
    }
}
