using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class WorldSceneController : MonoBehaviour
{

    public static WorldSceneController instance;

    [SerializeField]
    private GameObject fader , freeLifePanel,commingSoonPanel, panelForAvoidTouchAfterLoadScene;

    //[SerializeField]
    //private Animator commingSoonPanelAnim;

    [SerializeField]
    private Button world2Button, world3Button, world4Button;

    [SerializeField]
    private Sprite activeWorld2ButtonImage, activeWorld3ButtonImage, activeWorld4ButtonImage;

    [SerializeField]
    private GameObject TopWorld1, TopWorld2, TopWorld3, TopWorld4;

    [SerializeField]
    private Text World1ScoresText, World2ScoresText, World3ScoresText, World4ScoresText, WorldSurvivalScoresText;

    [SerializeField]
    private Text LivesText;

    [HideInInspector]
    public Animator comingSoonAnim;


    [SerializeField]
    private GameObject[] panelsToDeactive;

    private long lives;

    void Awake()
    {
        CreateInstance();
    }
    void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        GameController.instance.selectedWorld = 1;

        lives = GameController.instance.lives;
        SetNumbersToUI();

        int lastUnlockedLevel = 0;
        int world1Stars, world2Stars, world3Stars, world4Stars;

        world1Stars = world2Stars = world3Stars = world4Stars = 0;

        foreach (var level in GameController.instance.levels)
        {
            if (level.unlocked)
            {
                lastUnlockedLevel++;
            }

            //print(level.id);

            if (level.id < 16)
            {
                TopWorld1.SetActive(true);
                world1Stars += CalculateStars(level.progress);
            }
            else if (level.id >= 16 && level.id < 31)
            {
                TopWorld2.SetActive(true);
                world2Stars += CalculateStars(level.progress);
            }
            
            else if (level.id >= 31 && level.id < 46)
            {
                TopWorld3.SetActive(true);
                world3Stars += CalculateStars(level.progress);
            }

            else if (level.id >= 46 && level.id < 61)
            {
                TopWorld4.SetActive(true);
                world4Stars += CalculateStars(level.progress);
            }

        }

        World1ScoresText.text = world1Stars + " Of 45";
        World2ScoresText.text = world2Stars + " Of 45";
        World3ScoresText.text = world3Stars + " Of 45";

        //print(lastUnlockedLevel);

        TopWorld2.SetActive(false);
        TopWorld3.SetActive(false);
        TopWorld4.SetActive(false);

        if (lastUnlockedLevel > 15 && lastUnlockedLevel < 31)
        {
            TopWorld2.SetActive(true);

            world2Button.interactable = true;
            world2Button.image.sprite = activeWorld2ButtonImage;
        }
        if (lastUnlockedLevel > 30 && lastUnlockedLevel < 46)
        {
            TopWorld3.SetActive(true);

            world3Button.interactable = true;
            world3Button.image.sprite = activeWorld3ButtonImage;
        }
        if (lastUnlockedLevel > 45 && lastUnlockedLevel < 61)
        {
            TopWorld4.SetActive(true);

            world4Button.interactable = true;
            world4Button.image.sprite = activeWorld4ButtonImage;
        }

        WorldSurvivalScoresText.text = "Collect " + System.Environment.NewLine +
                                        GameController.instance.maxScore.ToString() + " Birds";
    }

    // Update is called once per frame
    void Update()
    {

    }

    private int CalculateStars(int value)
    {
        if (value == 100)
        {
            return 3;
        }
        else if (value >= 85)
        {
            return 2;
        }
        else if (value >= 50)
        {
            return 1;
        }
        return 0;
    }

    public void OpenSurvivalLevel()
    {
        foreach (var pnl in panelsToDeactive)
        {
            pnl.SetActive(false);
        }
        MusicController.instance.PlayButtonClip();
        StartCoroutine(GoToLevel("SurvivalLevel1" , true));
    }

    public void OpenLevelSelectScene(int number)
    {
        GameController.instance.selectedWorld = number;
        //SceneManager.LoadScene("LevelSelectScene");
        StartCoroutine(OpenSceneByFade("LevelSelectScene"));

    }

    public void OpenSurvivalLevelSelectScene()
    {
        //GameController.instance.selectedWorld = number;
        //SceneManager.LoadScene("LevelSelectScene");
        //StartCoroutine(OpenSceneByFade("SurvivalLevelSelectScene"));

        StartCoroutine(GoToLevel("SurvivalLevel1", true));

    }

    void DeactiveAllPanels()
    {
        foreach (var obj in panelsToDeactive)
        {
            obj.SetActive(false);
        }
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

    public IEnumerator BeginFade(bool value, float addedTime)
    {
        //print("begin Fade");
        yield return new WaitForSeconds(addedTime);
        float fadeTime = fader.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        LoadingController.instance.SetAllowSceneActivationTrue();
    }

    public void OpenMainMenu()
    {
        StartCoroutine(OpenSceneByFade("MainMenu"));
        //SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator OpenSceneByFade(string name)
    {
        panelForAvoidTouchAfterLoadScene.SetActive(true);

        MusicController.instance.PlayButtonClip();
        float fadeTime = fader.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(name);
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

    public void ComingSoonButton()
    {
        StartCoroutine(PlayComingSoonAnimation());
    }

    IEnumerator PlayComingSoonAnimation()
    {
        commingSoonPanel.SetActive(true);

        yield return new WaitForSeconds(1);
        commingSoonPanel.GetComponent<Animator>().Play("Up");
        yield return new WaitForSeconds(1);
        commingSoonPanel.SetActive(false);

    }
}

