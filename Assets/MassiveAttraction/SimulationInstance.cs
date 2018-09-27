using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationInstance : MonoBehaviourBaseModuleAccessObject
{
    public CameraController cameraController;
    public GameplayController GameplayController;

    public Player Player;
    public Core Core;

    public List<IEnemyBase> enemyObjects = new List<IEnemyBase>();
    public List<IInteractableWithEnemy> ListOfObjectsThatInteractiWithEnemiesAtThisFrame = new List<IInteractableWithEnemy>();

    public List<Exploder> launchedExploders;
    public List<Imploder> launchedImploders;
    public List<Defender> launchedDefenders;

    public void PreformGameplay()
    {
        MaintainListOfObjectsThatInteractiWithMeteorsAtThisFrame();
        Player.PreformPlayerBehaviour();
        GameplayController.PreformGameplay();
        cameraController.FollowPlayer();
  
        ProcessExploders();
        ProcessImploders(); 
        ProcessDefenders();
        ProcessEnymObjectsInSimulation();
    }
    public void ProcessEnymObjectsInSimulation()
    { 
        for (int i = enemyObjects.Count - 1; i >= 0; i--)
        {
            if (enemyObjects[i].toBeRemovedFromSimulation == true)
            {
                enemyObjects[i].toBeRemovedFromSimulation = false;
                PoolModule.BackObjectToPool(enemyObjects[i].GetMainObject());
                enemyObjects.RemoveAt(i);
            }
            else
            {
                enemyObjects[i].PreformBehaviour();
                if (enemyObjects[i].isAvaiableForInteraction)
                {
                    for (int j = 0; j < ListOfObjectsThatInteractiWithEnemiesAtThisFrame.Count; j++)
                    {
                        float distance = Vector2.Distance(enemyObjects[i].GetPosition(), ListOfObjectsThatInteractiWithEnemiesAtThisFrame[j].GetTransform().position);

                        if(enemyObjects[i].CanBehHitWithImploder && ListOfObjectsThatInteractiWithEnemiesAtThisFrame[j].isReadyToDestroyImplodingMinionsOnEnemy)
                        {
                            if(distance < enemyObjects[i].objectRadius)
                            {
                                ListOfObjectsThatInteractiWithEnemiesAtThisFrame[j].ToggleImplodingEnemyAttack(enemyObjects[i]);
                            }
                        }
                        if (distance < ListOfObjectsThatInteractiWithEnemiesAtThisFrame[j].GetInteractionDistance())
                        {
                            enemyObjects[i].LaunchInteraction(ListOfObjectsThatInteractiWithEnemiesAtThisFrame[j].GetReferenceToMainObject());
                        }
                    }
                }
            }    
        }
    }
    private void ProcessExploders()
    {
        for(int i = launchedExploders.Count - 1; i >= 0; i--)
        {
            launchedExploders[i].PreformBehaviour();
            if (launchedExploders[i].MoveToInteractingWithEnemiesList == true)
            {
                ListOfObjectsThatInteractiWithEnemiesAtThisFrame.Add(launchedExploders[i]);
                launchedExploders[i].MoveToInteractingWithEnemiesList = false;
            }
            if(launchedExploders[i].MoveBackToPlayersArsenal == true)
            {
                launchedExploders[i].MoveBackToPlayersArsenal = false;
                SimulationInstance.Player.ForceObjectsManager.Exploders.Enqueue(launchedExploders[i]);
                launchedExploders.RemoveAt(i);
            }

        }
    }
    private void ProcessImploders()
    {
        for (int i = launchedImploders.Count - 1; i >= 0; i--)
        {
            launchedImploders[i].PreformBehaviour();
            if(launchedImploders[i].MoveToInteractingWithEnemiesList == true)
            {
                Debug.Log("Imploder Putted Into InteractingWithEnemyList");
                ListOfObjectsThatInteractiWithEnemiesAtThisFrame.Add(launchedImploders[i]);
                launchedImploders[i].MoveToInteractingWithEnemiesList = false;
            }
            if (launchedImploders[i].MoveBackToPlayersArsenal == true)
            {
                launchedImploders[i].MoveBackToPlayersArsenal = false;
                SimulationInstance.Player.ForceObjectsManager.Imploders.Enqueue(launchedImploders[i]);
                launchedImploders.RemoveAt(i);
            }

        }
    }
    private void ProcessDefenders()
    {

        for (int i = launchedDefenders.Count - 1; i >= 0; i--)
        {
            launchedDefenders[i].PreformBehaviour();
            if (launchedDefenders[i].MoveToInteractingWithEnemiesList == true)
            {
                ListOfObjectsThatInteractiWithEnemiesAtThisFrame.Add(launchedDefenders[i]);
                launchedDefenders[i].MoveToInteractingWithEnemiesList = false;
            }
            if (launchedDefenders[i].MoveBackToPlayersArsenal == true)
            {
                launchedDefenders[i].MoveBackToPlayersArsenal = false;
                SimulationInstance.Player.ForceObjectsManager.Defenders.Enqueue(launchedDefenders[i]);
                launchedDefenders.RemoveAt(i);
            }

        }
    }

    private void MaintainListOfObjectsThatInteractiWithMeteorsAtThisFrame()
    {
        for(int i = ListOfObjectsThatInteractiWithEnemiesAtThisFrame.Count - 1; i >= 0; i--)
        {
            if(ListOfObjectsThatInteractiWithEnemiesAtThisFrame[i].GetInteractionType() == InteractionType.Explosion)
            {
                ListOfObjectsThatInteractiWithEnemiesAtThisFrame[i].MoveOutFromInteractingWithEnemiesList = false;
                ListOfObjectsThatInteractiWithEnemiesAtThisFrame.RemoveAt(i);
            } 
            else if (ListOfObjectsThatInteractiWithEnemiesAtThisFrame[i].MoveOutFromInteractingWithEnemiesList)
            {
                Debug.Log("Object If not using Exploders This is For SureImploder that got removed from InteractingWithEnemyList");
                ListOfObjectsThatInteractiWithEnemiesAtThisFrame[i].MoveOutFromInteractingWithEnemiesList = false;
                ListOfObjectsThatInteractiWithEnemiesAtThisFrame.RemoveAt(i);
            }
        }
    }

    public void LaunchInstance()
    {
        CreateInstanceModules();
    }
    public void CreateInstanceModules()
    {
        cameraController = ModuleCreator.Create<CameraController>("camController");
        PoolModule.CreatePool(PrefabCollection.Meteor, 1000, "Meteor Pool");
        PoolModule.CreatePool(PrefabCollection.Bomber, 30, "Bomber Pool");

        launchedExploders = new List<Exploder>();
        launchedImploders = new List<Imploder>();
        launchedDefenders = new List<Defender>();
        GameplayController = new GameplayController();
        cameraController.SetupTarget();
    }
    public void LoadLevel(Level _level)
    {
        GameplayController.SetupLevel(_level);

    }

}
