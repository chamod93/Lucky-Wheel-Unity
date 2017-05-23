using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class StageSceneController : MonoBehaviour
{
    private static readonly int MAX_BUTTON_PER_PAGE = 10;

    [SerializeField]
    private Sprite hexaUnclocked;

    [SerializeField]
    private Sprite hexaCurrent;

    [SerializeField]
    private Sprite hexaLocked;

    private GameData data;
    private ArrayList buttons = new ArrayList();
    private Button buttonPreviousPage;
    private Button buttonNextPage;

    private int currentStage;
    private int maxPage;
    private int currentPage;
    private int startPageStage;

    // Use this for initialization
    void Start()
    {
        initVariables();
        setupButtons();
        setupPagesNavigator();
        setupStages();
    }

    private void initVariables()
    {
        data = GameData.LoadFromJSONResource();
        currentStage = PlayerPrefHelper.GetCurrentStage();
        maxPage = (data.levelData.Length - 1) / MAX_BUTTON_PER_PAGE;
        currentPage = currentStage / MAX_BUTTON_PER_PAGE;
        startPageStage = currentPage * MAX_BUTTON_PER_PAGE + 1;
    }

    private void setupButtons()
    {
        buttonPreviousPage = GameObject.Find("ButtonPreviousPage").GetComponent<Button>();
        buttonNextPage = GameObject.Find("ButtonNextPage").GetComponent<Button>();
        for (int i = 1; i <= MAX_BUTTON_PER_PAGE; i++)
        {
            String name = "ButtonStage" + i;
            Button button = (GameObject.Find(name).GetComponent<Button>());
            buttons.Add(button);
        }
    }


    private void setupPagesNavigator()
    {

        buttonNextPage.onClick.AddListener(delegate ()
        {
				MusicPlayer.getInstance().handleClickSound();
            nextPage();
        });
        buttonPreviousPage.onClick.AddListener(delegate () { 
			previousPage(); 
			MusicPlayer.getInstance().handleClickSound();
		});
        showOrHidePagesNavigator();

    }

    public void nextPage()
    {
        currentPage = currentPage + 1;
        showOrHidePagesNavigator();
    }

    public void previousPage()
    {
        currentPage = currentPage - 1;
        showOrHidePagesNavigator();
    }

    private void showOrHidePagesNavigator()
    {
        if (currentPage == 0)
        {
            buttonPreviousPage.gameObject.SetActive(false);
        }
        else
        {
            buttonPreviousPage.gameObject.SetActive(true);
        }


        if (currentPage == maxPage)
        {
            buttonNextPage.gameObject.SetActive(false);
        }
        else
        {
            buttonNextPage.gameObject.SetActive(true);
        }
    }

    private void setupStages()
    {
        int i = startPageStage;
        foreach (Button button in buttons)
        {
            if (i > data.levelData.Length)
            {
                button.gameObject.SetActive(false);
            }
            else
            {
                button.onClick.AddListener(delegate () { 
					MusicPlayer.getInstance().handleClickSound();
					StartGame(i);
				});
                button.transform.Find("Text").GetComponent<Text>().text = i.ToString();
                button.gameObject.SetActive(true);
                setSpriteForImage(button, i);
            }
            i = i + 1;
        }
    }

    private void setSpriteForImage(Button button, int i)
    {
        Text text = button.transform.Find("Text").GetComponent<Text>();
        text.color = new Color(210, 244, 247);
        if (i < currentStage)
        {
            button.enabled = true;
            button.image.sprite = hexaUnclocked;
        }
        else if (i == currentStage)
        {
            button.enabled = true;
            button.image.sprite = hexaCurrent;
        }
        else
        {
            button.enabled = false;
            button.image.sprite = hexaLocked;
            text.color = new Vector4(210, 244, 247, 10f);
        }
    }

    private void StartGame(int level)
    {
        SceneManager.LoadScene("GameScene");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
