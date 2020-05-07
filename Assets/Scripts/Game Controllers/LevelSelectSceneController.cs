using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LevelSelectSceneController : MonoBehaviour {


    public static LevelSelectSceneController instance;

    [SerializeField]
    private GameObject  fader , freeLifePanel , panelForAvoidTouchAfterLoadScene;

    [SerializeField]
    private GameObject BG_1, BG_2, BG_3, BG_4;

    [SerializeField]
    private Text LivesText;

    [SerializeField]
    private GameObject[] panelsToDeactive;

    private long lives;

    void Awake()
    {
        CreateInstance();
    }


    // Use this for initialization
    void Start()
    {

        lives = GameController.instance.lives;
        SetNumbersToUI();

        BG_1.SetActive(false);
        BG_2.SetActive(false);
        BG_3.SetActive(false);
        BG_4.SetActive(false);

        //print(GameController.instance.selectedWorld);
        switch (GameController.instance.selectedWorld)
        {
            case 1:
                BG_1.SetActive(true);
                break;
            case 2:
                BG_2.SetActive(true);
                break;
            case 3:
                BG_3.SetActive(true);
                break;
            case 4:
                BG_4.SetActive(true);
                break;

            default:
                BG_1.SetActive(true);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print(" S "+GameController.instance.speed);
    }

    private void CreateInstance()
    {
        if (instance == null)
            instance = this;
    }

    public void CheatButton()
    {
        List<LevelManager.Level> levels = new List<LevelManager.Level>();
        levels.AddRange(GameController.instance.levels);

        foreach (var level in levels)
        {
            if (level.id <= 30)
            {
                if (!level.unlocked)
                {
                    level.unlocked = true;
                    GameController.instance.Save();
                    RestartCurrntScene();
                    break;
                }
            }

        }

        //levels.Find(foundLevel => foundLevel.id == levelId + 1).unlocked = true;

        //GameController.instance.Save();
    }

    public void OpenWorldScene()
    {
        panelForAvoidTouchAfterLoadScene.SetActive(true);
        //DeactiveAllPanels();
        StartCoroutine(OpenSceneByFade("WorldsScene"));
    }

    void RestartCurrntScene()
    {
        panelForAvoidTouchAfterLoadScene.SetActive(true);
        //DeactiveAllPanels();
        StartCoroutine(OpenSceneByFade(SceneManager.GetActiveScene().name));
    }

    public void OpenMainMenuScene()
    {
        panelForAvoidTouchAfterLoadScene.SetActive(true);
        //DeactiveAllPanels();
        StartCoroutine(OpenSceneByFade("MainMenu"));
    }

    public void OpenScene(string levelName, bool byLoading)
    {
        panelForAvoidTouchAfterLoadScene.SetActive(true);
        //UICanvas.SetActive(false);
        MusicController.instance.PlayButtonClip();
        StartCoroutine(GoToLevel(levelName, byLoading));
    }

    public void OpenSurvivalLevel()
    {
        foreach (var pnl in panelsToDeactive)
        {
            pnl.SetActive(false);
        }
        MusicController.instance.PlayButtonClip();
        StartCoroutine(GoToLevel("SurvivalLevel1", true));
    }

    IEnumerator GoToLevel(string name, bool byLoading)
    {
        DeactiveAllPanels();

        if (byLoading)
        {
            yield return new WaitForSeconds(0);
            LoadingController.instance.LoadLevel(name);
        }
        else
        {
            float fadeTime = fader.GetComponent<Fading>().BeginFade(1);
            yield return StartCoroutine(MyCoroutine.WaitForRealSeconds((fadeTime)));
            SceneManager.LoadScene(name);
        }

    }

    public IEnumerator BeginFade(bool value, float addedTime)
    {
        //print("begin Fade");
        yield return new WaitForSeconds(addedTime);
        float fadeTime = fader.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        LoadingController.instance.SetAllowSceneActivationTrue();
    }


    public IEnumerator OpenSceneByFade(string name)
    {
        MusicController.instance.PlayButtonClip();
        float fadeTime = fader.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(name);
    }

    void DeactiveAllPanels()
    {
        foreach(var obj in panelsToDeactive)
        {
            obj.SetActive(false);
        }
    }

    public void OpenFreeLifePanel()
    {
        MusicController.instance.PlayButtonClip();
        freeLifePanel.SetActive(true);
    }
    public void CloseFreeLifePanel()
    {
        MusicController.instance.PlayButtonClip();
        freeLifePanel.SetActive(false);
    }

    public void LoadAllValues()
    {
        lives = GameController.instance.lives;
    }
    public void SetNumbersToUI()
    {
        //LivesText.text = lives.ToString();
        LivesText.text = (lives-1268).ToString();
    }
}

