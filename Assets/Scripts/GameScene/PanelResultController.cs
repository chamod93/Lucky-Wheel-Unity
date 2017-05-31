using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelResultController : MonoBehaviour
{

    [SerializeField]
    private GameObject pannelResult;

    [SerializeField]
    private Button buttonNext;

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
        text.text = "Replay";
        buttonNext.gameObject.SetActive(true);
        pannelResult.SetActive(true);
    }

    public void ShowCompletePopup(int level)
    {
        Image bg = pannelResult.GetComponent<Image>();
        Text text = pannelResult.transform.Find("TextResult").GetComponent<Text>();
        text.text = string.Format("Level {0} Completed", level);
        bg.color = new Color(210, 244, 247);
        pannelResult.SetActive(true);
    }
}
