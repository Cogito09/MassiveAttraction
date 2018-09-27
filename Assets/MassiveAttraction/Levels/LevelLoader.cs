using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : NonMonoBehaviourBaseModuleAccess
{
    private LevelCollection levelCollection;
    private Level currentLevelParameters;

    public Level SetupAndReturnChoosenLevel(Level _LevelToBeSetupAndReturned)
    {
        currentLevelParameters = _LevelToBeSetupAndReturned;
        currentLevelParameters.SetupLevelSettings();
        return currentLevelParameters;

    }
    public LevelLoader()
    {
       
    }
}
  