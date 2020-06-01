using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{

    private void Awake()
    {
        if (!GameManager.firstLoad)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);    
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.firstLoad = false;
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == (int)GameManager.scene.TitleScene)
        {
            GameObject.FindWithTag("TutLevelButton").GetComponent<Button>().onClick.AddListener(loadTutLevel);
            GameObject.FindWithTag("LevelOneButton").GetComponent<Button>().onClick.AddListener(loadLevelOne);
            GameObject.FindWithTag("LevelTwoButton").GetComponent<Button>().onClick.AddListener(loadLevelTwo);
            GameObject.FindWithTag("LevelThreeButton").GetComponent<Button>().onClick.AddListener(loadLevelThree);
            GameObject.FindWithTag("LevelFourButton").GetComponent<Button>().onClick.AddListener(loadLevelFour);
        } else
        {
            GameObject.FindWithTag("TitleSceneButton").GetComponent<Button>().onClick.AddListener(loadTitleScene);
        }
    }

    public void loadTitleScene()
    {
        GameManager.currScene = GameManager.scene.TitleScene;
        SceneManager.LoadScene((int)GameManager.currScene);
    }

    public void loadTutLevel()
    {
        GameManager.currScene = GameManager.scene.tutLevel;
        SceneManager.LoadScene((int)GameManager.currScene);
    }

    public void loadLevelOne()
    {
        GameManager.currScene = GameManager.scene.levelOne;
        SceneManager.LoadScene((int)GameManager.currScene);
    }

    public void loadLevelTwo()
    {
        GameManager.currScene = GameManager.scene.levelTwo;
        SceneManager.LoadScene((int)GameManager.currScene);
    }

    public void loadLevelThree()
    {
        GameManager.currScene = GameManager.scene.levelThree;
        SceneManager.LoadScene((int)GameManager.currScene);
    }

    public void loadLevelFour()
    {
        GameManager.currScene = GameManager.scene.levelFour;
        SceneManager.LoadScene((int)GameManager.currScene);
    }

    private void loadNextLevel()
    {
        GameManager.currScene = (GameManager.scene)(((int)GameManager.currScene + 1) % 6);
        SceneManager.LoadScene((int)GameManager.currScene);
    }

    public void loadAfterWin(float time)
    {
        Invoke("loadNextLevel", time);
    }
}
