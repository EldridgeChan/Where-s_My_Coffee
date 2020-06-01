using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    static public bool firstLoad = true;
    static public scene currScene = scene.TitleScene;

    public enum scene
    {
        TitleScene,
        tutLevel,
        levelOne,
        levelTwo,
        levelThree,
        levelFour
    }


}
