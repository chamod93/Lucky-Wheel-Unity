using UnityEngine;
using UnityEngine.UI;
using AssemblyCSharp;
using UnityEngine.SceneManagement;


public class ButtonStageController : MonoBehaviour
{
    private Button buttonStage;
    private Text textLevel;

	[SerializeField]
	PersonalSceneManager sceneManager;

    void Start()
    {
        buttonStage = GameObject.Find("ButtonStage").GetComponent<Button>();
        textLevel = buttonStage.transform.Find("Text").GetComponent<Text>();
        textLevel.text = PlayerPrefHelper.GetCurrentStage().ToString();
    }

	public void onButtonStageClicked()
	{
		MusicPlayer.getInstance ().handleClickSound ();
		sceneManager.LoadScene ("StageScene");
	}

    private void Update()
    {

    }

}

