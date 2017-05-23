using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class MusicPlayer : GenericSingletonClass<MusicPlayer> {
	
	[SerializeField]
	private AudioClip startClip;

	[SerializeField]
	private AudioClip gameClip;

	[SerializeField]
	private AudioClip endClip;

	[SerializeField]
	private AudioClip clickClip;

	[SerializeField]
	private AudioClip explodeClip;

	[SerializeField]
	private AudioClip failClip;

	[SerializeField]
	private AudioClip successClip;

    private AudioSource music;
    private bool stopMusic = false;

    public void TurnOffMusic()
    {
        stopMusic = true;
        music.Stop();
    }

    public void TurnOnMusic()
    {
        stopMusic = false;
        music.loop = true;
        music.Play();
    }

    public static MusicPlayer getInstance()
    {
		return Instance;
    }

    public bool isMusicStopped()
    {
        return stopMusic;
    }

	public void handleClickSound()
	{
		if(PlayerPrefHelper.GetSoundSetting()){
			AudioSource.PlayClipAtPoint (clickClip, new Vector3 ());
		}
	}

	public void handleExplodeSound()
	{
		if(PlayerPrefHelper.GetSoundSetting()){
			AudioSource.PlayClipAtPoint (explodeClip, new Vector3 ());
		}
	}

	public void handleFailSound()
	{
		if(PlayerPrefHelper.GetSoundSetting()){
			AudioSource.PlayClipAtPoint (failClip, new Vector3 ());
		}
	}

	public void handleSuccessSound()
	{
		if(PlayerPrefHelper.GetSoundSetting()){
			AudioSource.PlayClipAtPoint (successClip, new Vector3 ());
		}
	}
		
	public override void Awake(){
		base.Awake ();
		if (Instance.music == null) {
			music = GetComponent<AudioSource> ();
			music.clip = startClip;
			if (PlayerPrefHelper.GetSoundSetting ()) {
				music.loop = true;
				music.Play ();
			}
		} else {
			music = Instance.music;
		}

	}


    private void OnLevelWasLoaded(int level)
    {
		bool needReplay = true;
		if(level == 0 || level == 1)
        {
			if (music.clip != startClip) {
				music.clip = startClip;
			} else {
				needReplay = false;
			}
        }else if(level == 2){
			if (music.clip != gameClip) {
				music.clip = gameClip;
			} else {
				needReplay = false;
			}
        }
        else{
            music.clip = endClip;
        }
		if (!isMusicStopped() && needReplay && PlayerPrefHelper.GetSoundSetting ()) {
			music.loop = true;
			music.Play ();
		}
    }
}
