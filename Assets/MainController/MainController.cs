using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainController : MonoBehaviour
{
    private static MainController _instance;
    public static MainController Instance { get { return _instance; } }

    public ModuleCreator ModuleCreator = new ModuleCreator();
    public MassiveAttraction MassiveAttraction;

    private void BuildMassiveAttractionSmulation()
    {
        MassiveAttraction = ModuleCreator.Create<MassiveAttraction>("MassiveAttraction");
        MassiveAttraction.LaunchModules();
        MassiveAttraction.LaunchStartupFunctions();
        MassiveAttraction.LoadPlayer();
        MassiveAttraction.LaunchTestLevel();
    }
    private void RestartGame()
    {
        SceneManager.LoadScene(0);
        BuildMassiveAttractionSmulation();
    }
    private void ExitGame()
    {
        Application.Quit();
    }
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        BuildMassiveAttractionSmulation();
    }
    private void Start()
    {       

    }
}
