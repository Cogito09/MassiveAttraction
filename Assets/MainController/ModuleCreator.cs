using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleCreator
{
    public T Create<T>(string moduleName) where T : MonoBehaviour
    {
        GameObject newModuleHolder = new GameObject(moduleName);
        return newModuleHolder.AddComponent<T>();
    }
    public T Create<T, Parent>(string moduleName, Parent parent) where T : MonoBehaviour where Parent : MonoBehaviour
    {
        GameObject newModuleHolder = new GameObject(moduleName);
        newModuleHolder.gameObject.transform.SetParent(parent.gameObject.transform, true);
        return newModuleHolder.AddComponent<T>();
    }
}
