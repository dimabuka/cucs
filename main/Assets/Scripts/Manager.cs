using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public GameObject teleportPoint;
    public GameObject teleport;
    
	void Start ()
    {
        /* Расстановка телепортов */
		for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                GameObject tmpTeleport = Instantiate(teleportPoint, new Vector3(i, 0, j), Quaternion.identity);
                tmpTeleport.transform.parent = teleport.transform;
            }
        }
	}
	
	void Update () {
		
	}
}
