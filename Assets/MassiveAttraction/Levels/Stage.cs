using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage
{
    public Vector3 FollowPosition;
    public float DurationOfStage;
    public float MeteorsSpawnSpeed;
    public float BombersSpawnSpeed;


    public bool isItCoreStage;
    public Core Core;
    public CoreParameters CoreParameters;

    public StageSpawnParameters GetStageSpawnParameters()
    {
        StageSpawnParameters _stageSpawnParameters;
        if (isItCoreStage)
        {
            _stageSpawnParameters = new StageSpawnParameters(Core, MeteorsSpawnSpeed, BombersSpawnSpeed);
        }
        else
        {
            _stageSpawnParameters = new StageSpawnParameters(MeteorsSpawnSpeed, BombersSpawnSpeed);
        }
        return _stageSpawnParameters;
    }

}
