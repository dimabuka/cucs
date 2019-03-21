using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : MonoBehaviour
{
    private int[,] dd = {
        {-1, 0},
        {1, 0},
        {0, 1},
        {0, -1}
    };

    public int id = 0;
    public Color clr;

    public int type;
    public Manager manager;

    private void Start()
    {
        manager = FindObjectOfType<Manager>();
    }

    private float t = 0;

    void Update ()
    {
        if (manager.pause) return;
        t += Time.deltaTime;
        if(t >= 2)
        {
            t = 0;
            for (int o = 0; o < 4; o++)
            {
                int tx = dd[o, 0] + (int)transform.position.x;
                int ty = dd[o, 1] + (int)transform.position.z;
                if (tx >= 0 && ty >= 0 && tx < 10 && ty < 10 && 0 <= manager.map[tx, ty] && manager.map[tx, ty] <= 3)
                {
                    manager.createResourse(type, transform.position.x, transform.position.z, o);
                }
            }
        }
	}
}
