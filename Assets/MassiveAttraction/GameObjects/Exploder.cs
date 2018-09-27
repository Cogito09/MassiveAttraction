using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ExploderState
{
    UnactiveFollowingPlayer,
    ActiveFollowingPlayer,
    PreformingFireKickoff,
    PursuitingTarget,
    Exploding,
    Recreating,
}
public class Exploder : ForceObject , IAnimatable , IInteractableWithEnemy
{
    public ExploderState State;
    public InteractionType myInteractionType;
    public AnimationHandler AnimationHandler;
    public bool MoveBackToPlayersArsenal { get; set; }
    public bool MoveToInteractingWithEnemiesList { get; set; }
    public bool MoveOutFromInteractingWithEnemiesList { get; set; }
    public bool isReadyToDestroyImplodingMinionsOnEnemy { get; set; }


    public float Damage = 120;
    public float explosionRange = 1f;
    private Vector2 target;
    private Transform playerTransform;
    public AnimationCurve DistanceForceModifier; // ShortetDistance  = bigger multiplier;
    public AnimationCurve DragToDistanceFactor;  // Shorter Distance = bigger drag;
    public float moveForceModifier = 5f;


    public float InteractionDistance = 2f;
    public float ExplosionDuration = 0.5f;
    public float RecratingDuration = 2f;
    public float RestoreToActiveDuration = 4f;

    
    public void PreformBehaviour()
    {
             if(State == ExploderState.PursuitingTarget)        { PursuitTarget();     }
        else if(State == ExploderState.Exploding)               { PreformExplosion();  }
        else if(State == ExploderState.Recreating)              { PreformRecreation(); }   
        else if(State == ExploderState.UnactiveFollowingPlayer) { PreformFollowMode(); }
        else if(State == ExploderState.ActiveFollowingPlayer)   { PreformFollowMode(); }
    }
    //Initialize BehaviourFunction
    public void ActivatePursuit(Vector2 _target)
    {
        State = ExploderState.PursuitingTarget;
        target = _target;
    }
    //PreformBEhaviours Function
    public void PreformFollowMode()
    {
        Vector3 moveVector = SimulationInstance.Player.transform.position - transform.position;
        transform.position += moveVector * Time.deltaTime;
    }

    public void PursuitTarget()
    {
        float distance;
        Vector2 moveVector = new Vector2();
        moveVector.x = target.x - transform.position.x;
        moveVector.y = target.y - transform.position.y;

        distance = Vector2.Distance(target, transform.position);
        moveVector.x *= DistanceForceModifier.Evaluate(distance);
        moveVector.y *= DistanceForceModifier.Evaluate(distance);

        rb.drag = DragToDistanceFactor.Evaluate(distance);
        rb.AddForce(moveVector * moveForceModifier);

        if(distance < 0.5f)
        {
            ToggleToExplosionState();
        }
    }
    private void PreformExplosion()
    {
        //AnimationHandlerPreformAnimation Explode
    }
    private void PreformRecreation()
    {
        //AnimationHandlerPreformAnimation Recreate
    }
    //ToggleFunctions
    private void ToggleToExplosionState()
    {
        State = ExploderState.Exploding;
        MoveToInteractingWithEnemiesList = true;
        TimeingManager.SchoudleDelayedFunctionTrigger(ExplosionDuration, ToggleToRecrationState);
    }
    private void ToggleToRecrationState()
    {
        State = ExploderState.Recreating;
        MoveOutFromInteractingWithEnemiesList = true;
        TimeingManager.SchoudleDelayedFunctionTrigger(RecratingDuration, ToggleToUnactiveFollowingPlayer);
    }
    private void ToggleToUnactiveFollowingPlayer()
    {
        State = ExploderState.UnactiveFollowingPlayer;
        TimeingManager.SchoudleDelayedFunctionTrigger(RestoreToActiveDuration, ToggleToActiveFollowingPlayer);
    }
    private void ToggleToActiveFollowingPlayer()
    {
        State = ExploderState.ActiveFollowingPlayer;
        MoveBackToPlayersArsenal = true;
    }

    //IInteractableWithEnemyFunctions
    public IInteractableWithEnemy GetReferenceToMainObject()
    {
        return this;
    }
    public InteractionType GetInteractionType()
    {
        return myInteractionType;
    }
    public Vector2 GetPosition()
    {
        return transform.position;
    }
    public ForceObject GetReferenceToObject()
    {
        return this as Exploder;
    }

    public float GetInteractionDistance()
    {
        return InteractionDistance;
    }

    public override void Reset()
    {
        rb.drag = 25;
        State = ExploderState.ActiveFollowingPlayer;
    }
    public override void Setup()
    {
        base.Setup();
        State = new ExploderState();
        State = ExploderState.ActiveFollowingPlayer;
        myInteractionType = new InteractionType();
        myInteractionType = InteractionType.Explosion;
    }

    public float GetDamage()
    {
        return Damage;
    }

    public void ToggleImplodingEnemyAttack(IEnemyBase _attackedObject)
    {
        Debug.Log("I am ExploderImNotDoinmg ImplodingEnemyAttack");
    }
    public Transform GetTransform()
    {
        return transform;
    }
}
