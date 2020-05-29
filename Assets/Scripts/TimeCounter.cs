﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    private float timeCounter = 0f;

    [SerializeField]
    private Text timeText;

    private InteractionManager interaction;

    private void Awake()
    {
        interaction = GameObject.FindWithTag("GameManager").GetComponent<InteractionManager>();
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
