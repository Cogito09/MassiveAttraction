using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRing : MonoBehaviour
{
    public Transform followTarget;
    public PositionPoint[] Points;
    public float rotationSpeed;
    public float followSpeed;

    private Vector3 moveVector;
    private float currentRotation = 0;
    float zRotationParameter = 0;
    float _xRotation = 0 ;

    private Vector3 positionInPreviousFrame = new Vector3(0, 0, 0);

    private float xRotationParameter = 0;

    public void ProcessBehaviour(float _speed)
    {
        moveVector = followTarget.transform.position - transform.position;
        Follow(_speed);
        UpdateMoveTowardsZRotation(_speed);
        UpdateRotateAroundAxisParameter(_speed);
        SetNewRotation();

    }
    public void Follow(float _speed)
    {
        float followSpeedFactor = followSpeed * _speed;
        transform.position += moveVector * Time.deltaTime * followSpeedFactor;
    }
    public void SetNewRotation()
    {


        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, zRotationParameter));

        xRotationParameter += rotationSpeed;
        transform.parent.transform.rotation = Quaternion.AngleAxis(xRotationParameter, new Vector3(1, 0, 0));

    }
    public void UpdateMoveTowardsZRotation(float _speed)
    {
        if(positionInPreviousFrame != transform.position)
        {
            zRotationParameter = GetRotateTowardsTargetParameterZ();
        }
        UpdateLastPositionParameter();
    }
    public void UpdateRotateAroundAxisParameter(float _speed)
    {
        _xRotation = GetRotateAroundAxisParameterX(_speed);
    }
    public float GetRotateAroundAxisParameterX(float _speed)
    {
        float _rotationSpeedFactor = rotationSpeed * _speed;
        currentRotation += _rotationSpeedFactor;
        return currentRotation;
    }
    public float GetRotateTowardsTargetParameterZ()
    {
        float rotationParameter;
        rotationParameter = GetRotationTOwardsTargetParameters();
        return rotationParameter + 90;
    }
    private float GetRotationTOwardsTargetParameters()
    {
        Vector3 direction = GetCurrentDirection();
        Vector3 baseDirection = new Vector3(0, 1, 0);

        float angleBetweenDirections = Vector2.Angle(direction, baseDirection);

        if (direction.x < 0)
        {
            return angleBetweenDirections;
        }
        else if (direction.x > 0)
        {
            return 360 - angleBetweenDirections;
        }
        else return angleBetweenDirections;
    }
    private Vector2 GetCurrentDirection()
    {
        Vector2 currentMoveDirection = new Vector2();
        currentMoveDirection.x = transform.position.x - positionInPreviousFrame.x;
        currentMoveDirection.y = transform.position.y - positionInPreviousFrame.y;
        return currentMoveDirection;
    }
    private void UpdateLastPositionParameter()
    {
        positionInPreviousFrame = transform.position;
    }

    public  void SpawnZAxisRotationParentObject()
    {
        Transform transformOfParentObject = MainController.Instance.MassiveAttraction.InstantiateModule.InstantiateEmptyGameObject("zAxisRotatorForRing").transform;
        this.transform.SetParent(transformOfParentObject);
    }
}
