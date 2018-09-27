using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourBaseModuleAccessObject : MonoBehaviour
{
    public ModuleCreator ModuleCreator { get { return MainController.Instance.ModuleCreator; } }
    public MassiveAttraction MassiveAttraction { get { return MainController.Instance.MassiveAttraction; } }
    public SimulationInstance SimulationInstance { get { return MainController.Instance.MassiveAttraction.SimulationInstance; } }
    public InstantiateModule InstantiateModule { get { return MainController.Instance.MassiveAttraction.InstantiateModule; } }
    public PoolModule PoolModule { get { return MainController.Instance.MassiveAttraction.PoolModule; } }
    public ObjectSpawner ObjectSpawner { get { return MainController.Instance.MassiveAttraction.ObjectSpawner; } }
    public PrefabCollection PrefabCollection { get { return MainController.Instance.MassiveAttraction.PrefabCollection; } }
    public LevelLoader LevelLoader { get { return MainController.Instance.MassiveAttraction.LevelLoader; } }
    public InputModulePlayMode InputModulePlayMode { get { return MainController.Instance.MassiveAttraction.InputModulePlayMode; } }
    public InputModuleUIMode InputModuleUIMode { get { return MainController.Instance.MassiveAttraction.InputModuleUIMode; } }
    public TimeingManager TimeingManager { get { return MainController.Instance.MassiveAttraction.TimeingManager; } }
    public StageCreator StageCreator { get { return MainController.Instance.MassiveAttraction.StageCreator; } }
    public CoreParametersCreator CoreParametersCreator { get { return MainController.Instance.MassiveAttraction.CoreParametersCreator; } }
    public PointSystemCreator PointSystemCreator { get { return MainController.Instance.MassiveAttraction.PointSystemCreator; } }
} 