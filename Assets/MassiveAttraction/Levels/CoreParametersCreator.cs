using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreParametersCreator :  NonMonoBehaviourBaseModuleAccess
{
    public CoreParameters CreateCoreParameters(float _hp,float _meteorSpawnRate,float _bomberSpawnRate)
    {
        CoreParameters theCoreBlueprint = new CoreParameters();
        theCoreBlueprint.Hp = _hp;
        theCoreBlueprint.MeteorSpawnRate = _meteorSpawnRate;
        theCoreBlueprint.BomberSpawnRate = _bomberSpawnRate;
        return theCoreBlueprint;
    }

}
