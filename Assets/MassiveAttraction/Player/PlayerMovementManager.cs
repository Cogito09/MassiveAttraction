using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementManager
{
    public Transform PlayerTransform;
    private Transform targetToFollowTransform;
    public float followSpeedModifier = 0.05f;

    public void PreformMovementBehaviour()
    {
        FollowTarget();
    }
    public void ChangeFollowPoint(Transform _targetPoint)
    {
        targetToFollowTransform = _targetPoint;
    }
    private void FollowTarget()
    {
        Vector3 moveVector = targetToFollowTransform.position - PlayerTransform.position;
        PlayerTransform.position += moveVector * Time.deltaTime  * followSpeedModifier;
    }
    private void OrbitingAround()
    {

    }
    public PlayerMovementManager(Transform _playerTransform)
    {
        PlayerTransform = _playerTransform;
    }
}
