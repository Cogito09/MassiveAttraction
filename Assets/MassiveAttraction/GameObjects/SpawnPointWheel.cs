using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointWheel : MonoBehaviour
{
    private PositionPoint[] spawnPoints;
    private Vector3 rotation;

    public Vector2 kickPositionForJustSpawnedMeteor;

	public Vector2 GetRandomSpawnPosition()
    {
        int _randomIndex = Random.Range(0, 5);
        SetKicPostionForJustSpawnedMeteor(_randomIndex);
        return spawnPoints[_randomIndex].transform.position;
    }

    public Transform GetRandomSpawnPointTransform()
    {
        //null exception ? 
        return spawnPoints[Random.Range(0, 5)].transform;
    }
    private void SetKicPostionForJustSpawnedMeteor(int _indexOfPostionThatMeteorJustSpawnedAt)
    {
        int kickSpawnPositionIndex = _indexOfPostionThatMeteorJustSpawnedAt;
        int randomizeDirection = Random.Range(-1, 1);
        if (randomizeDirection > 0) { kickSpawnPositionIndex++; }
        else { kickSpawnPositionIndex--; }
        if (kickSpawnPositionIndex > 5) { kickSpawnPositionIndex = 0; }
        else if (kickSpawnPositionIndex < 0) { kickSpawnPositionIndex = 5; }

        kickPositionForJustSpawnedMeteor = spawnPoints[kickSpawnPositionIndex].transform.position;
    }
    public Vector2 GetKickTagetPositionForJustSpawnedMeteor()
    {
        return kickPositionForJustSpawnedMeteor;
    }

    public void Rotate(float rotationSpeedFactior)
    {
        rotation.z += rotationSpeedFactior;
        transform.rotation = Quaternion.Euler(rotation);
    }


    public void CreateSpawnPositions()
    {
        Vector2[] positionsParameters = CreateSpawnPositionsParameters();
        spawnPoints = new PositionPoint[6];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = MainController.Instance.MassiveAttraction.InstantiateModule.InstantiateObjectWithScript<PositionPoint>(MainController.Instance.MassiveAttraction.PrefabCollection.PositionPoint);

            spawnPoints[i].transform.position = positionsParameters[i];
            spawnPoints[i].transform.SetParent(gameObject.transform);
        }
        
    }

    private Vector2[] CreateSpawnPositionsParameters()
    {
        Vector2[] spawnPositionsParameters = new Vector2[6];
        spawnPositionsParameters[0] = new Vector2(36, 0);
        spawnPositionsParameters[1] = new Vector2(16.2f, 30);
        spawnPositionsParameters[2] = new Vector2(-16.2f, 30);
        spawnPositionsParameters[3] = new Vector2(-36, 0);
        spawnPositionsParameters[4] = new Vector2(-16.2f, -30);
        spawnPositionsParameters[5] = new Vector2(16.2f, -30);
        return spawnPositionsParameters;
    }

    public void CreateSpawnPointWheel()
    {
        CreateSpawnPositions();
        rotation = new Vector3(0, 0, 0);
    } 

}
