using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    private float timeCounter = 0f;

    [SerializeField]
    private Text timeText;

    private RectTransform timerTransform;
    private InteractionManager interaction;

    private void Awake()
    {
        interaction = GameObject.FindWithTag("GameManager").GetComponent<InteractionManager>();
        timerTransform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!interaction.isWin)
        {
            timeCounter += Time.deltaTime;
            updateTime();
        }
        
    }

    public void win()
    {
        string time = timeText.text;
        timeText.text = "Congratulations!! \n" + "Your time is " + time;
        timeText.fontSize = 72;
        timerTransform.sizeDelta = new Vector2(900f, 600f);
        timerTransform.localPosition = Vector2.zero;
        if (GameManager.currScene != GameManager.scene.tutLevel) {
            GameManager.times[(int)GameManager.currScene - 3] = time;
        }
    }

    private void updateTime()
    {
        timeText.text = calMinute() + ":" + calSec() + ":" + calmSec();
    }

    private string calMinute()
    {
        string result = "";
        int minute = (int)(timeCounter / 60f);
        if (minute < 10)
        {
            result += "0";
        }
        return result + minute;
    }

    private string calSec()
    {
        int sec = (int)(timeCounter % 60);
        string result = "";
        if (sec < 10)
        {
            result += "0";
        }
        return result + sec;
    }

    private string calmSec()
    {
        int mSec = (int)((timeCounter * 100) % 100);
        string result = "";
        if (mSec < 10)
        {
            result += "0";
        }
        return  result + mSec;
    }
}
