using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InputAnswer
{
    public bool isPaused;
    public bool isSingleClicked;
    public bool isPathRecorded;
    public Vector3 singleClickPosition;
    public Vector3[] path;

    public void ClearPlayModeParameters()
    {
        isSingleClicked = false;
        isPathRecorded = false;
        path = null;
        
    }
}
