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
    private Button buttonProgress;
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
        buttonProgress = GameObject.Find("ButtonProgress").GetComponent<Button>();
        buttonProgress.transform.Find("Text").GetComponent<Text>().text = "LV. " + passingStage;
        buttonProgress.GetComponent<Image>().fillAmount = 0;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateTextScore()
    {
        score = score + 1;

        buttonProgress.GetComponent<Image>().fillAmount = (float)score / max;
        if (score >= max)
        {
         
			MusicPlayer.getInstance ().handleSuccessSound ();
			textProgress.text = score + "/" + max.ToString();
            CompleteLevel();
        }
        else
        {
            textProgress.text = score + "/" + max.ToString();
        }
    }

    private void CompleteLevel()
    {
        int currentStage = PlayerPrefHelper.GetCurrentStage();
        passingStage = passingStage + 1;
        PlayerPrefHelper.SavePassingStage(passingStage);
        if (passingStage > currentStage)
        {
            PlayerPrefHelper.SaveCurrentStage(passingStage);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	IEnumerator GoToNextLevel(float time)
	{
		yield return new WaitForSeconds (time);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
