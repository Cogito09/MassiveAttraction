using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyBase
{
    float objectRadius { get; set; }

    bool CanBehHitWithImploder { get; set; }
    bool isAvaiableForInteraction { get; set; }
    bool toBeRemovedFromSimulation { get; set; }
    IInteractableWithEnemy interactionTarget { get; set; }
    //float HealthPoints { get; set; }
    Rigidbody2D rb { get; set; }
    void SubtractHp(float _damage);
    void LaunchInteraction(IInteractableWithEnemy _interactionType);
    void PreformBehaviour();
    Vector3 GetPosition();
    Transform GetTransform();
    PoolableObject GetMainObject();
}
