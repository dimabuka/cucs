using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class RightActionScript : MonoBehaviour {

    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Vector2 touchPad;
    public Manager manager;
    public SteamVR_Action_Boolean resetBtn;

    private bool firstTouchReset = true;
    private bool firstTouchPause = true;
    private bool firstTouchGrid = true;

    void Update ()
    {
        if (touchPad.GetAxis(handType).x < 0)
        {
            if(firstTouchPause)
            {
                firstTouchPause = false;
                manager.pause = !manager.pause;
            }
        }
        else firstTouchPause = true;
        if(touchPad.GetAxis(handType).x > 0)
        {

        }

        if (resetBtn.GetState(handType))
        {
            if (firstTouchReset)
            {
                firstTouchReset = false;
                manager.Reset();
            }
        }
        else firstTouchReset = true;
    }
}
