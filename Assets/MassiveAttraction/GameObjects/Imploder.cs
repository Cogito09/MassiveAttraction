using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ImploderState
{
    UnactiveFollowingPlayer,
    ActiveFollowingPlayer,
    PreformingFireKickoff,
    PreformingDash,
    PreformingImplosionDrift,
    PreformingImplodingImpact,
    ImplodiningObjectAttacking
}
public class Imploder : ForceObject , IAnimatable , IInteractableWithEnemy
{
    public bool isReadyToDestroyImplodingMinionsOnEnemy { get; set; }
    public ImploderState State;
    public InteractionType myInteractionType;
    public float implosionRange = 1.5f;

    public AnimationCurve DistanceForceModifier;
    public float DashForceMultiplier = 1;
    public float FollowForceMultiplier = 0.3f;
    public float BackToPlanetForceMultiplier = 1;
    public float DashDrag = 20f;
    public float FollowPathDrag = 70f;
    public float BackToPlanetDrag = 4f;
    public float UnactiveDuration = 2f;

    public int indexOfCurrentFollowedPathPoint;
    public Vector3[] path;

    public bool MoveBackToPlayersArsenal { get; set; }
    public bool MoveToInteractingWithEnemiesList      { get; set; }
    public bool MoveOutFromInteractingWithEnemiesList { get; set; }

    public float InteractonDistance = 3f;
    public IEnemyBase TheAttackedObject;

    public void PreformBehaviour()
    {
             if(State == ImploderState.PreformingFireKickoff)     { PreformingFireKickoff();  }
        else if(State == ImploderState.PreformingDash)            { PreformDash();            }
        else if(State == ImploderState.PreformingImplosionDrift)  { PreformImplosionDrift();  }
        else if(State == ImploderState.PreformingImplodingImpact) { PreformImplodingImpact(); }
        else if(State == ImploderState.UnactiveFollowingPlayer)   { PreforPlayerFollow();     }
        else if(State == ImploderState.ActiveFollowingPlayer)     { PreforPlayerFollow();     }
        else if(State == ImploderState.ImplodiningObjectAttacking){ PreformObjectAttacking(); }
    }
    public void ActivateDriftBehaviour(Vector3[] _path)
    {
        path = _path;
        TogglePreformingDash();
    }
    public void TogglePreformingDash()
    {
        indexOfCurrentFollowedPathPoint = 0;
        State = ImploderState.PreformingDash;
        //toggle AnimateDash
    } 
    public void TogglePreformingImplosionDrift()
    {
        isReadyToDestroyImplodingMinionsOnEnemy = true;
        MoveToInteractingWithEnemiesList = true;
        State = ImploderState.PreformingImplosionDrift;
        myInteractionType = InteractionType.Implosion;
        //toggle AnimateImplosionDrift
    }
    public void TogglePreformingImplodingImpact()
    {
        isReadyToDestroyImplodingMinionsOnEnemy = true;
        State = ImploderState.PreformingImplodingImpact;
        TimeingManager.SchoudleDelayedFunctionTrigger(2f, TogglePreformingUnactiveFollowingPlayer);
        myInteractionType = InteractionType.ImplodingImpact;
        //toggle Animate
    }
    public void TogglePreformingUnactiveFollowingPlayer()
    {
        TheAttackedObject = null;
        isReadyToDestroyImplodingMinionsOnEnemy = false;

        MoveOutFromInteractingWithEnemiesList = true;
        State = ImploderState.UnactiveFollowingPlayer;
        TimeingManager.SchoudleDelayedFunctionTrigger(UnactiveDuration, TogglePrefrominActiveFollowingPlayer);
        //toggle Animate
    }
    public void TogglePrefrominActiveFollowingPlayer()
    {
        State = ImploderState.ActiveFollowingPlayer;
        MoveBackToPlayersArsenal = true;
        
        path = null;
        //toggle Aniamte
    }

    public void ToggleImplodingEnemyAttack(IEnemyBase _theAttackedObeject)
    {
        State = ImploderState.ImplodiningObjectAttacking;
        TheAttackedObject = _theAttackedObeject;
        TimeingManager.SchoudleDelayedFunctionTrigger(2f, TogglePreformingUnactiveFollowingPlayer);
        
    }


    public void PreformObjectAttacking()
    {
        Vector3 vector = TheAttackedObject.GetPosition() - transform.position;
        transform.position += vector * Time.deltaTime * 2f;
    }

    public void PreformingFireKickoff()
    {
        //Aniamte
        //ForcePush
    }
    public void PreformDash()
    {
        float _distance = Vector2.Distance(path[0], transform.position);
        Vector2 _moveVector = path[0] - transform.position;
        rb.AddForce(_moveVector * DashForceMultiplier * DistanceForceModifier.Evaluate(_distance));
        rb.drag = DashDrag;
        if (_distance < 0.3f)
        {
            TogglePreformingImplosionDrift();
            indexOfCurrentFollowedPathPoint++;
        }
    }
    public void PreformImplosionDrift()
    {
        float _distance = Vector2.Distance(path[indexOfCurrentFollowedPathPoint], transform.position);
        Vector2 _moveVector = path[indexOfCurrentFollowedPathPoint] - transform.position;
        rb.AddForce(_moveVector.normalized * FollowForceMultiplier);
        rb.drag = FollowPathDrag;
        if (_distance < 2f)
        {
            indexOfCurrentFollowedPathPoint++;
            if(indexOfCurrentFollowedPathPoint == path.Length-1)
            {
                TogglePreformingImplodingImpact();
            }
        }
    }
    public void PreformImplodingImpact()
    {
        //Aniamte
    }

    public void PreforPlayerFollow()
    {
        //float _distance = Vector2.Distance(SimulationInstance.Player.transform.position, transform.position);
        Vector3 _moveVector = SimulationInstance.Player.transform.position - transform.position;
        transform.position += _moveVector * Time.deltaTime;
        //rb.AddForce(_moveVector.normalized * BackToPlanetForceMultiplier);
        //rb.drag = BackToPlanetDrag;

    }










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
        return this as Imploder;
    }
    public Transform GetTransform()
    {
        return transform;
    }

    public float GetDamage()
    {
        float damage = 0;
        return damage;
    }

    public float GetInteractionDistance()
    {
        return InteractonDistance;
    }






    public override void Setup()
    {
        base.Setup();
        State = new ImploderState();
        State = ImploderState.ActiveFollowingPlayer;
        myInteractionType = new InteractionType();
    }
}
