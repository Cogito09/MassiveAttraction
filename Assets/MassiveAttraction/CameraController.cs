using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviourBaseModuleAccessObject
{
    Transform camTransform;
    Transform target;

    public void SetupTarget()
    {
        target = SimulationInstance.Player.transform;
    }
    public void Awake()
    {
        camTransform = Camera.main.transform;
        Camera.main.orthographicSize = 18;
    }
    public void FollowPlayer()
    {
        if (target != null)
        {
            Vector3 newCamPosition = new Vector3();
            newCamPosition.x = target.position.x;
            newCamPosition.y = target.position.y;
            newCamPosition.z = -10;
            camTransform.position = newCamPosition;
        }
    }
}
