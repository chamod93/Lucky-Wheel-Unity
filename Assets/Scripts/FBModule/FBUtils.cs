using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;
using UnityEngine.UI;
using AssemblyCSharp;
using UnityEngine.SceneManagement;

public class FBUtils : MonoBehaviour {

    private string userIdNA = "1395101493861";
    string appLinkUrl = "http://recipe-app.com/recipe/grilled-potato-salad";
    string previewImageUrl = "http://img11.deviantart.net/8aff/i/2014/256/7/4/natus_vincere_wallpaper_by_jaredftw-d7z09ni.png";

    Button btnAvatar;

    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
            // Continue with Facebook SDK
            // ...
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    public void Login()
    {

        var perms = new List<string>() { "public_profile", "email", "user_friends" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            Debug.Log("id" +aToken.UserId);
            userIdNA = aToken.UserId;
            PlayerPrefHelper.SaveFacebookId(aToken.UserId);
            // Print current access token's granted permissions
            foreach (string perm in aToken.Permissions)
            {
                Debug.Log(perm);
            }
            var loginParams = new Dictionary<string, object>();
            loginParams[AppEventParameterName.ContentID] = "login";
            loginParams[AppEventParameterName.Description] = "login";
            loginParams[AppEventParameterName.Success] = "1";
            FB.LogAppEvent(AppEventName.ActivatedApp, parameters: loginParams);
            SceneManager.LoadScene("StartScene");
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }

    public void GetAvatar(Button btnAvatar)
    {
        this.btnAvatar = btnAvatar;
        if (!PlayerPrefHelper.GetFacebookId().Equals(""))
        {
            Debug.Log("/v2.9/" + PlayerPrefHelper.GetFacebookId() + "/picture");
            FB.API(PlayerPrefHelper.GetFacebookId() + "/picture", Facebook.Unity.HttpMethod.GET, GetInfoUserCallBack);
        }
    }

    private void GetInfoUserCallBack(IGraphResult result)
    {
        if(result.Error != null)
        {
            Debug.Log("Problem with getting profile picture");
            FB.API("/v2.9/" + userIdNA + "/picture", Facebook.Unity.HttpMethod.GET, GetInfoUserCallBack);
            Debug.Log(result.Error);
            return;
        }
        else
        {
            btnAvatar.GetComponent<Image>().sprite = Sprite.Create(result.Texture, new Rect(0, 0, 50, 50), new Vector2(0, 0));
        }
    }

    public void Share()
    {
        FB.ShareLink(contentTitle: "FB SDK", contentDescription: "Test function share", contentURL: new System.Uri("http://www.gosugamers.net/dota2"),
            photoURL: new System.Uri("http://img11.deviantart.net/8aff/i/2014/256/7/4/natus_vincere_wallpaper_by_jaredftw-d7z09ni.png"),
            callback: OnShare);
    }

    private void OnShare(IShareResult result)
    {
        if (result.Cancelled || !string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("Share Link error " + result.Error);
        }
        else if (!string.IsNullOrEmpty(result.PostId))
        {
            Debug.Log(result.PostId);
        }
        else
        {
            Debug.Log("Share success");
            var loginParams = new Dictionary<string, object>();
            loginParams[AppEventParameterName.ContentID] = "share";
            loginParams[AppEventParameterName.Description] = "share";
            loginParams[AppEventParameterName.Success] = "1";
            FB.LogAppEvent("share", parameters: loginParams);
        }
    }

    public void Invite()
    {
        FB.Mobile.AppInvite(appLinkUrl: new System.Uri(appLinkUrl), previewImageUrl: new System.Uri(previewImageUrl), callback: OnInvite);
    }

    private void OnInvite(IAppInviteResult result)
    {
        if (result.Cancelled || !string.IsNullOrEmpty(result.Error))
        {
            Debug.Log("Share Link error " + result.Error);
        }
        else
        {
            Debug.Log(result.ToString());
        }
    }

    public void LogOut()
    {
        FB.LogOut();
        PlayerPrefHelper.SaveFacebookId("");
        SceneManager.LoadScene("StartScene");
    }

    public void GetImageFacebook()
    {

    }


}
