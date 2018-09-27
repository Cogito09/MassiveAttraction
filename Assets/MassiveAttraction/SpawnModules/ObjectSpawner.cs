using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviourBaseModuleAccessObject
{
    public Player SpawnPlayer(Vector3 _spawnPosition)
    {
        Player spawnedPlayer = InstantiateModule.InstantiateObjectWithScript<Player>(PrefabCollection.Player);
        spawnedPlayer.transform.position = _spawnPosition;
        return spawnedPlayer;
    }

    public Exploder SpawnExploder(Vector3 _spawnPosition)
    {
        Exploder spawnedExploder = InstantiateModule.InstantiateObjectWithScript<Exploder>(PrefabCollection.Exploder);
        spawnedExploder.Setup();
        ForceObjectCloud spawnedForceObjectCloud = InstantiateModule.InstantiateObjectWithScript<ForceObjectCloud>(PrefabCollection.ForceObjectCloud);
        spawnedForceObjectCloud.transform.SetParent(spawnedExploder.transform);
        spawnedExploder.transform.position = _spawnPosition;
        return spawnedExploder;
    }
    public Imploder SpawnImploder(Vector3 _spawnPosition)
    {
        Imploder spawnedImploder = InstantiateModule.InstantiateObjectWithScript<Imploder>(PrefabCollection.Imploder);
        spawnedImploder.Setup();
        ForceObjectCloud spawnedForceObjectCloud = InstantiateModule.InstantiateObjectWithScript<ForceObjectCloud>(PrefabCollection.ForceObjectCloud);
        spawnedForceObjectCloud.transform.SetParent(spawnedImploder.transform);
        spawnedImploder.transform.position = _spawnPosition;
        return spawnedImploder;
    }
    public Defender SpawnDefender(Vector3 _spawnPosition)
    {
        Defender spawnedDefender = InstantiateModule.InstantiateObjectWithScript<Defender>(PrefabCollection.Defender);
        ForceObjectCloud spawnedForceObjectCloud = InstantiateModule.InstantiateObjectWithScript<ForceObjectCloud>(PrefabCollection.ForceObjectCloud);
        spawnedForceObjectCloud.transform.SetParent(spawnedDefender.transform);
        spawnedDefender.transform.position = _spawnPosition;
        return spawnedDefender;
    }
    public OrbitationWheel SpawnOrbitationWheel(Vector2 _position)
    {
        OrbitationWheel spawnedOrbiationWheel = InstantiateModule.InstantiateObjectWithScript<OrbitationWheel>(PrefabCollection.OrbitationWheel);
        spawnedOrbiationWheel.Setup();
        spawnedOrbiationWheel.transform.position = _position;
        return spawnedOrbiationWheel;

    }
    public OrbitatingSpawnKickTargetPoint SpawnOrbitatingSpawnKickTargetPoint(Vector2 _position)
    {
        OrbitatingSpawnKickTargetPoint spawnedOrbiationWheel = InstantiateModule.InstantiateObjectWithScript<OrbitatingSpawnKickTargetPoint>(PrefabCollection.OrbitatingSpawnKickTargetPoint);
        spawnedOrbiationWheel.Setup();
        spawnedOrbiationWheel.transform.position = _position;
        return spawnedOrbiationWheel;
    }
    public SpawnPointWheel SpawnWheelSpawnerObject(Vector2 _spawnPosition)
    {
        SpawnPointWheel spawnedSpawnPointWheel  = InstantiateModule.InstantiateObjectWithScript<SpawnPointWheel>(PrefabCollection.SpawnPointWheel);
        spawnedSpawnPointWheel.transform.position = _spawnPosition;
        return spawnedSpawnPointWheel;
    }
    public Meteor SpawnMeteor(Vector2 _position)
    {
        Meteor newSpawnedMeteor = SpawnPollableObject(PrefabCollection.Meteor, _position) as Meteor;
        return newSpawnedMeteor;
    }
    public Meteor SpawnMeteorRandomlyOnSpawnWheel(SpawnPointWheel _spawnPointWheel)
    {
        Meteor newSpawnedMeteor = SpawnPollableObject(PrefabCollection.Meteor, _spawnPointWheel) as Meteor;
        return newSpawnedMeteor;
    }

    public Bomber SpawnBomberRandomlyOnSpawnWheel(SpawnPointWheel _spawnPointWheel)
    {
        Bomber newSpawnedBomber = SpawnPollableObject(PrefabCollection.Bomber, _spawnPointWheel) as Bomber;
        return newSpawnedBomber;
    }
    public Bomber SpawnBomber(Vector2 _spawnPosition)
    {
        Bomber _spawnedBobmer = SpawnPollableObject(PrefabCollection.Bomber,_spawnPosition) as Bomber;
        return _spawnedBobmer;
    }
    public Core SpawnCore(CoreParameters _coreSpawnParameters)
    {
        Core _spawnedCore = InstantiateModule.InstantiateObjectWithScript<Core>(PrefabCollection.Core);
        _spawnedCore.SetupParameters(_coreSpawnParameters);
        return _spawnedCore;
    }





    public PoolableObject SpawnPollableObject(Transform _prefab,SpawnPointWheel _spawnPointWheel)
    {
        Vector2 spawnPosition = _spawnPointWheel.GetRandomSpawnPosition();
        PoolableObject newPoolableObject = PoolModule.Reuse(_prefab);
        newPoolableObject.transform.position = spawnPosition;
        return newPoolableObject;
    }
    public PoolableObject SpawnPollableObject(Transform _prefab,Vector2 _spawnPosition)
    {
        Vector2 spawnPosition = _spawnPosition;
        PoolableObject newPoolableObject = PoolModule.Reuse(_prefab);
        newPoolableObject.transform.position = spawnPosition;
        return newPoolableObject;
    }
}
