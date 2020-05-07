using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour
{

    public static MainMenuController instance;

    [SerializeField]
    private GameObject fader;

    [SerializeField]
    private GameObject arrow, messagePanel, quitAlertPanel, creditPanel, settingPanel, sharePanel, storePanel, helpPanel, achievementsPanel , freeLifePanel ;

    [SerializeField]
    private GameObject[] panelsToDeactive;

    [SerializeField]
    private GameObject deactiveSprite;

    [SerializeField]
    private Text infoText,maxScore, lives;

    [SerializeField]
    private Text gatheredBlueBirds, gatheredGreenBirds, gatheredOrangeBirds, gatheredPurpleBirds, gatheredRedBirds, gatheredYellowBirds;

    [SerializeField]
    private Text blueBirdsShouldGathered, greenBirdsShouldGathered, orangeBirdsShouldGathered, purpleBirdsShouldGathered, redBirdsShouldGathered, yellowBirdsShouldGathered;

    [SerializeField]
    private GameObject BlueBirdFeather, GreenBirdFeather, OrangeBirdFeather, PurpleBirdFeather, RedBirdFeather, YellowBirdFeather;


    [SerializeField]
    private AnimationClip arrowClip;

    /*
    [SerializeField]
    private Slider numberOfTurnForBirdSlider , movePipe , pipeMoveSpeed;
    [SerializeField]
    private Text numberOfTurnForBirdText , pipeMoveRangeText , pipeMoveSpeedText;
    */

    private long currentLives, currentScores;

    private float pipeSpeed;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        infoText.text =
                        "Perky Birds"
                        + System.Environment.NewLine + System.Environment.NewLine +
                        "v " + Application.version.ToString()
                        + System.Environment.NewLine + System.Environment.NewLine +
                        "2016"
                        + System.Environment.NewLine + System.Environment.NewLine +
                        "All rights reserved(®)";
        
        Time.timeScale = 1.0f;

        LoadAllValues();
        SetNumbersToUI();
        ShowFeather();

        PrepareSoundButton();

        /*
        GameController.instance.numberOfTurnForBird = 1;

        numberOfTurnForBirdSlider.onValueChanged.AddListener((value) =>
        {
            numberOfTurnForBirdText.text = value.ToString();

            GameController.instance.numberOfTurnForBird = Mathf.FloorToInt(value);
        });
        */


        //
        //
        //      FOR DISABLE LIFE SYSTEM
        //
        //
        //currentLives = GameController.instance.lives = 30;
        //
        //
        //      FOR DISABLE LIFE SYSTEM
        //
        //

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadAllValues()
    {
        currentScores = GameController.instance.maxScore;
        currentLives = GameController.instance.lives;


    }

    public void SaveAllValues()
    {
        GameController.instance.lives = currentLives;
        if (GameController.instance.maxScore < currentScores)
        {
            GameController.instance.maxScore = currentScores;
        }
        GameController.instance.Save();
    }
    public void SetNumbersToUI()
    {
        maxScore.text = currentScores.ToString();
        //lives.text = currentLives.ToString();
        lives.text = (currentLives-1268).ToString();

        gatheredBlueBirds.text = GameController.instance.gatheredBlueBirds.ToString();
        gatheredGreenBirds.text = GameController.instance.gatheredGreenBirds.ToString();
        gatheredOrangeBirds.text = GameController.instance.gatheredOrangeBirds.ToString();
        gatheredPurpleBirds.text = GameController.instance.gatheredPurpleBirds.ToString();
        gatheredRedBirds.text = GameController.instance.gatheredRedBirds.ToString();
        gatheredYellowBirds.text = GameController.instance.gatheredYellowBirds.ToString();


        blueBirdsShouldGathered.text = GameController.instance.blueBirdsShouldGathered.ToString();
            greenBirdsShouldGathered.text = GameController.instance.greenBirdsShouldGathered.ToString();
        orangeBirdsShouldGathered.text = GameController.instance.orangeBirdsShouldGathered.ToString();
        purpleBirdsShouldGathered.text = GameController.instance.purpleBirdsShouldGathered.ToString();
        redBirdsShouldGathered.text = GameController.instance.redBirdsShouldGathered.ToString();
        yellowBirdsShouldGathered.text = GameController.instance.yellowBirdsShouldGathered.ToString();

    }


    public void OpenSurvivalLevel()
    {
        foreach(var pnl in panelsToDeactive)
        {
            pnl.SetActive(false);
        }
        MusicController.instance.PlayButtonClip();
        StartCoroutine(GoToLevel("SurvivalLevel1"));
    }



    public void PlayGame()
    {

        if (currentLives > 0)
        {
            MusicController.instance.PlayButtonClip();
            StartCoroutine(GoToLevel("GameScene"));
            //MusicController.instance.LevelIsLoadedTurnOfMusic();

        }
        else
        {

            StartCoroutine(ShowArrow(5));
            //OpenMessagePanel();
            OpenFreeLifePanel();
        }
    }


    IEnumerator GoToLevel(string name)
    {
        //MusicController.instance.LevelIsLoadedTurnOfMusic();
        //float fadeTime = fader.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(0);
        LoadingController.instance.LoadLevel(name);

    }

    public IEnumerator BeginFade(bool value, float addedTime)
    {
        yield return new WaitForSeconds(addedTime);
        float fadeTime = fader.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        print("In Fade");
        //LoadingController.instance.isFadeDone = true;
        LoadingController.instance.SetAllowSceneActivationTrue();
    }

    IEnumerator QuitGameWithFade()
    {
        float fadeTime = fader.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        Application.Quit();
    }



    public void QuitGame()
    {
        // GameController.instance.maxScore = 0;
        // GameController.instance.lives = 0;

        GameController.instance.Save();
        StartCoroutine(QuitGameWithFade());
    }

    IEnumerator ShowArrow(int time)
    {

        arrow.SetActive(true);
        float animLenght = arrowClip.length;
        yield return new WaitForSeconds(time * animLenght);
        arrow.SetActive(false);
        //print(arrowClip.length);
        //print(time * animLenght);

    }

    public void ShareButton()
    {
        MusicController.instance.PlayButtonClip();
        NativeShare.instance.ShareScreenshotWithText("Save the little Perky Bird. Download the game for free " + System.Environment.NewLine + "https://play.google.com/store/apps/details?id=com.parsveda.PerkyBirds");
    }


    void PrepareSoundButton()
    {
        if (GameController.instance.isMusicOn)
        {
            deactiveSprite.SetActive(false);
        }
        else
        {
            deactiveSprite.SetActive(true);
        }
    }

    public void SoundButton()
    {
        //MusicController.instance.PlayButtonClip();

        if (GameController.instance.isMusicOn)
        {
            MusicController.instance.StopBGMusic();

            //MusicController.instance.LevelIsLoadedTurnOfMusic();
            GameController.instance.isMusicOn = false;
            deactiveSprite.SetActive(true);
        }
        else
        {
            MusicController.instance.PlayBGMusic();
            MusicController.instance.ForcePlayButtonClip();
            GameController.instance.isMusicOn = true;
            deactiveSprite.SetActive(false);

        }

        GameController.instance.Save();
    }

    private void ShowFeather()
    {

        if (GameController.instance.gatheredBlueBirds >= GameController.instance.blueBirdsShouldGathered)
        {
            BlueBirdFeather.GetComponent<SpriteRenderer>().color = Color.white;
            //print("done Blue GOOOOOOOOOOOOOTED");
        }

        if (GameController.instance.gatheredGreenBirds >= GameController.instance.greenBirdsShouldGathered)
        {
            GreenBirdFeather.GetComponent<SpriteRenderer>().color = Color.white;
            //print("done Green GOOOOOOOOOOOOOTED");
        }

        if(GameController.instance.gatheredOrangeBirds >= GameController.instance.orangeBirdsShouldGathered)
        {
            OrangeBirdFeather.GetComponent<SpriteRenderer>().color = Color.white;
            //print("done Orange GOOOOOOOOOOOOOTED");
        }

        if (GameController.instance.gatheredPurpleBirds >= GameController.instance.purpleBirdsShouldGathered)
        {
            PurpleBirdFeather.GetComponent<SpriteRenderer>().color = Color.white;
            //print("done Purple GOOOOOOOOOOOOOTED");
        }
        
        if (GameController.instance.gatheredRedBirds >= GameController.instance.redBirdsShouldGathered)
        {
            RedBirdFeather.GetComponent<SpriteRenderer>().color = Color.white;
            //print("done Red GOOOOOOOOOOOOOTED");
        }

        if (GameController.instance.gatheredYellowBirds >= GameController.instance.yellowBirdsShouldGathered)
        {
            YellowBirdFeather.GetComponent<SpriteRenderer>().color = Color.white;
            //print("done Yellow GOOOOOOOOOOOOOTED");
        }



    }

    public void OpenWorldScene()
    {
        StartCoroutine(OpenSceneByFade("WorldsScene"));
    }

    public void GooglePlayButton()
    {
        NativeShare.instance.OpenMarket();
    }


    public IEnumerator OpenSceneByFade(string name)
    {
        MusicController.instance.PlayButtonClip();
        float fadeTime = fader.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(name);
    }

    #region PANELS
    public void OpenMessagePanel()
    {
        MusicController.instance.PlayButtonClip();
        // start close Other Panels
        //messagePanel.SetActive(false);
        quitAlertPanel.SetActive(false);
        creditPanel.SetActive(false);
        settingPanel.SetActive(false);
        sharePanel.SetActive(false);
        storePanel.SetActive(false);
        helpPanel.SetActive(false);
        achievementsPanel.SetActive(false);
        freeLifePanel.SetActive(false);
        // end close Other

        messagePanel.SetActive(true);
    }

    public void CloseMessagePanel()
    {
        MusicController.instance.PlayCloseClip();
        messagePanel.SetActive(false);
    }

    public void OpenQuitAlertPanel()
    {
        MusicController.instance.PlayButtonClip();
        // start close Other Panels
        messagePanel.SetActive(false);
        //quitAlertPanel.SetActive(false);
        creditPanel.SetActive(false);
        settingPanel.SetActive(false);
        sharePanel.SetActive(false);
        storePanel.SetActive(false);
        helpPanel.SetActive(false);
        achievementsPanel.SetActive(false);
        freeLifePanel.SetActive(false);

        // end close Other

        quitAlertPanel.SetActive(true);
    }

    public void CloseQuitAlertPanel()
    {
        MusicController.instance.PlayCloseClip();
        quitAlertPanel.SetActive(false);
    }

    public void OpenCreditPanel()
    {
        MusicController.instance.PlayButtonClip();
        // start close Other Panels
        messagePanel.SetActive(false);
        quitAlertPanel.SetActive(false);
        //creditPanel.SetActive(false);
        settingPanel.SetActive(false);
        sharePanel.SetActive(false);
        storePanel.SetActive(false);
        helpPanel.SetActive(false);
        achievementsPanel.SetActive(false);
        freeLifePanel.SetActive(false);

        // end close Other

        creditPanel.SetActive(true);
    }

    public void CloseCreditPanel()
    {
        MusicController.instance.PlayCloseClip();
        creditPanel.SetActive(false);
        settingPanel.SetActive(true);
    }




    public void OpenSettingPanel()
    {
        MusicController.instance.PlayButtonClip();
        // start close Other Panels
        messagePanel.SetActive(false);
        quitAlertPanel.SetActive(false);
        creditPanel.SetActive(false);
        //settingPanel.SetActive(false);
        sharePanel.SetActive(false);
        storePanel.SetActive(false);
        helpPanel.SetActive(false);
        achievementsPanel.SetActive(false);
        freeLifePanel.SetActive(false);

        // end close Other

        settingPanel.SetActive(true);
    }

    public void CloseSettingPanel()
    {
        MusicController.instance.PlayCloseClip();
        settingPanel.SetActive(false);
    }

    public void OpenSharePanel()
    {
        MusicController.instance.PlayButtonClip();
        // start close Other Panels
        messagePanel.SetActive(false);
        quitAlertPanel.SetActive(false);
        creditPanel.SetActive(false);
        settingPanel.SetActive(false);
        //sharePanel.SetActive(false);
        storePanel.SetActive(false);
        helpPanel.SetActive(false);
        achievementsPanel.SetActive(false);
        freeLifePanel.SetActive(false);

        // end close Other

        sharePanel.SetActive(true);
    }

    public void CloseSharePanel()
    {

        MusicController.instance.PlayCloseClip();
        sharePanel.SetActive(false);
    }



    public void OpenStorePanel()
    {
        MusicController.instance.PlayButtonClip();
        // start close Other Panels
        messagePanel.SetActive(false);
        quitAlertPanel.SetActive(false);
        creditPanel.SetActive(false);
        settingPanel.SetActive(false);
        sharePanel.SetActive(false);
        //storePanel.SetActive(false);
        helpPanel.SetActive(false);
        achievementsPanel.SetActive(false);
        freeLifePanel.SetActive(false);

        // end close Other

        storePanel.SetActive(true);
    }

    public void CloseStorePanel()
    {

        MusicController.instance.PlayCloseClip();
        storePanel.SetActive(false);
        messagePanel.SetActive(true);
    }

    public void OpenHelpPanel()
    {
        MusicController.instance.PlayButtonClip();
        // start close Other Panels
        messagePanel.SetActive(false);
        quitAlertPanel.SetActive(false);
        creditPanel.SetActive(false);
        settingPanel.SetActive(false);
        sharePanel.SetActive(false);
        storePanel.SetActive(false);
        //helpPanel.SetActive(false);
        achievementsPanel.SetActive(false);
        freeLifePanel.SetActive(false);
        // end close Other
        helpPanel.SetActive(true);

    }

    public void CloseHelpPanel()
    {
        MusicController.instance.PlayCloseClip();
        helpPanel.SetActive(false);
        settingPanel.SetActive(true);
    }



    public void OpenAchievementsPanel()
    {
        MusicController.instance.PlayButtonClip();
        // start close Other Panels
        messagePanel.SetActive(false);
        quitAlertPanel.SetActive(false);
        creditPanel.SetActive(false);
        settingPanel.SetActive(false);
        sharePanel.SetActive(false);
        storePanel.SetActive(false);
        helpPanel.SetActive(false);
        freeLifePanel.SetActive(false);

        // end close Other

        achievementsPanel.SetActive(true);
    }

    public void CloseAchievementsPanel()
    {
        MusicController.instance.PlayCloseClip();
        achievementsPanel.SetActive(false);
    }

    public void OpenFreeLifePanel()
    {
        MusicController.instance.PlayButtonClip();
        // start close Other Panels
        messagePanel.SetActive(false);
        quitAlertPanel.SetActive(false);
        creditPanel.SetActive(false);
        settingPanel.SetActive(false);
        sharePanel.SetActive(false);
        storePanel.SetActive(false);
        helpPanel.SetActive(false);
        achievementsPanel.SetActive(false);
        //freeLifePanel.SetActive(false);
        // end close Other

        freeLifePanel.SetActive(true);
    }

    public void CloseFreeLifePanel()
    {
        MusicController.instance.PlayCloseClip();
        freeLifePanel.SetActive(false);
        //messagePanel.SetActive(true);
    }

    public void OpenLeaderbordsPanel()
    {
        LeaderboardsController.instance.OpenLeaderboardsScore();
    }


    #endregion



}
