using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitationWheel : MonoBehaviour
{
    private PositionPoint[] positionPoints;
    private Vector3 rotation;



    public PositionPoint FindNearestPoint(Vector2 _positionOfAskingObject)
    {
        float closestDistance = Vector2.Distance(positionPoints[0].transform.position,_positionOfAskingObject);
        int indexOfClosestPoint = 0;
        for(int i = 1; i <positionPoints.Length; i++)
        {
            float distance = Vector2.Distance(_positionOfAskingObject, positionPoints[i].transform.position);
            if(distance< closestDistance)
            {
                distance = closestDistance;
                indexOfClosestPoint = i;
            }
        }
        return positionPoints[indexOfClosestPoint];
    }

    public void Rotate(float rotationSpeedFactior)
    {
        rotation.z += rotationSpeedFactior;
        transform.rotation = Quaternion.Euler(rotation);
    }

    public void CreateSpawnPositions(float _size)
    {
        Vector2[] positionsParameters = CreateSpawnPositionsParameters(_size);
        positionPoints = new PositionPoint[6];
        for (int i = 0; i < positionPoints.Length; i++)
        {
            positionPoints[i] = MainController.Instance.MassiveAttraction.InstantiateModule.InstantiateObjectWithScript<PositionPoint>(MainController.Instance.MassiveAttraction.PrefabCollection.PositionPoint);

            positionPoints[i].transform.position = positionsParameters[i];
            positionPoints[i].transform.SetParent(gameObject.transform);
        }
    }

    private Vector2[] CreateSpawnPositionsParameters(float _size)
    {
        Vector2[] spawnPositionsParameters = new Vector2[6];
        spawnPositionsParameters[0] = new Vector2(6     * _size, 0  * _size);
        spawnPositionsParameters[1] = new Vector2(2.7f  * _size, 5  * _size);
        spawnPositionsParameters[2] = new Vector2(-2.7f * _size, 5  * _size);
        spawnPositionsParameters[3] = new Vector2(-6    * _size, 0  * _size);
        spawnPositionsParameters[4] = new Vector2(-2.7f * _size, -5 * _size);
        spawnPositionsParameters[5] = new Vector2(2.7f  * _size, -5 * _size);
        return spawnPositionsParameters;
    }

    public void Setup()
    {
        CreateSpawnPositions(2);
        rotation = new Vector3(0, 0, 0);
    }
}
