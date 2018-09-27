using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputModuleUIMode : NonMonoBehaviourBaseModuleAccess
{
    private InputAnswer inputAnswer;

    public InputAnswer ListenInput()
    {
        Debug.Log("UI Inpuit TODO");
        inputAnswer = new InputAnswer();
        inputAnswer.isPaused = false;
        return inputAnswer;
    }
}
