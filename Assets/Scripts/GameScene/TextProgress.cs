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
    private int currentLevel;

    void Start()
    {
        score = 0;
        data = GameData.LoadFromJSONResource();
        currentLevel = PlayerPrefHelper.GetCurrentStage();
        max = data.levelData[currentLevel].limit;
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
			currentLevel = currentLevel + 1;
			PlayerPrefHelper.SaveCurrentStage(currentLevel);
			MusicPlayer.getInstance ().handleSuccessSound ();
			textProgress.text = score + "/" + max.ToString();
			StartCoroutine (GoToNextLevel (5));
        }
        else
        {
            textProgress.text = score + "/" + max.ToString();
        }
    }

	IEnumerator GoToNextLevel(float time)
	{
		yield return new WaitForSeconds (time);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
}
