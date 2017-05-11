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
        setSpriteForSoundButton();
    }

    private void setSpriteForSoundButton()
    {
        if (PlayerPrefHelper.GetSoundSetting())
        {
            soundSprite.sprite = soundOff;
            PlayerPrefHelper.SaveSoundSetting(false);
        }
        else
        {
            soundSprite.sprite = soundOn;
            PlayerPrefHelper.SaveSoundSetting(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


}
