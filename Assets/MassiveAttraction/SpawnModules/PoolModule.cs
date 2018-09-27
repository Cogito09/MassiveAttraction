using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolModule : MonoBehaviourBaseModuleAccessObject
{
    public Dictionary<int, Queue<PoolableObject>> _poolDictionary = new Dictionary<int, Queue<PoolableObject>>();

    public void CreatePool(Transform prefab, int capacity, string name)
    {
        GameObject createdPoolHolder = new GameObject(name);
        createdPoolHolder.transform.SetParent(this.transform);

        int poolKey = prefab.GetInstanceID();
        Queue<PoolableObject> newPool = new Queue<PoolableObject>(capacity);

        for (int i = 0; i < capacity; i++)
        {
            PoolableObject newObject = InstantiateModule.InstantiateObjectWithScript<PoolableObject>(prefab);
            newObject.SetPoolKey(poolKey);
            newObject.gameObject.transform.SetParent(createdPoolHolder.transform);
            newObject.gameObject.SetActive(false);
            newPool.Enqueue(newObject);
        }
        _poolDictionary.Add(poolKey, newPool);
    }
    public PoolableObject Reuse(Transform prefab)
    {
        int poolKey = prefab.GetInstanceID();
        PoolableObject objectToSpawn = _poolDictionary[poolKey].Dequeue();
        objectToSpawn.gameObject.SetActive(true);
        objectToSpawn.Setup();
        objectToSpawn.Reset();
        return objectToSpawn;
    }
    public void BackObjectToPool(PoolableObject obj)
    {
        int poolKey = obj.GetPoolKey();
        obj.gameObject.SetActive(false);
        _poolDictionary[poolKey].Enqueue(obj);
    }
}
