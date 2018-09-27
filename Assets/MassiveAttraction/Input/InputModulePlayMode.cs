using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputModulePlayMode : NonMonoBehaviourBaseModuleAccess
{
    private InputAnswer inputAnswer;

    private bool isSingeClickDetectctionPhaseTriggerd;
    private float singleClickDetectionStartTime;

    private bool isPathArrayFull;
    private bool isRecordingPath;
    private int pathRecordIndex;
    private int pathCapacity;
    private Vector3[] path;

    public InputAnswer ListenToInput()
    {
        inputAnswer.ClearPlayModeParameters();
        if (Input.GetMouseButton(0) && isRecordingPath == false)
        {
            pathRecordIndex = 0;
            path[pathRecordIndex] = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isRecordingPath = true;

            if (isSingeClickDetectctionPhaseTriggerd == false)
            {
                isSingeClickDetectctionPhaseTriggerd = true;
                singleClickDetectionStartTime = Time.time;
            }
        }
        else if (Input.GetMouseButton(0) && isRecordingPath == true && isPathArrayFull == false)
        {
            pathRecordIndex++;
            if (pathRecordIndex < pathCapacity)
            {
                path[pathRecordIndex] = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (pathRecordIndex == 63) { isPathArrayFull = true; }
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            isPathArrayFull = false;
            isRecordingPath = false;
            isSingeClickDetectctionPhaseTriggerd = false;

            float detectionTimePassed = Time.time - singleClickDetectionStartTime;
            if (detectionTimePassed < 0.2f)
            {
                inputAnswer.isSingleClicked = true;
                inputAnswer.singleClickPosition = path[0];
            }
            else
            {
                inputAnswer.isPathRecorded = true;
                inputAnswer.path = new Vector3[pathRecordIndex];
                for (int i = 0; i < pathRecordIndex; i++)
                {
                    inputAnswer.path[i] = path[i];
                }
            }
        }

        return inputAnswer;
    }
    public InputModulePlayMode()
    {
        pathCapacity = 128;
        path = new Vector3[pathCapacity];
        for (int i = 0; i < path.Length; i++)
        {
            path[i] = new Vector3(0, 0, 0);
        }
        inputAnswer = new InputAnswer();
    }
}



