using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{

    public Manager manager;
    public Color clr;
    int debug = 0;
    int time_stay = 0;
    float tm = 0;
    public int id = 0;
    public GameObject explosion;
    bool f = true;

    private int[,] dd = {
        {-1, 0},
        {1, 0},
        {0, 1},
        {0, -1}
    };

    public int last = 1;

    void Start()
    {
        manager = FindObjectOfType<Manager>();
    }

    void KillMe(int type = 1)
    {
        if (type == 1)
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity, manager.allItems.transform);
        }
        else f = false;
    }

    void Update()
    {
        if (!f) return;
        if (manager.pause) return;
        tm += Time.deltaTime;
        debug += 1;
        int x = (int)Mathf.Floor(transform.position.x);
        int y = (int)Mathf.Floor(transform.position.z);
        if(x >= 0 && y >= 0 && x < 10 && y < 10 && 0 <= last && last <= 3)
        {
            int tx = dd[last, 0];
            int ty = dd[last, 1];
            time_stay = 0;
            transform.Translate(new Vector3(tx, 0, ty) * Time.deltaTime);
            Vector3 pos = transform.position;
            if (tx != 0 && Mathf.Abs(pos.x - Mathf.Floor(pos.x) - 0.5f) < 0.01f && debug > 0)
            {
                last = manager.map[x, y];
                transform.position = new Vector3(x + 0.5f, transform.position.y, y + 0.5f);
            }
            if (ty != 0 && Mathf.Abs(pos.z - Mathf.Floor(pos.z) - 0.5f) < 0.01f && debug > 0)
            {
                last = manager.map[x, y];
                transform.position = new Vector3(x + 0.5f, transform.position.y, y + 0.5f);
            }
            int cur = manager.map[x, y];
            if (cur == -1) // Земля
            {
                KillMe(1);
            }
            if ((manager.map[x, y] == 5 || manager.map[x, y] == 6 || manager.map[x, y] == 7 || manager.map[x, y] == 8) && tm > 1) //источник
            {
                KillMe(1);
            }
            if (last == 10) // Попадание на склад
            {
                manager.addItem(clr);
                KillMe(0);
            }
            if (last == 100) // Попадание в котел
            {
                manager.pots[x, y].GetComponent<Factory>().NewItem(clr);
                KillMe(1);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
