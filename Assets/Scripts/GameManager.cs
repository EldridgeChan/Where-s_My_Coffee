using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    static public bool firstLoad = true;
    static public scene currScene = scene.TitleScene;
    static public string[] times = new string[4];

    public enum scene
    {
        TitleScene,
        LevelSelect,
        tutLevel,
        levelOne,
        levelTwo,
        levelThree,
        levelFour,
        finishScene
    }


}
