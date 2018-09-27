using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BomberState
{
    PreformingStartupKick,
    MovingTowardsPlayer,
    Bombing,
    DestructingAerial,
    DestructingTarget,
    FreeState
}
public class Bomber : PoolableObject ,IEnemyBase, IFollowingEnemy , IAvaiableForEnemySpawn
{
    public bool CanBehHitWithImploder { get; set; }

    public float objectRadius { get; set; }
    public static int poolKey;
    public BomberState State;
    public bool isLaunchingMeteorsAvaiable;
    public bool isAvaiableForInteraction { get; set; }
    public bool toBeRemovedFromSimulation { get; set; }
    public float healthPoints { get; set; }
    public float playerBombingDistance = 8f;
    public Imploder theObjectThatMeteorIsCathedBy;
    public IInteractableWithEnemy interactionTarget { get; set; }
    public Transform playerTransform { get; set; }
    public PositionPoint moveTargetPositionPoint { get; set; }
    public Vector3 moveVector { get; set; }

    public AnimationCurve MoveTowardsPlanetDistanceCurve;
    public AnimationCurve MoveTowardsTargetDistanceCurve;
    public Rigidbody2D rb { get; set; }

    public OrbitatingSpawnKickTargetPoint OrbitatingSpawnKickTargetPoint;
    public float OrbiatingSpawnKickPointSpeed = 2f;

    public OrbitationWheel OrbitationWheelOfTargetedObject;
    public float MeteorSpawnRatio = 2f;
    public float QuitBombingDistanceDifference = 3f;

    public void PreformBehaviour()
    {
        if      (State == BomberState.FreeState) {; }
        else if (State == BomberState.PreformingStartupKick) { PreformStartupKick(); }
        else if (State == BomberState.MovingTowardsPlayer)   { PreformMoveTowardsPlayer(); }
        else if (State == BomberState.Bombing)               { PrefromBombing(); }
        else if (State == BomberState.DestructingAerial)     { PrefromDestrctingAerial(); }
        else if (State == BomberState.DestructingTarget)     { PreformDestructingTarget(); }


        OrbitatingSpawnKickTargetPoint.Rotate(OrbiatingSpawnKickPointSpeed);


    }
    public void ToggleToMoveTowardsPlayer()
    {
        State = BomberState.MovingTowardsPlayer;
        isLaunchingMeteorsAvaiable = false;
    }
    public void ToggleToBombing()
    {
        State = BomberState.Bombing;
        moveTargetPositionPoint = OrbitationWheelOfTargetedObject.FindNearestPoint(transform.position);
        isLaunchingMeteorsAvaiable = true;
        LaunchingMeteor();
    }
    public void ToggleToAerialDestructionState()
    {
        isLaunchingMeteorsAvaiable = false;
        isAvaiableForInteraction = false;
        State = BomberState.DestructingAerial;
        TimeingManager.SchoudleDelayedFunctionTrigger(1f, RemoveFromSimulation);
    }
    public void ToggleToDestructingTargetState(Vector3 _targetPosition)
    {
        isLaunchingMeteorsAvaiable = false;
        State = BomberState.DestructingTarget;
        //Calculate Vector of Animation Direction
        // Send Vector To Aniamtionhandler 
        TimeingManager.SchoudleDelayedFunctionTrigger(1f, RemoveFromSimulation);
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

    public void LaunchingMeteor()
    {
        if (isLaunchingMeteorsAvaiable)
        {
            SimulationInstance.GameplayController.GameplaySpawnManager.SpawnMeteorOutOfObject(this);
            TimeingManager.SchoudleDelayedFunctionTrigger(MeteorSpawnRatio, LaunchingMeteor);
        }
        

    }
    public void PreformStartupKick()
    {
        //Animate
    }
    public void PreformMoveTowardsPlayer()
    {
        float distance = Vector2.Distance(playerTransform.position, transform.position);
        if (distance < playerBombingDistance)
        {
            ToggleToBombing();
        }
        moveVector = playerTransform.position - transform.position;
        float multiplier = MoveTowardsPlanetDistanceCurve.Evaluate(distance);
        rb.AddForce(moveVector * multiplier);
        //Animate
    }
    public void PrefromBombing()
    {
        float distance = Vector2.Distance(playerTransform.position, transform.position);
        if (distance > playerBombingDistance + QuitBombingDistanceDifference)
        {
            ToggleToMoveTowardsPlayer();
        }
        moveVector = moveTargetPositionPoint.transform.position - transform.position;
        float multiplier = MoveTowardsPlanetDistanceCurve.Evaluate(distance);
        rb.AddForce(moveVector * multiplier);

        // Animate 
    }
    public void PrefromDestrctingAerial()
    {
        //AnimateDestiction
    }
    public void PreformDestructingTarget()
    {
        //Funtion Not Implemented In Bomber
        //AnimateTargettedDestructionAnimation
    }
    public void PushOutFromExplosionPosition(Vector3 _position, float _damage)
    {
        float _pushForce = _damage * 9f;//
        Vector2 forceVector = -1 * (_position - transform.position);
        rb.AddForce(forceVector.normalized * _pushForce);
    }
    public void LaunchStartupKick(Vector2 _kickVector)
    {
        rb.drag = Random.Range(0.4f, 1.2f);
        rb.AddForce(_kickVector * Random.Range(20, 50));
        isAvaiableForInteraction = true;
    }
    public void CheckIfDestroyed()
    {
        if (healthPoints <= 0)
        {
            ToggleToAerialDestructionState();
        }
    }
    public void SubtractHp(float _damage)
    {
        healthPoints -= _damage;
        CheckIfDestroyed();
    }
    public Transform GetTransform()
    {
        return transform;
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public Vector2 GetSpawnKickTargetPosition()
    {
        return OrbitatingSpawnKickTargetPoint.PositionPoint.transform.position;
        //zrob orbitujacy obiekt
    }
    public void OrbitateKickTagetPoint()
    {
        // rotaation =
        // transform.EuelerAngles.Raotaion(rotation)
    }















    public void RemoveFromSimulation()
    {
        toBeRemovedFromSimulation = true;
    }
    public void SetupPlayersOrbitationWheel(OrbitationWheel _orbitationWheel)
    {
        OrbitationWheelOfTargetedObject = _orbitationWheel;
    }
    public void SetupPlayerTransform(Transform _target)
    {
        playerTransform = _target.transform;
    }
    public PoolableObject GetMainObject()
    {
        return this;
    }
    public override void Setup()
    {
        objectRadius = 1.5f;
        CanBehHitWithImploder = true;
        healthPoints = 500;
        State = new BomberState();
        OrbitatingSpawnKickTargetPoint = ObjectSpawner.SpawnOrbitatingSpawnKickTargetPoint(this.transform.position);
        OrbitatingSpawnKickTargetPoint.transform.SetParent(this.transform);
    }
    public override void Reset()
    {
        healthPoints = 500;
    }
    public override int GetPoolKey()
    {
        return poolKey;
    }
    public override void SetPoolKey(int key)
    {
        poolKey = key;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
