using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour {

    private MusicPlayer musicPlayer;
    private Text text;
    private string defaultText = "Sound is";

    // Use this for initialization
    void Start () {
        musicPlayer = GameObject.FindObjectOfType<MusicPlayer>().GetComponent<MusicPlayer>();
        text = (GameObject.Find("SoundButton")).GetComponentInChildren<Text>();
        if (MusicPlayer.getInstance().isMusicStopped())
        {
            text.text = defaultText + " Off";
        }
        else
        {
            text.text = defaultText + " On";
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnButtonSoundClicked()
    {
        if (!MusicPlayer.getInstance().isMusicStopped())
        {
            musicPlayer.TurnOffMusic();
            text.text = defaultText + " Off";
        }
        else
        {
            musicPlayer.TurnOnMusic();
            text.text = defaultText + " On";
        }
    }
}
