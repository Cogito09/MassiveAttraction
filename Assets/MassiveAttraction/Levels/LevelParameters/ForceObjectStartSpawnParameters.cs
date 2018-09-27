using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceObjectStartSpawnParameters
{
    public int ImplodersCount ;
    public int ExplodersCount ;
    public int DefendersCount ;

    public ForceObjectStartSpawnParameters()
    {

    }
    public ForceObjectStartSpawnParameters(int _implodersCount,int _explodersCount,int _defendersCount)
    {
        ImplodersCount = _implodersCount;
        ExplodersCount = _explodersCount;
        DefendersCount = _defendersCount;
    }
}
