using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt05 : MonoBehaviour {

    public int ang;

	void Update ()
    {
        transform.rotation = Quaternion.Euler(0, ang, 0);
	}
}
