using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceObjectsManager : NonMonoBehaviourBaseModuleAccess
{
    private Player player;

    public Queue<Defender> Defenders = new Queue<Defender>(9);
    public Queue<Exploder> Exploders = new Queue<Exploder>(18);
    public Queue<Imploder> Imploders = new Queue<Imploder>(9);

    public void PreformBehaviours()
    {
        foreach (Exploder _exploder in Exploders)
        {
            _exploder.PreformBehaviour();
        }
        foreach (Imploder _imploder in Imploders)
        {
            _imploder.PreformBehaviour();
        }
        foreach(Defender _defender in Defenders)
        {
            _defender.PreformBehaviour();
        }
    }

  
    public void LaunchExploder(Vector2 _target)
    {
        if (Exploders.Count > 0)
        {
            Exploder _exploder = Exploders.Dequeue();
            _exploder.ActivatePursuit(_target);
            SimulationInstance.launchedExploders.Add(_exploder);
        }
    }
    public void LaunchImploder(Vector3[] _path)
    {
        if(Imploders.Count > 0)
        {
            Imploder _imploder = Imploders.Dequeue();
            _imploder.ActivateDriftBehaviour(_path);
            _imploder.MoveBackToPlayersArsenal = false;
            _imploder.MoveOutFromInteractingWithEnemiesList = false;
            _imploder.MoveToInteractingWithEnemiesList = false;
            SimulationInstance.launchedImploders.Add(_imploder);

        }
    }
    public void LaunchDefender(IDefensable _target)
    {
        if (Defenders.Count > 0)
        {
            Debug.Log("DefenderLaunchedf");
            Defender _defneder = Defenders.Dequeue();
            _defneder.ActivateDefense(_target);
            SimulationInstance.launchedDefenders.Add(_defneder);

        }
    }


    public ForceObjectsManager(Player _playerTheManagerIsAttatchedTo ,ForceObjectStartSpawnParameters _forceObjectStartSpawnParameters)
    {
        player = _playerTheManagerIsAttatchedTo;

        for (int i = 0; i < _forceObjectStartSpawnParameters.DefendersCount; i++)
        {
            Defender _createdDefender = ObjectSpawner.SpawnDefender(player.transform.position);
            _createdDefender.Target = player.transform;
            Defenders.Enqueue(_createdDefender);

        }
        for(int i = 0; i < _forceObjectStartSpawnParameters.ExplodersCount; i++)
        {
            Exploders.Enqueue(ObjectSpawner.SpawnExploder(player.transform.position));
        }
        for(int i = 0; i < _forceObjectStartSpawnParameters.ImplodersCount; i++)
        {
            Imploders.Enqueue(ObjectSpawner.SpawnImploder(player.transform.position));
        }
    }
}
