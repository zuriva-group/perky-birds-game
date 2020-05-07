using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SurvivalLevelSelectSceneController : MonoBehaviour
{
    public static SurvivalLevelSelectSceneController instance;


    [SerializeField]
    private GameObject fader , freeLifePanel, panelForAvoidTouchAfterLoadScene;

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
    }

    // Update is called once per frame
    void Update()
    {

    }
    void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void OpenSurvivalScene(int number)
    {
        MusicController.instance.PlayButtonClip();
        StartCoroutine(GoToLevel("SurvivalLevel"+number, true));
    }

    public void BackButton()
    {
        StartCoroutine(GoToLevel("WorldsScene", false));
    }

    IEnumerator GoToLevel(string name, bool byLoading)
    {
        panelForAvoidTouchAfterLoadScene.SetActive(true);

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

    void DeactiveAllPanels()
    {
        foreach (var obj in panelsToDeactive)
        {
            obj.SetActive(false);
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
