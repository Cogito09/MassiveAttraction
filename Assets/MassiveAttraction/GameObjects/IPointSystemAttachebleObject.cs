using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPointSystemAttachebleObject
{
    Transform Target { get; set; }
    int followPointSystemIndex { get; set; }
    void SetFollowTransform(Transform _transform);
    void SetFollowPointIndex(int _index);
}
