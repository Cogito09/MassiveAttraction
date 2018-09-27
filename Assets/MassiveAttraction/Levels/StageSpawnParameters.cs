using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSpawnParameters
{
    public Core Core;
    public float MeteorsSpawnSpeed;
    public float BombersSpawnSpeed;


    public StageSpawnParameters(Core _core,float _meteorsSpawnSpeed, float _bombersSpawnSpeed)
    {
        Core = _core;
        MeteorsSpawnSpeed = _meteorsSpawnSpeed;
        BombersSpawnSpeed = _bombersSpawnSpeed;
    }
    public StageSpawnParameters(float _meteorsSpawnSpeed, float _bombersSpawnSpeed)
    {
        MeteorsSpawnSpeed = _meteorsSpawnSpeed;
        BombersSpawnSpeed = _bombersSpawnSpeed;
    }
}
