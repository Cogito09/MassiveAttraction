using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : NonMonoBehaviourBaseModuleAccess
{
    public Vector3 PlayerStartPosition;
    public Stage[] Stages;
    public ForceObjectStartSpawnParameters ForceObjectStartSpawnParameters;

    public virtual void SetupLevelSettings()
    {
        PlayerStartPosition = new Vector3(0, 0, 0);
        Debug.Log("BaseLevelSetupFunctuionLaunched");
    }
}
