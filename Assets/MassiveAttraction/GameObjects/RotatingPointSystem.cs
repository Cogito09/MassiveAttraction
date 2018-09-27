using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotatingPointSystemType
{
    PlayerFollowSystem,
    ImploderFollowSystem,
    OrbitatingSystem,
    OrbitatingSystem3D,
}
public class RotatingPointSystem : NonMonoBehaviourBaseModuleAccess
{
    public RotatingPointSystemType SystemType;
    public float RotationSpeed;


    public PointRing[] SystemPointRings;

    public Dictionary<int, bool> TableOfAvaiablePointPositionsTransformsForActiveForceObjects;
    public Dictionary<int, bool> TableOfAvaiablePointPositionsTransformsForUnactiveFoceObjects;
    public Transform[] PositionPointTransformsForActiveForceObjects;
    public Transform[] PositionPointTransformsForUnactiveFoceObjects;

    public void PreformBehaviour(float _speed)
    {
        if(SystemType == RotatingPointSystemType.PlayerFollowSystem) { PreformPlayerFollowSystemBehaviour(_speed); }
    }
    public void PreformPlayerFollowSystemBehaviour(float _speed)
    {
        for(int i= 0; i <SystemPointRings.Length; i++)
        {
            SystemPointRings[i].ProcessBehaviour(_speed);
        }
    }
    public void DetachFromUnactives(int _index)
    {
        TableOfAvaiablePointPositionsTransformsForUnactiveFoceObjects[_index] = true;
    }
    public void DetachFromAcives(int _index)
    {
        TableOfAvaiablePointPositionsTransformsForActiveForceObjects[_index] = true;
    }
    public void FindFollowPointAsUnactive(IPointSystemAttachebleObject _object)
    {
        int _index = FindEmptySlotAt(TableOfAvaiablePointPositionsTransformsForUnactiveFoceObjects);
        _object.SetFollowTransform(PositionPointTransformsForUnactiveFoceObjects[_index]);
        _object.SetFollowPointIndex(_index);
    }
    public void FindFollowPointAsActive(IPointSystemAttachebleObject _object)
    {
        int _index = FindEmptySlotAt(TableOfAvaiablePointPositionsTransformsForActiveForceObjects);
        _object.SetFollowTransform(PositionPointTransformsForActiveForceObjects[_index]);
        _object.SetFollowPointIndex(_index);
    }

    public int FindEmptySlotAt(Dictionary<int, bool> _dictionayOfSlots)
    {
        int _index = 0;
        for(int i = 0; i< _dictionayOfSlots.Count; i++)
        {
            if(_dictionayOfSlots[i] == true)
            {
                Debug.Log("EmptySlotFound");
                _dictionayOfSlots[i] = false;
                _index =  i;
            }
        }
        return _index;
    }


}
