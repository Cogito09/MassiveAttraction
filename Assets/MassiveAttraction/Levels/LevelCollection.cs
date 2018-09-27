using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCollection
{

    public TestLevel testLevel;



    private void SetupLevels()
    {
        testLevel = new TestLevel();
    }
    public LevelCollection()
    {
        SetupLevels();
    }
}
