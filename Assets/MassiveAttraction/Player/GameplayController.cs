using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : NonMonoBehaviourBaseModuleAccess
{
    public GameplayPlayerMovementManager GameplayPlayerMovementManager;
    public GameplaySpawnManager GameplaySpawnManager;
    public int currentStageIndex;
    public Stage[] Stages;
    
    public void PreformGameplay()
    {
        GameplaySpawnManager.PreformSpawnerAction();
        GameplayPlayerMovementManager.PreformMovementManagerAction();
    }
    public void StartStage()
    {
        TimeingManager.SchoudleDelayedFunctionTrigger(Stages[currentStageIndex].DurationOfStage, ChangeStage);
        GameplaySpawnManager.SetSpawnParameters(Stages[currentStageIndex].GetStageSpawnParameters());
        GameplaySpawnManager.LaunchSpawning();
    }
    public void ChangeStage()
    {
        currentStageIndex++;
        if(currentStageIndex < Stages.Length)
        {
            Debug.Log("Next Stage Begun!");
            TimeingManager.SchoudleDelayedFunctionTrigger(Stages[currentStageIndex].DurationOfStage, ChangeStage);
            GameplaySpawnManager.SetSpawnParameters(Stages[currentStageIndex].GetStageSpawnParameters());
            if(Stages[currentStageIndex].isItCoreStage == true)
            {
                LaunchCoreStagePhase();
            }
        }
    }  
    public void LaunchCoreStagePhase()
    {
        Vector3 spawnPosition = GameplaySpawnManager.SpawnPointWheel.GetRandomSpawnPosition();
        GameplaySpawnManager.SpawnCore(spawnPosition);
        SimulationInstance.Core.StartCoreAction();
        SimulationInstance.enemyObjects.Add(SimulationInstance.Core);

        GameplayPlayerMovementManager.FollowPointWheel.transform.SetParent(SimulationInstance.Core.transform);
        GameplayPlayerMovementManager.FollowPointWheel.transform.position = SimulationInstance.Core.transform.position;
    }
    public void SetupLevel(Level _level)
    {
        Stages = _level.Stages;
        int finalStageIndex = Stages.Length -1;
        Stages[finalStageIndex].Core = ObjectSpawner.SpawnCore(Stages[finalStageIndex].CoreParameters);
        Stages[finalStageIndex].Core.gameObject.SetActive(false);
        currentStageIndex = 0;
    }
    public GameplayController()
    {
        GameplaySpawnManager          = new GameplaySpawnManager();
        GameplayPlayerMovementManager = new GameplayPlayerMovementManager();
    }
}
