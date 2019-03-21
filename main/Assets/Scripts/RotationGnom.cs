using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationGnom : MonoBehaviour {

    float x = 0.1f;
    int u = 0;

	void Start () {
		
	}
	
	void Update () {
        if(u % 10 == 0)
        { 
            transform.Translate(0, x, 0);
            x *= -1;
        }
        u++;
	}
}
