using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : PoolableObject ,IAvaiableForEnemySpawn , IEnemyBase
{


    public static int poolKey;
    public float HealthPoints;
    public float BomberSpawnRate;
    public float MeteorSpawnRate;

    public GameObject targetObject { get; set; }
    float hp { get; set; }
    public Rigidbody2D rb { get; set; } 

    public float objectRadius { get; set; }
    public bool toBeRemovedFromSimulation { get; set; }




    public bool CanBehHitWithImploder { get; set; }
    public bool isAvaiableForInteraction { get; set; }
    public IInteractableWithEnemy interactionTarget { get; set; }


    public OrbitatingSpawnKickTargetPoint OrbitatingSpawnKickTargetPoint;
    public float OrbiatingSpawnKickPointSpeed = 2f;




    public void StartCoreAction()
    {
        
        Debug.Log("CoreActionLaunched");
        Debug.Log("Core Hp = " + HealthPoints);
        TimeingManager.SchoudleDelayedFunctionTrigger(BomberSpawnRate, SpawnBomberACtion);
        TimeingManager.SchoudleDelayedFunctionTrigger(MeteorSpawnRate, SpawnMeteorAction);
        isAvaiableForInteraction = true;
        CanBehHitWithImploder = true;
   
    }



public void SpawnMeteorAction()
    {
        SimulationInstance.GameplayController.GameplaySpawnManager.SpawnMeteorOutOfObject(this);
        TimeingManager.SchoudleDelayedFunctionTrigger(MeteorSpawnRate, SpawnMeteorAction);
    }


    public void SpawnBomberACtion()
    {
        SimulationInstance.GameplayController.GameplaySpawnManager.SpawnBomberOutOfObject(this);
        TimeingManager.SchoudleDelayedFunctionTrigger(BomberSpawnRate, SpawnBomberACtion);
    }



    public void SetupParameters(CoreParameters _coreParameters)
    {
        Setup();
        HealthPoints = _coreParameters.Hp;
        BomberSpawnRate = _coreParameters.BomberSpawnRate;
        MeteorSpawnRate = _coreParameters.MeteorSpawnRate;
        objectRadius = 5f;
    }
    public override void Setup()
    {
        OrbitatingSpawnKickTargetPoint = ObjectSpawner.SpawnOrbitatingSpawnKickTargetPoint(this.transform.position);
        OrbitatingSpawnKickTargetPoint.transform.SetParent(this.transform);


    }
    public void LaunchInteraction(IInteractableWithEnemy _targetObject)
    {
        interactionTarget = _targetObject.GetReferenceToMainObject();
        var _interactionType = _targetObject.GetInteractionType();
        if (_interactionType == InteractionType.Explosion)
        {
            //AnimateShake
            SubtractHp(interactionTarget.GetDamage());
        }
    }

    public void PreformBehaviour()
    {
        OrbitatingSpawnKickTargetPoint.Rotate(OrbiatingSpawnKickPointSpeed);
    }


    public void SetTargetObjectOfInteraction(GameObject targetObject)
    {
        
    }

    public Vector3 GetPosition()
    {
        return this.transform.position;
    }
    public Transform GetTransform()
    {
        return transform;
    }
    public Vector2 GetSpawnKickTargetPosition()
    {
         return OrbitatingSpawnKickTargetPoint.PositionPoint.transform.position;
    }

    public void SubtractHp(float _damage)
    {
        HealthPoints = HealthPoints - _damage;
        CheckIfDestroyed();
        Debug.Log("CoreGotHi!");
    }
    public void CheckIfDestroyed()
    {
        if (HealthPoints <= 0)
        {
            toBeRemovedFromSimulation = true;
            this.gameObject.SetActive(false);
        }
    }
 

    public PoolableObject GetMainObject()
    {
        return this;
    }


    public override void Reset()
    {

    }



    public override int GetPoolKey()
    {
        return poolKey;
    }
    public override void SetPoolKey(int key)
    {
        poolKey = key;
    }

}
