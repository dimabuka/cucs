using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{

    public Manager manager;
    public Color clr;

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

    void Update()
    {
        int x = (int)Mathf.Floor(transform.position.x);
        int y = (int)Mathf.Floor(transform.position.z);
        if(x >= 0 && y >= 0 && x < 10 && y < 10 && 0 <= last && last <= 3)
        { 
            int tx = dd[last, 0];
            int ty = dd[last, 1];
            transform.Translate(new Vector3(tx, 0, ty) * Time.deltaTime);
            Vector3 pos = transform.position;
            if (tx != 0 && Mathf.Abs(pos.x - Mathf.Floor(pos.x) - 0.5f) < 0.01f)
            {
                last = manager.map[x, y];
            }
            if (ty != 0 && Mathf.Abs(pos.z - Mathf.Floor(pos.z) - 0.5f) < 0.01f)
            {
                last = manager.map[x, y];
            }
            int cur = manager.map[x, y];
            if (cur == -1) // Земля или источник
            {
                Destroy(gameObject);
            }
            if(last == 10) // Попадание на склад
            {
                Destroy(GetComponent<Motion>());
            }
            if(last == 100) // Попадание в котел
            {
                manager.pots[x, y].GetComponent<Factory>().NewItem(clr);
                Destroy(GetComponent<Motion>());
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
