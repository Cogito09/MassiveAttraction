using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType
{
    Explosion,
    Implosion,
    ImplodingImpact,
}
public interface IInteractableWithEnemy
{
    bool MoveBackToPlayersArsenal { get; set; }
    bool isReadyToDestroyImplodingMinionsOnEnemy { get; set; }
    void ToggleImplodingEnemyAttack(IEnemyBase _attackedObject);
    IInteractableWithEnemy GetReferenceToMainObject() ;
    InteractionType GetInteractionType();
    ForceObject GetReferenceToObject();
    Vector2 GetPosition();
    Transform GetTransform();
    float GetDamage();
    float GetInteractionDistance();

    bool MoveToInteractingWithEnemiesList { get; set; }
    bool MoveOutFromInteractingWithEnemiesList { get; set; }
}
