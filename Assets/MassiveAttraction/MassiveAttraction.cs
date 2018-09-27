     using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassiveAttraction : MonoBehaviour
{
    public SimulationInstance SimulationInstance;
    public UiInstance UiInstance;

    public Player playerGlobal;

    public TimeingManager TimeingManager;
    public InstantiateModule InstantiateModule;
    public PoolModule PoolModule;
    public ObjectSpawner ObjectSpawner;
    public PrefabCollection PrefabCollection;
    public LevelLoader LevelLoader;
    public LevelCollection LevelCollection;
    public InputModuleUIMode InputModuleUIMode;
    public InputModulePlayMode InputModulePlayMode;
    public StageCreator StageCreator;
    public CoreParametersCreator CoreParametersCreator;
    public PointSystemCreator PointSystemCreator;

    private InputAnswer inputAnswer;
    private bool inPlaymode;
    public bool SimulationLoaded = false;


    public void Update()
    {
        if (SimulationLoaded)
        {
            if (inPlaymode)
            {
                TimeingManager.PreformTimerManagerCycle();

                inputAnswer = InputModulePlayMode.ListenToInput();
                if (inputAnswer.isPaused)
                      { inPlaymode = false; }
                else
                {
                    SimulationInstance.Player.LaunchAction(inputAnswer);
                    SimulationInstance.PreformGameplay();
                }
            }
            else
            {
                Time.timeScale = 0;
                inputAnswer = InputModuleUIMode.ListenInput();
                if (inputAnswer.isPaused == false)
                {
                    Time.timeScale = 1;
                    inPlaymode = true;
                }
                else { UiInstance.PreformUIMode(inputAnswer); }
            }
        }
        
    }
    public void OnGameModeSwap()
    {
        if(inPlaymode == false) // if just toggled to pauseMode
        {
            TimeingManager.ToggleToPauseMode();
        }
        if(inPlaymode == true) // if just toggled to playMode;
        {
            TimeingManager.ToggleToPlayMode();
        }
    }
    public void LaunchModules()
    {
        InstantiateModule     = MainController.Instance.ModuleCreator.Create<InstantiateModule>   ("InstantiateModule");
        PoolModule            = MainController.Instance.ModuleCreator.Create<PoolModule>          ("PoolModule");
        PrefabCollection      = MainController.Instance.ModuleCreator.Create<PrefabCollection>    ("PrefabCollection");
        ObjectSpawner         = MainController.Instance.ModuleCreator.Create<ObjectSpawner>       ("ObjectSpawner");
        LevelCollection       = new LevelCollection();
        LevelLoader           = new LevelLoader();
        TimeingManager        = new TimeingManager();
        InputModuleUIMode     = new InputModuleUIMode();
        InputModulePlayMode   = new InputModulePlayMode();
        StageCreator          = new StageCreator();
        CoreParametersCreator = new CoreParametersCreator();
        PointSystemCreator    = new PointSystemCreator();

    }
    public void LoadPlayer()
    {
        playerGlobal = ObjectSpawner.SpawnPlayer(Vector3.zero);
        playerGlobal.Setup(new ForceObjectStartSpawnParameters(3,3,3));

    }
    public void LaunchStartupFunctions()
    {
        PrefabCollection.Setup();
    }
    public void LaunchTestLevel()
    {
        SimulationInstance = MainController.Instance.ModuleCreator.Create<SimulationInstance>("SimulationInstance");
        SimulationInstance.Player = playerGlobal;
        SimulationInstance.LaunchInstance();
        SimulationInstance.LoadLevel(LevelLoader.SetupAndReturnChoosenLevel(LevelCollection.testLevel));
        SimulationInstance.GameplayController.StartStage();
        SimulationLoaded = true;
        inPlaymode = true;
    }
}
