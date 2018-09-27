using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerInstance : NonMonoBehaviourBaseModuleAccess
{
    public bool isCorutineFinished;
    private  float corutineEndTime;
    private Action functionToLaunchAfterCorutineFinished;


    public void CheckIfTimerReachedFinishTime(float _currentTime)
    {
        if (_currentTime >= corutineEndTime)
        {
            functionToLaunchAfterCorutineFinished();
            isCorutineFinished = true;
        }
    }
    public TimerInstance(float _corutineEndTime, Action _functionToLaunchAfterCorutineFinished)
    {
        corutineEndTime = _corutineEndTime;
        functionToLaunchAfterCorutineFinished = _functionToLaunchAfterCorutineFinished;
        isCorutineFinished = false;
    }

    public void AddTimeToTimerEndTime(float _time)
    {
        corutineEndTime = _time;
    }
}
