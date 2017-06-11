using AssemblyCSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersonalSceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }


    public void LoadInGameScene()
    {
        int currentStage = PlayerPrefHelper.GetCurrentStage();
		print ("current stage:" + currentStage)  ;
        PlayerPrefHelper.SavePassingStage(currentStage);
        SceneManager.LoadScene("GameScene");
    }

	public void LoadSceneAfterClickButton(string name)
	{
		MusicPlayer.getInstance ().handleClickSound ();
		SceneManager.LoadScene (name);
	}

    public void Quit()
    {
        Application.Quit();
    }

}
