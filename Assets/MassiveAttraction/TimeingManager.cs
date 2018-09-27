using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeingManager : NonMonoBehaviourBaseModuleAccess
{
    private List<TimerInstance> timersProcessing;

    private float timeWhenToggledToPauseMode;
    private bool isInPlayMode = true;

    public void ToggleToPlayMode()
    {
        isInPlayMode = false;
        float timePassedInPauseMode = Time.time - timeWhenToggledToPauseMode;
        AddPasueModeTimePassedToTimers(timePassedInPauseMode);
    }
    public void ToggleToPauseMode()
    {
        isInPlayMode = true;
        timeWhenToggledToPauseMode = Time.time;
    }

    public void SchoudleDelayedFunctionTrigger(float duration, Action _functionToBeCalledAfterDurationTimePassed)
    {
       
       float timerInstanceEndTime = Time.time + duration;
       TimerInstance newTimer = new TimerInstance(timerInstanceEndTime, _functionToBeCalledAfterDurationTimePassed);
       timersProcessing.Add(newTimer);
    }

    public void ProcessTimers()
    {
        if(isInPlayMode == true)
        {
            float currentTime = Time.time;

                for (int i = timersProcessing.Count-1; i >= 0; i--)
                {
                    timersProcessing[i].CheckIfTimerReachedFinishTime(currentTime);
                    if (timersProcessing[i].isCorutineFinished)
                    {
                        timersProcessing.RemoveAt(i);
                    }
                }
        }
    }

    private void AddPasueModeTimePassedToTimers(float _timePassedInPauseMode)
    {
        for (int i = 0; i < timersProcessing.Count ; i++)
        {
            timersProcessing[i].AddTimeToTimerEndTime(_timePassedInPauseMode);
        }
    }
    public TimeingManager()
    {
        timersProcessing = new List<TimerInstance>();
    }
    public void PreformTimerManagerCycle()
    {
        ProcessTimers();
    }
}
