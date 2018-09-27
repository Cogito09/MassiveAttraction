using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableObject : MonoBehaviourBaseModuleAccessObject
{
    public abstract int GetPoolKey();
    public abstract void  SetPoolKey(int key);

    public abstract void Setup();
    public abstract void Reset();
}
