using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MeteorState
{
    PreformingStartupKick,
    MovingTowardsPlayer,
    MovingTowardsTargetAsImplosionCatched,
    DestructingAerial,
    DestructingTarget,
    FreeState
}
public class Meteor : PoolableObject, IEnemyBase, IFollowingEnemy
{
    public bool CanBehHitWithImploder { get; set; }


    public float objectRadius { get; set; }
    public float damage;
    public static int poolKey;
    public MeteorState State;
    public bool isStartupKickDone;
    public bool isAvaiableForInteraction { get; set; }
    public bool alredyIteractingWithImploder;
    public bool toBeRemovedFromSimulation { get; set; }
    public float healthPoints { get; set; }
    public float playerColissionDistance = 1f;
    public Imploder theObjectThatMeteorIsCathedBy;
    public IInteractableWithEnemy interactionTarget { get; set; }
    public Transform playerTransform { get; set; }
    public Transform targetTransform { get; set; }
    public Vector3 moveVector { get; set; }
    public AnimationCurve MoveTowardsPlanetDistanceCurve;
    public AnimationCurve ImplodingImpactForceToDistanceMultiplier;
    public AnimationCurve ImplodingImpactDragToDistanceModifier;
    public float ImplodingImpactGeneralForceMultiplier = 4f;
    public AnimationCurve MoveTowardsMassFactorCurve;
    public AnimationCurve FollowWhenImploderAffected;
    public float regulatDrag = 1f;
    public float regularMass = 2f;
    public Rigidbody2D rb { get; set; }

