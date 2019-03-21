using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject selectCell;
    public GameObject location;
    public GameObject[] resourses;
    public bool pause = false;
    public Canvas canvas;
    public GameObject item;
    public GameObject allItems;

    private int cnt = 0;
    private int[,] dd = {
        {-1, 0},
        {1, 0},
        {0, 1},
        {0, -1}
    };

    public int[,] map = new int[10,10];
    List<Vector3> factories = new List<Vector3>();
    public GameObject[,] pots = new GameObject[10, 10];
    Dictionary<Color, GameObject> stock = new Dictionary<Color, GameObject>();

    private void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                map[i, j] = -1;
                GameObject cell = Instantiate(selectCell, new Vector3(i + 0.5f, 0.01f, j + 0.5f), Quaternion.identity, allItems.transform);
                cell.transform.parent = location.transform.Find("SelectedCells");
                cell.SetActive(false);
                cell.name = "SCell" + i.ToString() + j.ToString();
            }
        }
    }

    public void Reset()
    {
        for(int i = allItems.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(allItems.transform.GetChild(i).gameObject);
        }
        for(int i = canvas.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(canvas.transform.GetChild(i).gameObject);
        }
        for(int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                map[i, j] = -1;
            }
        }
        stock.Clear();
        factories.Clear();
    }

    public void addPot(int i, int j, GameObject a, int nap)
    {
        pots[i, j] = a;
        a.GetComponent<Factory>().nap = nap;
    }

    public void addFactory(int i, int j, int x, GameObject fac)
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

    public void createResourse(int type, float x, float y, int o)
    {
        GameObject newResorse = Instantiate(resourses[type], new Vector3(x, 0.32f, y), Quaternion.identity, allItems.transform);
        newResorse.GetComponent<Motion>().last = o;
    }

    public void addItem(Color clr)
    {
        if(stock.ContainsKey(clr))
        {
            string s = stock[clr].transform.GetChild(2).GetComponent<Text>().text.Substring(2, stock[clr].transform.GetChild(2).GetComponent<Text>().text.Length - 2);
            int num = 0;
            for(int i = 0; i < s.Length; i++)
            {
                num *= 10;
                num += s[i] - '0';
            }
            num++;
            stock[clr].transform.GetChild(2).GetComponent<Text>().text = "X " + num.ToString();
        }
        else
        {
            GameObject it = Instantiate(item, canvas.transform.position, canvas.transform.rotation, canvas.transform);
            stock[clr] = it;
            it.transform.GetChild(1).gameObject.GetComponent<Image>().color = new Color(clr.r, clr.g, clr.b, 1);
            it.transform.GetChild(2).gameObject.GetComponent<Text>().text = "X 1";
            it.transform.Translate(new Vector3(cnt / 4 * 2, -cnt % 4 * 0.5f, 0));
            cnt++;
        }
    }

    private void Update()
    {
        
    }
}
