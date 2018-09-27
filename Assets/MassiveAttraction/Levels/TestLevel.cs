using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevel : Level
{
    

    public override void SetupLevelSettings()
    {
        PlayerStartPosition = new Vector3(0, 0, 0);
        Stages = new Stage[3];
        Stages[0] = StageCreator.CreateStage(new Vector3(3, 3, 0),20, 2,16);
        Stages[1] = StageCreator.CreateStage(new Vector3(9, 0, 0), 20, 2, 15);
        Stages[2] = StageCreator.CreateStage(new Vector3(-4, -12, 0), 20, 20, 1200,CoreParametersCreator.CreateCoreParameters(2500,5,20));

        ForceObjectStartSpawnParameters = new ForceObjectStartSpawnParameters();
        ForceObjectStartSpawnParameters.DefendersCount = 3;
        ForceObjectStartSpawnParameters.ExplodersCount = 3;
        ForceObjectStartSpawnParameters.ImplodersCount = 3;
    }

}
