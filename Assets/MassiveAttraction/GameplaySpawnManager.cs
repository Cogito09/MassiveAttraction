using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySpawnManager : NonMonoBehaviourBaseModuleAccess
{
    public SpawnPointWheel SpawnPointWheel;
    public Core CoreToBeSpawned;

    private bool spawningEnabled;
    public float MeteorSpawnDelayDuration;
    public float BomberSpawnDelayDuration;

    public void PreformSpawnerAction()
    {
        SpawnPointWheel.Rotate(1f);
    }
    public void LaunchSpawning()
    {
        spawningEnabled = true;
        TimeingManager.SchoudleDelayedFunctionTrigger(MeteorSpawnDelayDuration, SpawnMeteorFromGlobalSpawnWheel);
        TimeingManager.SchoudleDelayedFunctionTrigger(BomberSpawnDelayDuration, SpawnBomber);
    }
    public void SuspendSpawning()
    {
        spawningEnabled = false;
    }
    public void SpawnMeteorFromGlobalSpawnWheel()
    {
        if (spawningEnabled)
        {
            Meteor newMeteor = ObjectSpawner.SpawnMeteorRandomlyOnSpawnWheel(SimulationInstance.GameplayController.GameplaySpawnManager.SpawnPointWheel);
            SimulationInstance.enemyObjects.Add(newMeteor);
            newMeteor.SetupPlayerTransform(SimulationInstance.Player.transform);
            newMeteor.LaunchStartupKick(SpawnPointWheel.GetKickTagetPositionForJustSpawnedMeteor());
            newMeteor.ToggleToMoveTowardsPlayer();
            
            TimeingManager.SchoudleDelayedFunctionTrigger(MeteorSpawnDelayDuration, SpawnMeteorFromGlobalSpawnWheel);
        }
    }
    public void SpawnMeteorOutOfObject(IAvaiableForEnemySpawn _object)
    {
        Meteor newMeteor = ObjectSpawner.SpawnMeteor(_object.GetPosition());
        SimulationInstance.enemyObjects.Add(newMeteor);
        newMeteor.SetupPlayerTransform(SimulationInstance.Player.transform);
        newMeteor.LaunchStartupKick(_object.GetSpawnKickTargetPosition());
        newMeteor.ToggleToMoveTowardsPlayer();
    }

    public void SpawnBomberOutOfObject(IAvaiableForEnemySpawn _object)
    {
        Bomber newBomber = ObjectSpawner.SpawnBomber(_object.GetPosition());
        SimulationInstance.enemyObjects.Add(newBomber);
        newBomber.SetupPlayerTransform(SimulationInstance.Player.transform);
        newBomber.SetupPlayersOrbitationWheel(SimulationInstance.Player.OrbitationWheel);
        newBomber.LaunchStartupKick(_object.GetSpawnKickTargetPosition());
        newBomber.ToggleToMoveTowardsPlayer();
    }

    private void SpawnBomber()
    {
        if (spawningEnabled)
        {
            Bomber newBomber = ObjectSpawner.SpawnBomberRandomlyOnSpawnWheel(SpawnPointWheel);
            SimulationInstance.enemyObjects.Add(newBomber);
            newBomber.SetupPlayerTransform(SimulationInstance.Player.transform);
            newBomber.SetupPlayersOrbitationWheel(SimulationInstance.Player.OrbitationWheel);
            newBomber.LaunchStartupKick(SpawnPointWheel.GetKickTagetPositionForJustSpawnedMeteor());
            newBomber.ToggleToMoveTowardsPlayer();

            TimeingManager.SchoudleDelayedFunctionTrigger(BomberSpawnDelayDuration, SpawnBomber);
        }
    }
    public void SpawnCore(Vector3 _spawnPosition)
    {
        CoreToBeSpawned.gameObject.SetActive(true);
        CoreToBeSpawned.transform.position = _spawnPosition;
        SimulationInstance.Core = CoreToBeSpawned;
    }
    public void SetSpawnParameters(StageSpawnParameters _stageSpawnParameters)
    {
        MeteorSpawnDelayDuration = _stageSpawnParameters.MeteorsSpawnSpeed;
        BomberSpawnDelayDuration = _stageSpawnParameters.BombersSpawnSpeed;
        if(_stageSpawnParameters.Core != null)
        {
            CoreToBeSpawned = _stageSpawnParameters.Core;
        }
    }
    public GameplaySpawnManager()
    {
        SpawnPointWheel = ObjectSpawner.SpawnWheelSpawnerObject(SimulationInstance.Player.transform.position);
        SpawnPointWheel.CreateSpawnPointWheel();
        SpawnPointWheel.transform.SetParent(SimulationInstance.Player.transform);
    }
}
