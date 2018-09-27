using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :  MonoBehaviourBaseModuleAccessObject , IDefensable
{
    public PlayerMovementManager PlayerMovementManager;
    public ForceObjectsManager ForceObjectsManager;
    public OrbitationWheel OrbitationWheel;
    public float OrbitationWheelSpeed = 0.2f;


    public void LaunchAction(InputAnswer _inputAnswer)
    {
        if (_inputAnswer.isSingleClicked)
        {
            float _clickDistanceToPlayerPosition = Vector2.Distance(_inputAnswer.singleClickPosition, transform.position);
            if (_clickDistanceToPlayerPosition < 0.5f) { ForceObjectsManager.LaunchDefender(this); }
            else ForceObjectsManager.LaunchExploder(_inputAnswer.singleClickPosition);
        }
        else if (_inputAnswer.isPathRecorded)
        {
            ForceObjectsManager.LaunchImploder(_inputAnswer.path);
        }
    }
    public void PreformPlayerBehaviour()
    {
        PlayerMovementManager.PreformMovementBehaviour();
        ForceObjectsManager.PreformBehaviours();
        OrbitationWheel.Rotate(OrbitationWheelSpeed * Time.deltaTime);
    }
    public void Setup(ForceObjectStartSpawnParameters _forceObjectStartSpawnParameters)
    {
        PlayerMovementManager = new PlayerMovementManager(this.transform);
        ForceObjectsManager = new ForceObjectsManager(this,_forceObjectStartSpawnParameters);
        OrbitationWheel = ObjectSpawner.SpawnOrbitationWheel(this.transform.position);
        OrbitationWheel.transform.SetParent(transform);
    }

    public Transform GetTransform()
    {
        return transform;
    }

}
