using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DefenderState
{
    UnactiveFollowingPlayer,
    ActiveFollowingPlayer,
    DashingToCenterOfDefendingObject,
    Exloding,
    Recreating,
}

public class Defender : ForceObject, IAnimatable, IInteractableWithEnemy
{
    public DefenderState State;
    public InteractionType myInteractionType;
    public bool MoveBackToPlayersArsenal { get; set; }
    public bool isReadyToDestroyImplodingMinionsOnEnemy { get; set; }
    public bool MoveToInteractingWithEnemiesList { get; set; }
    public bool MoveOutFromInteractingWithEnemiesList { get; set; }


    public Transform Target;
    //BehaviourEstethicsParameters
    public AnimationCurve DefenderDashCurve;
    public float followSpeedModifier      =     2f;
    public float UnactiveDuration         =     5f;
    public float DefenderExplosionDuration=   0.3f;
    public float RecreatingDuration       =   0.7f;
    //DefendersMainParameters;
    public float InteractionDistance      =     4f;
    public float Damage                   =    260f;
    
    

    public void PreformBehaviour()
    {
             if (State == DefenderState.UnactiveFollowingPlayer)          { PreformFollowingTarget(); }
        else if (State == DefenderState.ActiveFollowingPlayer)            { PreformFollowingTarget(); }
        else if (State == DefenderState.DashingToCenterOfDefendingObject) { PreformDashingToCenterOfDefendingObject(); }
        else if (State == DefenderState.Exloding)                         { PreformExloding();}
        else if (State == DefenderState.Recreating)                       { PreformRecreating(); }
    }

    //ActivateFunction
    public void ActivateDefense(IDefensable _defenseTarget)
    {
        Target = _defenseTarget.GetTransform();
        ToggleToDashingToCenterOfDefendingObject();
    }
    //PreformFuncitons
    public void PreformFollowingTarget()
    {
        Vector3 moveVector = Target.transform.position - transform.position;
        transform.position += moveVector * Time.deltaTime * followSpeedModifier;
    }
    
    public void PreformDashingToCenterOfDefendingObject()
    {
        Vector3 moveVector = Target.transform.position - transform.position;
        float distance = Vector2.Distance(Target.transform.position, transform.position);
        transform.position += moveVector * Time.deltaTime * DefenderDashCurve.Evaluate(distance);
        if(distance < 0.3f)
        {
            ToggleToExloding();
        }
        
    }
    public void PreformExloding()
    {
        //AnimateExplosion

    }

    public void PreformRecreating()
    {
        //AnimateRecreation
    }

    //ToggleFunction
    public void ToggleToUnactiveFollowingPlayer()
    {
        Target = SimulationInstance.Player.transform;
        State = DefenderState.UnactiveFollowingPlayer;
        TimeingManager.SchoudleDelayedFunctionTrigger(UnactiveDuration, ToggleToActiveFollowingPlayer);

    }
    public void ToggleToActiveFollowingPlayer()
    {
        State = DefenderState.ActiveFollowingPlayer;
        MoveBackToPlayersArsenal = true;

    }
    public void ToggleToDashingToCenterOfDefendingObject()
    {
        State = DefenderState.DashingToCenterOfDefendingObject;
    }
    public void ToggleToExloding()
    {
        State = DefenderState.Exloding;
        TimeingManager.SchoudleDelayedFunctionTrigger(DefenderExplosionDuration, TogglToRecreating);
        MoveToInteractingWithEnemiesList = true;
        Debug.Log("Defender Explosion!");

    }
    public void TogglToRecreating()
    {
        State = DefenderState.Recreating;
        TimeingManager.SchoudleDelayedFunctionTrigger(RecreatingDuration, ToggleToUnactiveFollowingPlayer);
    }



    public float GetDamage()
    {
        return Damage;
    }

    public float GetInteractionDistance()
    {
        return InteractionDistance;
    }

    public InteractionType GetInteractionType()
    {
        return myInteractionType;
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public IInteractableWithEnemy GetReferenceToMainObject()
    {
        return this;
    }

    public ForceObject GetReferenceToObject()
    {
        return this;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void ToggleImplodingEnemyAttack(IEnemyBase _attackedObject)
    {
        Debug.Log("Im Defender , i nod doing ImplodingEnemyAttack");
    }



    //public void LoadDefendersSpecs(DefenderSpecs _defnederSpecs)



    public override void Setup()
    {
        base.Setup();
        State = new DefenderState();
        State = DefenderState.ActiveFollowingPlayer;
        myInteractionType = InteractionType.Explosion;
        
    }
}
