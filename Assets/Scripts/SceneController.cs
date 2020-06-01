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
            GameObject.FindWithTag("LevelSelectButton").GetComponent<Button>().onClick.AddListener(loadLevelSelectScene);
            GameObject.FindWithTag("QuitButton").GetComponent<Button>().onClick.AddListener(quitGame);

        } else if (scene.buildIndex == (int)GameManager.scene.LevelSelect)
        {
            GameObject.FindWithTag("LevelOneButton").GetComponent<Button>().onClick.AddListener(loadLevelOne);
            GameObject.FindWithTag("LevelTwoButton").GetComponent<Button>().onClick.AddListener(loadLevelTwo);
            GameObject.FindWithTag("LevelThreeButton").GetComponent<Button>().onClick.AddListener(loadLevelThree);
            GameObject.FindWithTag("LevelFourButton").GetComponent<Button>().onClick.AddListener(loadLevelFour);
            GameObject.FindWithTag("TitleSceneButton").GetComponent<Button>().onClick.AddListener(loadTitleScene);
        }
        else if (scene.buildIndex == (int)GameManager.scene.tutLevel)
        {
            GameObject.FindWithTag("TitleSceneButton").GetComponent<Button>().onClick.AddListener(loadTitleScene);
        } else
        {
            GameObject.FindWithTag("LevelSelectButton").GetComponent<Button>().onClick.AddListener(loadLevelSelectScene);
        }
    }

    public void loadTitleScene()
    {
        CancelInvoke();
        GameManager.currScene = GameManager.scene.TitleScene;
        SceneManager.LoadScene((int)GameManager.currScene);
    }

    public void loadLevelSelectScene()
    {
        CancelInvoke();
        GameManager.currScene = GameManager.scene.LevelSelect;
        SceneManager.LoadScene((int)GameManager.currScene);
    }

    public void loadTutLevel()
    {
        CancelInvoke();
        GameManager.currScene = GameManager.scene.tutLevel;
        SceneManager.LoadScene((int)GameManager.currScene);
    }

    public void loadLevelOne()
    {
        CancelInvoke();
        GameManager.currScene = GameManager.scene.levelOne;
        SceneManager.LoadScene((int)GameManager.currScene);
    }

    public void loadLevelTwo()
    {
        CancelInvoke();
        GameManager.currScene = GameManager.scene.levelTwo;
        SceneManager.LoadScene((int)GameManager.currScene);
    }

    public void loadLevelThree()
    {
        CancelInvoke();
        GameManager.currScene = GameManager.scene.levelThree;
        SceneManager.LoadScene((int)GameManager.currScene);
    }

    public void loadLevelFour()
    {
        CancelInvoke();
        GameManager.currScene = GameManager.scene.levelFour;
        SceneManager.LoadScene((int)GameManager.currScene);
    }

    public void quitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    private void loadNextLevel()
    {
        GameManager.currScene = (GameManager.scene)(((int)GameManager.currScene + 1) % 7);
        SceneManager.LoadScene((int)GameManager.currScene);
    }

    public void loadAfterWin(float time)
    {
        Invoke("loadNextLevel", time);
    }
}
