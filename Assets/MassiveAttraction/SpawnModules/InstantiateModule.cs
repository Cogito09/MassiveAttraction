using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateModule : MonoBehaviourBaseModuleAccessObject
{

    public T InstantiateObjectWithScript<T>(Transform prefab)
    {
        Transform obj = Instantiate(prefab) as Transform;
        T objScript = obj.GetComponent<T>();
        return objScript;
    }

    public T[] InstantiateObjectWithScript<T>(Transform prefab, int numberOfInstances)
    {
        T[] Scripts = new T[numberOfInstances];

        for (int i = 0; i < numberOfInstances; i++)
        {
            Transform obj = Instantiate(prefab) as Transform;
            T objScript = obj.GetComponent<T>();
            Scripts[i] = objScript;
        }
        return Scripts;
    }

    public Transform InstantiateEmptyGameObject(string name)
    {
        var newObject = new GameObject(name);
        Transform newObjectTransform = newObject.transform;
        return newObjectTransform;
    }
}
