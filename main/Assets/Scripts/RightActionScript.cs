using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RightActionScript : MonoBehaviour {

    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Vector2 touchPad;
    public Manager manager;   


	void Update ()
    {
        if(touchPad.GetAxis(handType).x < 0)
        {

        }
        if(touchPad.GetAxis(handType).x > 0)
        {

        }
    }
}
