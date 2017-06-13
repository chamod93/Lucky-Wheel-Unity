using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AssemblyCSharp;

public class StartSceneController : MonoBehaviour {

    [SerializeField]
    GameObject fbPanel;

    [SerializeField]
    Button btnFb;

    [SerializeField]
    FBUtils fbUtils;

    [SerializeField]
    Button btnAva;

	// Use this for initialization
	void Start () {
        Init();
	}

    private void Init()
    {
        AddListenerForBtnFacebook();
    }

    private void AddListenerForBtnFacebook()
    {
        Debug.Log("aaa" + PlayerPrefHelper.GetFacebookId());
        if(PlayerPrefHelper.GetFacebookId() == "")
        {
            btnFb.onClick.AddListener(delegate ()
            {
                fbUtils.Login();
            });
        }
        else
        {
            btnFb.onClick.AddListener(delegate ()
            {
                ShowFacebookPanel();
                fbUtils.GetAvatar(btnAva);
            });
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ShowFacebookPanel()
    {
        fbPanel.SetActive(true);
    }

    public void HideFacebookPanel()
    {
        fbPanel.SetActive(false);
    }
}