    public void PreformBehaviour()
    {
        if (State == MeteorState.FreeState) {; }
        else if (State == MeteorState.PreformingStartupKick) { PreformStartupKick(); }
        else if (State == MeteorState.MovingTowardsPlayer) { PreformMoveTowardsPlayer(); }
        else if (State == MeteorState.MovingTowardsTargetAsImplosionCatched) { PreformMovingTowardsTargetAsImplosionCatched(); }
        else if (State == MeteorState.DestructingAerial) { PrefromDestrctingAerial(); }
        else if (State == MeteorState.DestructingTarget) { PreformDestructingTarget(); }
    }
    public void ToggleToMoveTowardsPlayer()
    {
        State = MeteorState.MovingTowardsPlayer;
        rb.drag = regulatDrag;
        rb.mass = regularMass;
    }
    public void ToggleToMoveTowardsTargetAsImploded()
    {
        State = MeteorState.MovingTowardsTargetAsImplosionCatched;
        
    }
    public void ToggleToAerialDestructionState()
    {
        isAvaiableForInteraction = false;
        State = MeteorState.DestructingAerial;
        TimeingManager.SchoudleDelayedFunctionTrigger(1f, RemoveFromSimulation);
    }
    public void ToggleToDestructingTargetState(Vector3 _targetPosition)
    {
        rb.drag = 1000; // stopingTheMovement
        State = MeteorState.DestructingTarget;
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
            PushOutFromExplosionPosition(interactionTarget.GetPosition(), interactionTarget.GetDamage());
            SubtractHp(interactionTarget.GetDamage());
        }
        else if (_interactionType == InteractionType.Implosion && alredyIteractingWithImploder == false)
        {
            
            alredyIteractingWithImploder = true;
            theObjectThatMeteorIsCathedBy = interactionTarget.GetReferenceToObject() as Imploder;
            ToggleToMoveTowardsTargetAsImploded();
        }
    }

    public void PreformStartupKick()
    {
        //Animate
    }
    public void PreformMoveTowardsPlayer()
    {
        float distance = Vector2.Distance(playerTransform.position, transform.position);
        if (distance < playerColissionDistance)
        {
            ToggleToDestructingTargetState(playerTransform.position);
        }
        else
        {
            moveVector = playerTransform.position - transform.position;
            float multiplier = MoveTowardsPlanetDistanceCurve.Evaluate(distance);
            rb.AddForce(moveVector * multiplier);
            //Animate
        }

    }
    public void PreformMovingTowardsTargetAsImplosionCatched()
    {
        if(theObjectThatMeteorIsCathedBy.State == ImploderState.ImplodiningObjectAttacking)
        {
            float distance = Vector3.Distance(theObjectThatMeteorIsCathedBy.TheAttackedObject.GetPosition(),transform.position);
            moveVector = theObjectThatMeteorIsCathedBy.TheAttackedObject.GetPosition() - transform.position;
            transform.position += moveVector * Time.deltaTime * 2f;
            if(distance <= theObjectThatMeteorIsCathedBy.TheAttackedObject.objectRadius)
            {
                theObjectThatMeteorIsCathedBy.TheAttackedObject.SubtractHp(damage);
                ToggleToDestructingTargetState(theObjectThatMeteorIsCathedBy.TheAttackedObject.GetPosition());
            }
        }




        else if(theObjectThatMeteorIsCathedBy.State == ImploderState.PreformingImplosionDrift)
        {
            float distance = Vector2.Distance(theObjectThatMeteorIsCathedBy.transform.position, transform.position);
            moveVector = theObjectThatMeteorIsCathedBy.transform.position - transform.position;
            transform.position += moveVector * FollowWhenImploderAffected.Evaluate(distance);
            // Animate 
        }
        else if(theObjectThatMeteorIsCathedBy.State == ImploderState.PreformingImplodingImpact)
        {
            float distance = Vector2.Distance(theObjectThatMeteorIsCathedBy.transform.position, transform.position);
            float multiplier = ImplodingImpactForceToDistanceMultiplier.Evaluate(distance);
            moveVector = theObjectThatMeteorIsCathedBy.transform.position - transform.position;
            rb.drag = ImplodingImpactDragToDistanceModifier.Evaluate(distance);
            rb.AddForce(moveVector * multiplier * ImplodingImpactGeneralForceMultiplier);
            // Animate 
        }
        else ToggleBackToFollowingPlayerAfterImplodingImpact();
    }

    public void ToggleBackToFollowingPlayerAfterImplodingImpact()
    {
        theObjectThatMeteorIsCathedBy = null;
        isAvaiableForInteraction = true;
        alredyIteractingWithImploder = false;
        ToggleToMoveTowardsPlayer();
    }
    public void PrefromDestrctingAerial()
    {
        //AnimateDestiction
    }
    public void PreformDestructingTarget()
    {
        //AnimateTargettedDestructionAnimation
    } 
  
    public void PushOutFromExplosionPosition(Vector3 _position, float _damage)
    {
        float _pushForce = _damage * 9f;//
        Vector2 forceVector = -1 * (_position - transform.position);
        rb.AddForce(forceVector.normalized * _pushForce);
    }
    public void LaunchStartupKick(Vector3 _kickPosition)
    { 
        Vector3 _kickVector = _kickPosition - transform.position;


        rb.mass = regularMass;

        rb.drag = regulatDrag;
        regulatDrag = Random.Range(0.4f, 1.2f);
        rb.AddForce(_kickVector * Random.Range(400, 550));
        isAvaiableForInteraction = true;
    }
    public void CheckIfDestroyed()
    {
        if(healthPoints<= 0)
        {
            ToggleToAerialDestructionState();
        }
    }
    public void SubtractHp(float _damage)
    {
        healthPoints -= _damage;
        CheckIfDestroyed(); 
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void RemoveFromSimulation()
    {
        toBeRemovedFromSimulation = true;
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
        objectRadius = 0.3f;
        CanBehHitWithImploder = false;
        healthPoints = 100;
        damage = 60;
        State = new MeteorState();
    }
    public override void Reset()
    {
        healthPoints = 100;
        damage = 60;
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

    public Transform GetTransform()
    {
        return transform;
    }
}
