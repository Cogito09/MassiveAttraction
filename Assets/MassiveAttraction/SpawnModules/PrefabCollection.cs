using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabCollection : MonoBehaviour
{
    public Transform Player;
    public Transform Meteor;
    public Transform Core;
    public Transform Bomber;
    public Transform SpawnPointWheel;
    public Transform PositionPoint;
    public Transform ForceObjectCloud;
    public Transform Exploder;
    public Transform Imploder;
    public Transform Defender;
    public Transform OrbitatingSpawnKickTargetPoint;
    public Transform OrbitationWheel;
    public Transform PointRing;

    public void LoadAllPrefabs()
    {
        Player = LoadPrefab("Player");
        Meteor = LoadPrefab("Meteor");
        Core = LoadPrefab("Core");
        Bomber = LoadPrefab("Bomber");
        SpawnPointWheel = LoadPrefab("SpawnPointWheel");
        PositionPoint = LoadPrefab("PositionPoint");
        ForceObjectCloud = LoadPrefab("ForceObjectCloud");
        Exploder = LoadPrefab("Exploder");
        Imploder = LoadPrefab("Imploder");
        Defender = LoadPrefab("Defender");
        OrbitationWheel = LoadPrefab("OrbitationWheel");
        OrbitatingSpawnKickTargetPoint = LoadPrefab("OrbitatingSpawnKickTargetPoint");
        PointRing = LoadPrefab("PointRing");

    }
    private Transform LoadPrefab(string _prefabNameInResourecsFolder)
    {
        GameObject prefabToLoad = Resources.Load(_prefabNameInResourecsFolder) as GameObject;
        if (prefabToLoad == null)
        {
            Debug.Log(_prefabNameInResourecsFolder + "not Loaded");
        }
        return prefabToLoad.transform;
    }

    public void Setup()
    {
        LoadAllPrefabs();
    }
}

