using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitatingSpawnKickTargetPoint : MonoBehaviour
{
    public PositionPoint PositionPoint;
    public Vector3 rotation;



    public void Rotate(float _rotationSpeedFactior)
    {
        rotation.z += _rotationSpeedFactior;
        transform.rotation = Quaternion.Euler(rotation);
    }

    public void CreateSpawnPosition()
    {
        PositionPoint = MainController.Instance.MassiveAttraction.InstantiateModule.InstantiateObjectWithScript<PositionPoint>(MainController.Instance.MassiveAttraction.PrefabCollection.PositionPoint);
        PositionPoint.transform.position = transform.position + new Vector3(0, 1,0);
        PositionPoint.transform.SetParent(this.transform);
    }

    public void Setup()
    {
        CreateSpawnPosition();
        rotation = new Vector3(0, 0, 0);
    }
}
