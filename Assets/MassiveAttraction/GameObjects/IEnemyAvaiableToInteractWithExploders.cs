using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAvaiableToInteractWithExploders
{

	bool isDestroyed { get; set; }

    void SubtractHp(float _damage);
    void PushOutFromExplosionPosition(Vector3 _position, float _damage);
}
