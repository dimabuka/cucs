using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour {

	public bool initalization = true;
	public GameObject jam;
    public int nap;

	private Color a, b;
	private bool first = false;
	private int cnt1 = 0, cnt2 = 0;
    private Manager manager;

    private int[,] dd = {
        {-1, 0},
        {1, 0},
        {0, 1},
        {0, -1}
    };

    public void NewItem(Color clr)
	{
		if (initalization) {
			if (!first) {
				a = clr;
				first = true;
				cnt1++;
			} else {
				if (clr != a) {
					b = clr;
					initalization = false;
					cnt2++;
				} else {
					cnt1++;
				}
			}
		} else {
			if (clr == a) {
				cnt1++;
			}
			if (clr == b) {
				cnt2++;
			}
		}
	}

    private void Start()
    {
        manager = FindObjectOfType<Manager>();
    }

    void Update () {
        Debug.Log(new Vector2(cnt1, cnt2));
        if (!initalization) {
			if(cnt1 != 0 && cnt2 != 0)
			{
                int tx = (int)transform.position.x + dd[nap, 0];
                int ty = (int)transform.position.z + dd[nap, 1];
                if (0 <= tx && tx < 10 && 0 <= ty && ty < 10 && manager.isConv(tx, ty))
                {
                    GameObject tmp = Instantiate(jam, transform.position, Quaternion.identity);
                    tmp.GetComponent<Motion>().last = nap;
                    cnt1--;
                    cnt2--;
                    Material mater = tmp.transform.GetChild(1).GetComponent<Renderer>().material;
                    mater.color = new Color((a.r + b.r) / 2, (a.g + b.g) / 2, (a.b + b.b) / 2);
                }
			}
		}
	}
}