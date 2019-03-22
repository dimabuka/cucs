using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {

	public bool initalization = true;
	public GameObject jam;
    public int nap;

	public Color a, b;
    public int ida = -1, idb = -1;
    public int id;
	private bool first = false;
	private int cnt1 = 0, cnt2 = 0;
    private Manager manager;

    private int[,] dd = {
        {-1, 0},
        {1, 0},
        {0, 1},
        {0, -1}
    };

    List<GameObject> as1 = new List<GameObject>(), bs1 = new List<GameObject>();

    public void NewItem(Color clr, int id, GameObject u)
	{
        Debug.Log(id);
		if (initalization) {
			if (!first) {
				a = clr;
				first = true;
				cnt1++;
                as1.Add(u);
			} else {
				b = clr;
				initalization = false;
				cnt2++;
                bs1.Add(u);
			}
		} else {
            if(ida == idb && id == ida)
            {
                if (cnt1 < cnt2)
                {
                    cnt1++;
                    as1.Add(u);
                }
                else
                {
                    cnt2++;
                    bs1.Add(u);
                }
                return;
            }
			if (clr == a || id == ida) {
				cnt1++;
                as1.Add(u);
			}
			if (clr == b || id == idb) {
				cnt2++;
                bs1.Add(u);
			}
		}
	}

    private void Start()
    {
        manager = FindObjectOfType<Manager>();
    }

    void Update () {
        if (!initalization) {
            if (cnt1 != 0 && cnt2 != 0)
			{
                int tx = (int)transform.position.x + dd[nap, 0];
                int ty = (int)transform.position.z + dd[nap, 1];
                if (0 <= tx && tx < 10 && 0 <= ty && ty < 10 && (manager.isConv(tx, ty) || manager.map[tx, ty] == 10))
                {
                    GameObject tmp = Instantiate(jam, new Vector3(transform.position.x, 0.3f, transform.position.z), Quaternion.identity, manager.allItems.transform);
                    tmp.GetComponent<Motion>().last = nap;
                    tmp.GetComponent<Motion>().id = id;
                    tmp.GetComponent<Motion>().clr = new Color((a.r + b.r) / 2, (a.g + b.g) / 2, (a.b + b.b) / 2);
                    cnt1--;
                    cnt2--;
                    Destroy(as1[as1.Count - 1].GetComponent<Motion>());
                    as1.RemoveAt(as1.Count - 1);
                    Destroy(bs1[bs1.Count - 1].GetComponent<Motion>());
                    bs1.RemoveAt(bs1.Count - 1);
                    Material mater = tmp.transform.GetChild(1).GetComponent<Renderer>().material;
                    mater.color = new Color((a.r + b.r) / 2, (a.g + b.g) / 2, (a.b + b.b) / 2);
                }
			}
		}
	}
}