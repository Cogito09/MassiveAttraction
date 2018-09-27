using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreator : NonMonoBehaviourBaseModuleAccess
{
    public Stage CreateStage(Vector3 _playerFollowLocation, float stageDuration, float meteorSpawnTimeStep, float bomberSpawnTimeStep)
    {
        Stage newCreatedStage = new Stage();
        newCreatedStage.isItCoreStage = false;
        newCreatedStage.FollowPosition = _playerFollowLocation;
        newCreatedStage.DurationOfStage = stageDuration;
        newCreatedStage.MeteorsSpawnSpeed = meteorSpawnTimeStep;
        newCreatedStage.BombersSpawnSpeed = bomberSpawnTimeStep;
        return newCreatedStage;

    }
 



    public Stage CreateStage(Vector3 _playerFollowLocation, float stageDuration, float meteorSpawnTimeStep, float bomberSpawnTimeStep, CoreParameters passCoreParameters)
    {
        Stage newCreatedStage = new Stage();
        newCreatedStage.isItCoreStage = true;
        newCreatedStage.FollowPosition = _playerFollowLocation;
        newCreatedStage.DurationOfStage = stageDuration;
        newCreatedStage.MeteorsSpawnSpeed = meteorSpawnTimeStep;
        newCreatedStage.BombersSpawnSpeed = bomberSpawnTimeStep;
        newCreatedStage.CoreParameters = passCoreParameters;
        return newCreatedStage;
    }

}
