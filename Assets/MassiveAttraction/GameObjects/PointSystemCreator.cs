using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystemCreator : NonMonoBehaviourBaseModuleAccess
{
    private float[] RingSizeingPatternForPlayerPointSystem = new float[] { 0.1f, 0.2f, 0.3f, 0.5f, 0.4f, 0.2f, 0.1f, 0.2f, 0.1f, 0.4f, 0.6f, 0.5f, 0.4f, 0.3f, 0.2f, 0.1f, 0.2f, 0.1f };
   
    public float GetPlayerSetupSizeingPatternOfRingsForIndex(int _indexOfAskedParameter)
    {
        return RingSizeingPatternForPlayerPointSystem[_indexOfAskedParameter];
    }

    public PointRing Create6PointRing(float _size)
    {
        PointRing _newPointRing = InstantiateModule.InstantiateObjectWithScript<PointRing>(PrefabCollection.PointRing);
        PositionPoint[] _positionPoints = new PositionPoint[6];
        for(int i = 0; i < _positionPoints.Length; i++)
        {
            _positionPoints[i] = InstantiateModule.InstantiateObjectWithScript<PositionPoint>(PrefabCollection.PositionPoint);
        }
        _positionPoints[0].transform.position = new Vector3(0 * _size, 2.7f  * _size,  5 * _size);
        _positionPoints[1].transform.position = new Vector3(0 * _size,-2.7f  * _size,  5 * _size);
        _positionPoints[2].transform.position = new Vector3(0 * _size,-6     * _size,  0 * _size);
        _positionPoints[3].transform.position = new Vector3(0 * _size,-2.7f  * _size, -5 * _size);
        _positionPoints[4].transform.position = new Vector3(0 * _size, 2.7f  * _size, -5 * _size);
        _positionPoints[5].transform.position = new Vector3(0 * _size, 6f    * _size,  0 * _size);

        _newPointRing.Points = _positionPoints;
        for (int i = 0; i < _positionPoints.Length; i++)
        {
            _positionPoints[i].transform.SetParent(_newPointRing.transform);
        }
        return _newPointRing;
    }

    public PointRing Create3PointRing(float _size)
    {
        PointRing _newPointRing = InstantiateModule.InstantiateObjectWithScript<PointRing>(PrefabCollection.PointRing);
        PositionPoint[] _positionPoints = new PositionPoint[3];
        for (int i = 0; i < _positionPoints.Length; i++)
        {
            _positionPoints[i] = InstantiateModule.InstantiateObjectWithScript<PositionPoint>(PrefabCollection.PositionPoint);
        }
        _positionPoints[0].transform.position = new Vector3(0 * _size, 2.7f * _size, 5 * _size);
        _positionPoints[1].transform.position = new Vector3(0 * _size, -6 * _size, 0 * _size);
        _positionPoints[2].transform.position = new Vector3(0 * _size, 2.7f * _size, -5 * _size);

        _newPointRing.Points = _positionPoints;
        for (int i = 0; i < _positionPoints.Length; i++)
        {
            _positionPoints[i].transform.SetParent(_newPointRing.transform);
        }
        return _newPointRing;
    }

    public PointRing Create3PointRingInverted(float _size)
    {
        PointRing _newPointRing = InstantiateModule.InstantiateObjectWithScript<PointRing>(PrefabCollection.PointRing);
        PositionPoint[] _positionPoints = new PositionPoint[3];
        for (int i = 0; i < _positionPoints.Length; i++)
        {
            _positionPoints[i] = InstantiateModule.InstantiateObjectWithScript<PositionPoint>(PrefabCollection.PositionPoint);
        }
        _positionPoints[0].transform.position = new Vector3(0 * _size, -2.7f * _size,  5 * _size);
        _positionPoints[1].transform.position = new Vector3(0 * _size, -2.7f * _size, -5 * _size);
        _positionPoints[2].transform.position = new Vector3(0 * _size,    6f * _size,  0 * _size);

        _newPointRing.Points = _positionPoints;
        for (int i = 0; i < _positionPoints.Length; i++)
        {
            _positionPoints[i].transform.SetParent(_newPointRing.transform);
        }
        return _newPointRing;
    }

    public RotatingPointSystem BuildPlayerFollowRingSetup(Transform _transformTheSystemIsAttachedTo)
    {
        RotatingPointSystem _newRotatingPointSystem = new RotatingPointSystem();

        bool createInveted = false;
        PointRing[] _systemPointRings = new PointRing[18];
        for (int i = 0; i < 18; i++)
        {
            if (createInveted == true)
            {
                createInveted = false;
                _systemPointRings[i] = PointSystemCreator.Create3PointRing(PointSystemCreator.GetPlayerSetupSizeingPatternOfRingsForIndex(i));
            }
            else
            {
                createInveted = true;
                _systemPointRings[i] = PointSystemCreator.Create3PointRingInverted(PointSystemCreator.GetPlayerSetupSizeingPatternOfRingsForIndex(i));
            }
            _systemPointRings[i].followTarget = _transformTheSystemIsAttachedTo;
            _systemPointRings[i].SpawnZAxisRotationParentObject();
            //_systemPointRings[i].transform.SetParent(_transformTheSystemIsAttachedTo);
        }
        //Setup Ring Follow Speed To Simulate Rings Spread

        //Actives Rings
        _systemPointRings[0].followSpeed = 8f;
        _systemPointRings[1].followSpeed = 7f;
        _systemPointRings[2].followSpeed = 5.6f;
        _systemPointRings[3].followSpeed = 4f;
        _systemPointRings[4].followSpeed = 3f;
        _systemPointRings[5].followSpeed = 2f;
        _systemPointRings[6].followSpeed = 1f;
        _systemPointRings[7].followSpeed = 0.8f;
        _systemPointRings[8].followSpeed = 0.5f;
        //Unactives Rings
        _systemPointRings[9].followSpeed = 6f;
        _systemPointRings[10].followSpeed = 4f;
        _systemPointRings[11].followSpeed = 3f;
        _systemPointRings[12].followSpeed = 2f;
        _systemPointRings[13].followSpeed = 1f;
        _systemPointRings[14].followSpeed = 0.7f;
        _systemPointRings[15].followSpeed = 0.4f;
        _systemPointRings[16].followSpeed = 0.2f;
        _systemPointRings[17].followSpeed = 0.14f;

        //SetupRotationSpeeds to Simulate GravitionalForces

        _systemPointRings[0].rotationSpeed = 5f;
        _systemPointRings[1].rotationSpeed = 4.6f;
        _systemPointRings[2].rotationSpeed = 2f;
        _systemPointRings[3].rotationSpeed = 1f;
        _systemPointRings[4].rotationSpeed = 2f;
        _systemPointRings[5].rotationSpeed = 3.5f;
        _systemPointRings[6].rotationSpeed = 5f;
        _systemPointRings[7].rotationSpeed = 2f;
        _systemPointRings[8].rotationSpeed = 3f;
        //Unactives Rings
        _systemPointRings[9].rotationSpeed = 1f;
        _systemPointRings[10].rotationSpeed = 0.7f;
        _systemPointRings[11].rotationSpeed = 1.2f;
        _systemPointRings[12].rotationSpeed = 1.8f;
        _systemPointRings[13].rotationSpeed = 2f;
        _systemPointRings[14].rotationSpeed = 4f;
        _systemPointRings[15].rotationSpeed = 5f;
        _systemPointRings[16].rotationSpeed = 7f;
        _systemPointRings[17].rotationSpeed = 2f;

        

        //CustomTransformSetupPattern
        Transform[] _positionPointTransformsForActiveForceObjects;
        Transform[] _positionPointTransformsForUnactiveForceObjects;

        _positionPointTransformsForActiveForceObjects = new Transform[27];
        _positionPointTransformsForActiveForceObjects[0] = _systemPointRings[0].Points[0].transform;
        _positionPointTransformsForActiveForceObjects[1] = _systemPointRings[1].Points[0].transform;
        _positionPointTransformsForActiveForceObjects[2] = _systemPointRings[2].Points[1].transform;
        _positionPointTransformsForActiveForceObjects[3] = _systemPointRings[0].Points[1].transform;
        _positionPointTransformsForActiveForceObjects[4] = _systemPointRings[1].Points[1].transform;
        _positionPointTransformsForActiveForceObjects[5] = _systemPointRings[2].Points[2].transform;
        _positionPointTransformsForActiveForceObjects[6] = _systemPointRings[0].Points[2].transform;
        _positionPointTransformsForActiveForceObjects[7] = _systemPointRings[1].Points[2].transform;
        _positionPointTransformsForActiveForceObjects[8] = _systemPointRings[2].Points[1].transform;
        _positionPointTransformsForActiveForceObjects[9] = _systemPointRings[3].Points[0].transform;
        _positionPointTransformsForActiveForceObjects[10] = _systemPointRings[4].Points[0].transform;
        _positionPointTransformsForActiveForceObjects[11] = _systemPointRings[5].Points[1].transform;
        _positionPointTransformsForActiveForceObjects[12] = _systemPointRings[3].Points[1].transform;
        _positionPointTransformsForActiveForceObjects[13] = _systemPointRings[4].Points[1].transform;
        _positionPointTransformsForActiveForceObjects[14] = _systemPointRings[5].Points[2].transform;
        _positionPointTransformsForActiveForceObjects[15] = _systemPointRings[3].Points[2].transform;
        _positionPointTransformsForActiveForceObjects[16] = _systemPointRings[4].Points[2].transform;
        _positionPointTransformsForActiveForceObjects[17] = _systemPointRings[5].Points[1].transform;
        _positionPointTransformsForActiveForceObjects[18] = _systemPointRings[6].Points[0].transform;
        _positionPointTransformsForActiveForceObjects[19] = _systemPointRings[7].Points[0].transform;
        _positionPointTransformsForActiveForceObjects[20] = _systemPointRings[8].Points[1].transform;
        _positionPointTransformsForActiveForceObjects[21] = _systemPointRings[6].Points[1].transform;
        _positionPointTransformsForActiveForceObjects[22] = _systemPointRings[7].Points[1].transform;
        _positionPointTransformsForActiveForceObjects[23] = _systemPointRings[8].Points[2].transform;
        _positionPointTransformsForActiveForceObjects[24] = _systemPointRings[6].Points[2].transform;
        _positionPointTransformsForActiveForceObjects[25] = _systemPointRings[7].Points[2].transform;
        _positionPointTransformsForActiveForceObjects[26] = _systemPointRings[8].Points[1].transform;

        _positionPointTransformsForUnactiveForceObjects = new Transform[27];
        _positionPointTransformsForUnactiveForceObjects[0] = _systemPointRings[0].Points[0].transform;
        _positionPointTransformsForUnactiveForceObjects[1] = _systemPointRings[1].Points[0].transform;
        _positionPointTransformsForUnactiveForceObjects[2] = _systemPointRings[2].Points[1].transform;
        _positionPointTransformsForUnactiveForceObjects[3] = _systemPointRings[0].Points[1].transform;
        _positionPointTransformsForUnactiveForceObjects[4] = _systemPointRings[1].Points[1].transform;
        _positionPointTransformsForUnactiveForceObjects[5] = _systemPointRings[2].Points[2].transform;
        _positionPointTransformsForUnactiveForceObjects[6] = _systemPointRings[0].Points[2].transform;
        _positionPointTransformsForUnactiveForceObjects[7] = _systemPointRings[1].Points[2].transform;
        _positionPointTransformsForUnactiveForceObjects[8] = _systemPointRings[2].Points[1].transform;
        _positionPointTransformsForUnactiveForceObjects[9] = _systemPointRings[3].Points[0].transform;
        _positionPointTransformsForUnactiveForceObjects[10] = _systemPointRings[4].Points[0].transform;
        _positionPointTransformsForUnactiveForceObjects[11] = _systemPointRings[5].Points[1].transform;
        _positionPointTransformsForUnactiveForceObjects[12] = _systemPointRings[3].Points[1].transform;
        _positionPointTransformsForUnactiveForceObjects[13] = _systemPointRings[4].Points[1].transform;
        _positionPointTransformsForUnactiveForceObjects[14] = _systemPointRings[5].Points[2].transform;
        _positionPointTransformsForUnactiveForceObjects[15] = _systemPointRings[3].Points[2].transform;
        _positionPointTransformsForUnactiveForceObjects[16] = _systemPointRings[4].Points[2].transform;
        _positionPointTransformsForUnactiveForceObjects[17] = _systemPointRings[5].Points[1].transform;
        _positionPointTransformsForUnactiveForceObjects[18] = _systemPointRings[6].Points[0].transform;
        _positionPointTransformsForUnactiveForceObjects[19] = _systemPointRings[7].Points[0].transform;
        _positionPointTransformsForUnactiveForceObjects[20] = _systemPointRings[8].Points[1].transform;
        _positionPointTransformsForUnactiveForceObjects[21] = _systemPointRings[6].Points[1].transform;
        _positionPointTransformsForUnactiveForceObjects[22] = _systemPointRings[7].Points[1].transform;
        _positionPointTransformsForUnactiveForceObjects[23] = _systemPointRings[8].Points[2].transform;
        _positionPointTransformsForUnactiveForceObjects[24] = _systemPointRings[6].Points[2].transform;
        _positionPointTransformsForUnactiveForceObjects[25] = _systemPointRings[7].Points[2].transform;
        _positionPointTransformsForUnactiveForceObjects[26] = _systemPointRings[8].Points[1].transform;

        _newRotatingPointSystem.SystemPointRings = _systemPointRings;
        _newRotatingPointSystem.PositionPointTransformsForActiveForceObjects = _positionPointTransformsForActiveForceObjects;
        _newRotatingPointSystem.PositionPointTransformsForUnactiveFoceObjects = _positionPointTransformsForUnactiveForceObjects;
        _newRotatingPointSystem.TableOfAvaiablePointPositionsTransformsForActiveForceObjects = new Dictionary<int, bool>(18);
        _newRotatingPointSystem.TableOfAvaiablePointPositionsTransformsForUnactiveFoceObjects = new Dictionary<int, bool>(18);
        _newRotatingPointSystem.SystemType = RotatingPointSystemType.PlayerFollowSystem;

        return _newRotatingPointSystem;
    }
    public void SpawnOrbitatingSystem(int _layerCount, Transform _transformTheSystemIsAttachedTo)
    {


    }

    public void SpawnImploderRingSetup(int _ringsCount)
    {

    }
}
