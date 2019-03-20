using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt05 : MonoBehaviour {

    public int ang;

	void Update ()
    {
        transform.LookAt(new Vector3(5, 0, 10));
        transform.Rotate(new Vector3(-transform.rotation.x, ang, -transform.rotation.y));
	}
}
