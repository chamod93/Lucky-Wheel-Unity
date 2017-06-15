using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AssemblyCSharp;

public class PanelResultController : MonoBehaviour
{

    [SerializeField]
    private GameObject pannelResult;

    [SerializeField]
    private Button buttonNext;

    [SerializeField]
    private GameObject generalDialog;

    private Color coverColor = Color.black;

    [SerializeField]
    private float showingTime = 0.3f;

    // Use this for initialization
    void Start()
    {
        buttonNext.gameObject.SetActive(false);
        pannelResult.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowFailPopup()
    {
        Text text = buttonNext.transform.Find("Text").GetComponent<Text>();
		text.text = "Retry";
        buttonNext.gameObject.SetActive(true);
        pannelResult.SetActive(true);
    }

    public void ShowCompletePopup(int level)
    {
        Text text = pannelResult.transform.Find("TextResult").GetComponent<Text>();
        text.text = string.Format("Level {0} Completed", level);
		pannelResult.GetComponent<Image>().color =new Color(210, 244, 247, 1);
        pannelResult.SetActive(true);
        if(PlayerPrefHelper.GetPassingStage() <= GameData.LoadFromJSONResource().levelData.Length)
        {
            buttonNext.gameObject.SetActive(true);
        }
        else
        {
            StartCoroutine(ShowCongratulationDialog());
            PlayerPrefHelper.SavePassingStage(PlayerPrefHelper.GetPassingStage() - 1);
        }
    }
    
    private IEnumerator ShowCongratulationDialog()
    {
        generalDialog.SetActive(true);
        GameObject realDialog = generalDialog.transform.GetChild(0).gameObject;
        realDialog.SetActive(true);
        realDialog.transform.localScale = new Vector3(0, 0, 0);
        float t = 0;
        while(t <= 1)
        {
            realDialog.transform.localScale = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(1.1f, 1.1f, 1.1f), t);
            t += Time.deltaTime / showingTime;
            generalDialog.GetComponent<Image>().color = coverColor * t*0.8f;
            yield return null ;
        }
        realDialog.transform.localScale = new Vector3(1, 1, 1);
    }
}
