using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalTime : MonoBehaviour
{
    private Text times;

    private void Awake()
    {
        times = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        string temp = "";
        for (int i = 0; i < GameManager.times.Length; i++)
        {
            temp += GameManager.times[i];
            if (i < GameManager.times.Length - 1)
            {
                temp += "\n";
            }
        }
        times.text = temp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
