using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AssemblyCSharp;

public class ButtonSoundController : MonoBehaviour
{
    [SerializeField]
    private Sprite soundOn;

    [SerializeField]
    private Sprite soundOff;

	[SerializeField]
	private MusicPlayer musicPlayer;

    private Button buttonSound;
    private Image soundSprite;

    // Use this for initialization
    void Start()
    {
        setupSoundButton();

    }

    private void setupSoundButton()
    {
        buttonSound = GameObject.Find("ButtonSound").GetComponent<Button>();
        soundSprite = buttonSound.transform.Find("Image").GetComponent<Image>();
        if (PlayerPrefHelper.GetSoundSetting())
        {
            soundSprite.sprite = soundOn;
        }
        else
        {
            soundSprite.sprite = soundOff;
        }
    }

    public void onSoundButtonClicked()
    {
		MusicPlayer.getInstance ().handleClickSound ();
		if (PlayerPrefHelper.GetSoundSetting())
		{
			soundSprite.sprite = soundOff;
			PlayerPrefHelper.SaveSoundSetting(false);
			musicPlayer.TurnOffMusic ();
		}
		else
		{
			soundSprite.sprite = soundOn;
			PlayerPrefHelper.SaveSoundSetting(true);
			musicPlayer.TurnOnMusic ();
		}
    }

    // Update is called once per frame
    void Update()
    {

    }


}
