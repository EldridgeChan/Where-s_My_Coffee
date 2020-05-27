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
        if (scene.buildIndex == 5)
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
        SceneManager.LoadScene((int)GameManager.scene.TitleScene);
    }

    public void loadTutLevel()
    {
        SceneManager.LoadScene((int)GameManager.scene.tutLevel);
    }

    public void loadLevelOne()
    {
        SceneManager.LoadScene((int)GameManager.scene.levelOne);
    }

    public void loadLevelTwo()
    {
        SceneManager.LoadScene((int)GameManager.scene.levelTwo);
    }

    public void loadLevelThree()
    {
        SceneManager.LoadScene((int)GameManager.scene.levelThree);
    }

    public void loadLevelFour()
    {
        SceneManager.LoadScene((int)GameManager.scene.levelFour);
    }

}
