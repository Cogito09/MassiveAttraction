using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayPlayerMovementManager : NonMonoBehaviourBaseModuleAccess
{
    public bool CoreAttackMode = false;

    public SpawnPointWheel FollowPointWheel;
    public bool rotationDirection;
    public float rotationSpeed = 0.2f;

    public bool movementParametersFlow = true;

    public void PreformMovementManagerAction()
    {
        if(rotationDirection == false)
        {
            FollowPointWheel.Rotate(-rotationSpeed);
        }
        else
        {
            FollowPointWheel.Rotate(rotationSpeed);
        }  
    }

    public void TriggerRotationChangeCycle()
    {
        if(movementParametersFlow == true)
        {
            TimeingManager.SchoudleDelayedFunctionTrigger(Random.Range(3, 25), SwapRotationDirection);
        }
    }
    public void TriggerRotationSpeedChangeCycle()
    {
        if (movementParametersFlow == true)
        {
            TimeingManager.SchoudleDelayedFunctionTrigger(Random.Range(5, 15), ChangeRotationSpeed);
        }
    }
    public void ChangeRotationSpeed()
    {
        rotationSpeed = Random.Range(0.1f, 1f);
        TriggerRotationSpeedChangeCycle();
    }
    public void SwapRotationDirection()
    {
        if (rotationDirection == false) { rotationDirection = true; }
        else rotationDirection = false;

        TriggerRotationChangeCycle();
    }
    public Transform GetFollowPointTransform()
    {
        return FollowPointWheel.GetRandomSpawnPointTransform();
    }
    public GameplayPlayerMovementManager()
    {
        FollowPointWheel = ObjectSpawner.SpawnWheelSpawnerObject(SimulationInstance.Player.transform.position);
        FollowPointWheel.transform.SetParent(SimulationInstance.Player.transform);
        FollowPointWheel.CreateSpawnPositions();
        SimulationInstance.Player.PlayerMovementManager.ChangeFollowPoint(FollowPointWheel.GetRandomSpawnPointTransform());
        movementParametersFlow = true;
        TriggerRotationChangeCycle();
        TriggerRotationChangeCycle();
    }

}
