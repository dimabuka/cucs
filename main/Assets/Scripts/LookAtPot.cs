using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPot : MonoBehaviour {

    public int ang;

	void Start () {
        transform.Rotate(new Vector3(90, 0, 90 + ang));
        transform.Translate(new Vector3(0, 0, -0.3f));
	}
	
	void Update () {
		
	}
}
