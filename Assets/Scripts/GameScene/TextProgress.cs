using AssemblyCSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextProgress : MonoBehaviour
{

    private Text textProgress;
    // Use this for initialization
    private GameData data;
    private int score;
    private int max;
    private int passingStage;

    void Start()
    {
        score = 0;
        data = GameData.LoadFromJSONResource();
        passingStage = PlayerPrefHelper.GetPassingStage();
        max = data.levelData[passingStage].limit;
        textProgress = GameObject.Find("TextProgress").GetComponent<Text>();
        textProgress.text = score + "/" + max.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateTextScore()
    {
        score = score + 1;
        if (score >= max)
        {
            passingStage = passingStage + 1;
            PlayerPrefHelper.SaveCurrentStage(passingStage);
            int currentStage = PlayerPrefHelper.GetCurrentStage();
            if (passingStage > currentStage)
            {
                PlayerPrefHelper.SaveCurrentStage(currentStage + 1);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            textProgress.text = score + "/" + max.ToString();
        }
    }
}
