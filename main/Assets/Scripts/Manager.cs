using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;
using System.IO;

public class Manager : MonoBehaviour
{
    public GameObject selectCell;
    public GameObject location;
    public GameObject[] resourses;
    public bool pause = false;
    public Canvas canvas;
    public GameObject item;
    public ActionScript actionScript;
    public GameObject allItems;
    public float max_time = 0;
    public bool used = true;
    public int num = 0;

    private int cnt = 0;
    private int[,] dd = {
        {-1, 0},
        {1, 0},
        {0, 1},
        {0, -1}
    };

    public int ToInt(string s)
    {
        int x = 0;
        for (int i = 0; i < s.Length; i++)
        {
            x *= 10;
            x += s[i] - '0';
        }
        return x;
    }

    public int[,] map = new int[10,10];
    List<Vector3> factories = new List<Vector3>();
    public GameObject[,] pots = new GameObject[10, 10];
    Dictionary<Color, GameObject> stock = new Dictionary<Color, GameObject>();
    public Dictionary<int, Color> colors = new Dictionary<int, Color>();

    private void Start()
    {
        for (int i = 0; i < 10; i++)
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
        try
        {
            string s = @"input";
            if (num + 1 < 10) s += "0";
            s += (num + 1).ToString();
            s += ".txt";
            using (StreamReader sr = new StreamReader(s, System.Text.Encoding.Default))
            {
                string line;
                line = sr.ReadLine();
                int n = ToInt(line);
                colors.Add(0, resourses[0].GetComponent<Motion>().clr);
                colors.Add(1, resourses[1].GetComponent<Motion>().clr);
                colors.Add(2, resourses[2].GetComponent<Motion>().clr);
                colors.Add(3, resourses[3].GetComponent<Motion>().clr);
                for (int i = 0; i < n; i++)
                {
                    line = sr.ReadLine();
                    string[] a = line.Split(' ');
                    float x = float.Parse(a[0]) + 5;
                    float y = float.Parse(a[1]) + 5;
                    int type = int.Parse(a[2]);
                    GameObject t = Instantiate(actionScript.objects[type], new Vector3(x, 0, y), Quaternion.identity, allItems.transform);
                    map[(int)x, (int)y] = 100;
                }
                line = sr.ReadLine();
                n = int.Parse(line);
                for (int i = 0; i < n; i++)
                {
                    line = sr.ReadLine();
                    string[] a = line.Split(' ');
                    float x = float.Parse(a[0]) + 5;
                    float y = float.Parse(a[1]) + 5;
                    int t1 = int.Parse(a[2]);
                    int t2 = int.Parse(a[3]);
                    int t3 = int.Parse(a[4]);
                    float dx = -float.Parse(a[5]);
                    float dy = -float.Parse(a[6]);
                    for (int o = 0; o < 4; o++)
                    {
                        if (dx == dd[o, 0] && dy == dd[o, 1])
                        {
                            GameObject t = Instantiate(actionScript.objects[9 + o], new Vector3(x, 0, y), Quaternion.identity, allItems.transform);
                            t.GetComponent<Factory>().a = colors[t1];
                            t.GetComponent<Factory>().b = colors[t2];
                            t.GetComponent<Factory>().id = t3;
                            t.GetComponent<Factory>().initalization = false;
                            Color c = new Color((colors[t1].r + colors[t2].r) / 2, (colors[t1].g + colors[t2].g) / 2, (colors[t1].b + colors[t2].b) / 2);
                            colors[t3] = c;
                            pots[(int)x, (int)y] = t;
                            map[(int)x, (int)y] = 100;
                        }
                    }
                }
                line = sr.ReadLine();
                n = int.Parse(line);
                for (int i = 0; i < n; i++)
                {
                    line = sr.ReadLine();
                    string[] a = line.Split(' ');
                    float x = float.Parse(a[0]) + 5;
                    float y = float.Parse(a[1]) + 5;
                    float dx = -float.Parse(a[2]);
                    float dy = -float.Parse(a[3]);
                    for (int o = 0; o < 4; o++)
                    {
                        if (dx == dd[o, 0] && dy == dd[o, 1])
                        {
                            GameObject t = Instantiate(actionScript.objects[4 + o], new Vector3(x, 0, y), Quaternion.identity, allItems.transform);
                            map[(int)x, (int)y] = o;
                        }
                    }
                }
                line = sr.ReadLine();
                n = int.Parse(line);
                for (int i = 0; i < n; i++)
                {
                    line = sr.ReadLine();
                    string[] a = line.Split(' ');
                    float x = float.Parse(a[0]) + 5;
                    float y = float.Parse(a[1]) + 5;
                    GameObject t = Instantiate(actionScript.objects[8], new Vector3(x, 0, y), Quaternion.identity, allItems.transform);
                    map[(int)x, (int)y] = 10;
                }
                line = sr.ReadLine();
                max_time = float.Parse(line);
                if(max_time == -1)
                {
                    max_time = 1000000000;
                }
            }
        }
        catch (IOException a) { }
    }

    public void WriteFile()
    {
        used = true;
        using (StreamWriter sw = new StreamWriter(@"output.txt", false, System.Text.Encoding.Default))
        {
            int cnt = 0;
            for (int i = 0; i < allItems.transform.childCount; i++)
            {
                if (allItems.transform.GetChild(i).GetComponent<Motion>())
                {
                    cnt++;
                }
            }
            sw.WriteLine(cnt);
            for (int i = 0; i < allItems.transform.childCount; i++)
            {
                if (allItems.transform.GetChild(i).GetComponent<Motion>())
                {
                    Transform a = allItems.transform.GetChild(i);
                    sw.WriteLine((Mathf.Round(a.position.x * 10) / 10.0f - 5).ToString() + " " + (Mathf.Round(a.position.z * 10) / 10.0f - 5).ToString() + " " + allItems.transform.GetChild(i).GetComponent<Motion>().id);
                }
            }
        }
    }

    public void Reset()
    {
        num = (num + 1) % 99;
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
        if (pause) return;
        max_time -= Time.deltaTime;
        if(max_time <= 0)
        {
            pause = true;
            if (used)
            {
                WriteFile();
                used = false;
            }
        }
    }
}
