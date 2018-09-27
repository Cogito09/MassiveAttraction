using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFollowingEnemy
{
    Transform playerTransform { get; set; }
    Vector3 moveVector { get; set; }

    void PreformStartupKick();
    void PreformMoveTowardsPlayer();
    void SetupPlayerTransform(Transform _targetTransform);
}
