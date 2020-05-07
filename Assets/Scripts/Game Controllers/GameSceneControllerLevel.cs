using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameSceneControllerLevel : MonoBehaviour
{
    public static GameSceneControllerLevel instance;

    [SerializeField]
    private bool lastLevel , openNextWorld;

    [HideInInspector]
    public bool isBirdTurnedYet;


    //[SerializeField]
    //private BirdScript.Bird[] bird;

    [SerializeField]
    private GameObject[] bird;

    private int birdIndex , lostBirdsCount;

    //[SerializeField]
    //private PipeScript.Pipe[] pipe;

    [SerializeField]
    private GameObject[] pipe;

    [SerializeField]
    private GameObject[] targets;

    public IDictionary<GameObject, bool> targetBirds;

    private int gatteredTarget;

    private int pipeIndex;

    //[SerializeField]
    //private GameObject birdsAndPipesForLevelDesign;

    [SerializeField]
    private GameObject hiddenGround;

    //[SerializeField]
    //private GameObject leftDirectionChanger, rightDirectionChanger;

    //[SerializeField]
    //private Text levelText;

    [SerializeField]
    private GameObject[] birdsOnScreen, birdsBGOnScreen, checkMarkImage , failMarkImage;


    [SerializeField]
    private GameObject[] targetBirdsOnScreen, targetBirdsBGOnScreen, targetCheckMarkImage, targetFailMarkImage;


    [SerializeField]
    private GameObject  pausedPanel, quitAlertPanel,freeLifePanel, levelFinishedPanel , deadPanelHaveLife , deadPanelDontHaveLife, pauseButton, nextLevelButton, restartButtonInPausePanel, muteButton, firstStar, secondStar, thirdStar;

    //[SerializeField]
    //private GameObject touchPanel;

    [SerializeField]
    private GameObject fader;


    [SerializeField]
    private Text livesText, levelText, progressText;

    private int levelId;


    [SerializeField]
    private int progress;

    [SerializeField]
    private GameObject starsPanel;

    private static long deadCount;

    private bool screenTouched, screenUnTouched, gameClosed ,lostBird;

    

    [SerializeField]
    private GameObject[] panelsToDeactive;

    private long currentLives;

    void Awake()
    {
        CreateInstance();
        //currentScore = GameController.instance.currentScore;
    }

    // Use this for initialization
    void Start()
    {

        //numberOfTurnForBirdText.gameObject.SetActive(false);

        foreach (var birdd in bird)
        {
            birdd.SetActive(false);
        }
        foreach(var pipee in pipe)
        {
            pipee.SetActive(false);
        }

        targetBirds = new Dictionary<GameObject, bool>();
        foreach (var trgt in targets)
        {
            targetBirds.Add(trgt, false);
        }

        gatteredTarget=0;


        lostBird = false;

        Time.timeScale = 1.0f;

        currentLives = GameController.instance.lives;
        
        if (currentLives > 1268)
        {

            //birdsAndPipesForLevelDesign.SetActive(false);


            for (int i = 0; i < bird.Length; i++)
            {

                birdsOnScreen[i].GetComponent<Image>().sprite = bird[i].gameObject.GetComponent<SpriteRenderer>().sprite;
                birdsOnScreen[i].gameObject.SetActive(true);

                birdsBGOnScreen[i].gameObject.SetActive(true);
            }

            for (int i = 0; i < targets.Length; i++)
            {

                targetBirdsOnScreen[i].GetComponent<Image>().sprite = targets[i].gameObject.GetComponent<SpriteRenderer>().sprite;
                targetBirdsOnScreen[i].gameObject.SetActive(true);

                targetBirdsBGOnScreen[i].gameObject.SetActive(true);
            }

            birdIndex = 0;
            pipeIndex = 0;
            progress = 0;
            lostBirdsCount = 0;

            gameClosed = false;

            SetNumbersToUI();

            levelId = int.Parse(SceneManager.GetActiveScene().name.Remove(0, 5));

            MakeBird();

            MakePipe();

        }

        else
        {
            OpenFreeLifePanel();
        }


        if (currentLives > 1268)
        {
            currentLives--;
        }




        SetNumbersToUI();
        PrepareSoundButton();
    }



    // Update is called once per frame
    void Update()
    {
        //print(progress);
    }

    void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private bool MakeRandomTrueOrFalse()
    {
        // MAKE Random true or false
        bool boolean = (Random.value > 0.5f);
        return boolean;
    }

    private void MakeBird()
    {

        bird[birdIndex].SetActive(true);
        bird[birdIndex].GetComponent<BirdScript>().StartFly() ;

        //GameObject brd;

        //isBirdTurnedYet = false;

        ////Vector3 coordinates = new Vector3(bird[birdIndex].location.transform.position.x,
        ////                                  bird[birdIndex].location.transform.position.y,
        ////                                  bird[birdIndex].location.transform.position.z);


        //Vector3 coordinates = new Vector3(bird[birdIndex].transform.position.x,
        //                                  bird[birdIndex].transform.position.y,
        //                                  bird[birdIndex].transform.position.z);

        //brd = Instantiate(bird[birdIndex], coordinates, Quaternion.identity) as GameObject;

        ////brd.GetComponent<BirdScript>().SetNumberOfTurnForBird(bird[birdIndex].turnCount);

        //ActiveHiddenGround(coordinates);


        ActiveHiddenGround(bird[birdIndex].transform.position);
        //bird[birdIndex]

        birdIndex++;
    }

    void ActiveHiddenGround(Vector3 pos)
    {
        pos.x = 0f;
        pos.y -= 0.1f;

        hiddenGround.transform.position = pos;
        hiddenGround.SetActive(true);
        //print("YESSSSSSSSSS");


        //hiddenGround.GetComponent<HiddenGroundScript>().StartVerticalMove(hiddenGround.transform.position);


    }


    //
    // HiddenGround is a Collider  bellow the bird
    //
    void DeactiveHiddenGround()
    {
        hiddenGround.SetActive(false);
    }

    void DeactiveDirectionChangers()
    {
        //leftDirectionChanger.SetActive(false);
        //rightDirectionChanger.SetActive(false);
    }

    void ActiveDirectionChangers()
    {
        //leftDirectionChanger.SetActive(true);
        //rightDirectionChanger.SetActive(true);
    }

    public void RefreshnumberOfTurnForBirdText(int value, Vector3 pos)
    {

        //numberOfTurnForBirdText.text = value.ToString();
        //numberOfTurnForBirdText.transform.position = new Vector3(pos.x, pos.y + 2, pos.z);
    }
    public void ActiveNumberOfTurnForBirdText()
    {
        //numberOfTurnForBirdText.gameObject.SetActive(true);
    }

    public void DropBird()
    {
        DeactiveHiddenGround();
        //DeactiveDirectionChangers();

        GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdScript>().DropBird();

        DeactiveNumberOfTurnForBirdText();
    }

    void DeactiveNumberOfTurnForBirdText()
    {
        //numberOfTurnForBirdText.gameObject.SetActive(false);
    }

    private void MakePipe()
    {
        pipe[pipeIndex].SetActive(true);

        //GameObject ppe;


        ////Vector3 coordinates = new Vector3(pipe[pipeIndex].location.transform.position.x,
        ////                                  pipe[pipeIndex].location.transform.position.y,
        ////                                  pipe[pipeIndex].location.transform.position.z);

        //Vector3 coordinates = pipe[pipeIndex].transform.position;



        ////ppe = Instantiate(pipe[pipeIndex].pipe, coordinates, Quaternion.identity) as GameObject;
        ////ppe = Instantiate(pipe[pipeIndex], coordinates, Quaternion.identity) as GameObject;

        ////ppe.GetComponent<SpecialPipe>().SetCurrentPipeType(pipe[pipeIndex].type);
        ////ppe.GetComponent<PipeScript>().ActiveMovingPipeOnX(pipe[pipeIndex].isFullMoveActive, pipe[pipeIndex].speed, pipe[pipeIndex].movementRange);
        ////ppe.GetComponent<SpecialPipe>().PrepareCurrentPipeBadge();

        //Quaternion rotation = pipe[pipeIndex].gameObject.GetComponent<PipeScript>().me.self.gameObject.transform.localRotation;

        //ppe = Instantiate(pipe[pipeIndex], coordinates, rotation) as GameObject;

        ////ppe.GetComponent<SpecialPipe>().SetCurrentPipeType(pipe[pipeIndex].);
        ////ppe.GetComponent<PipeScript>().ActiveMovingPipeOnX(pipe[pipeIndex].isFullMoveActive, pipe[pipeIndex].speed, pipe[pipeIndex].movementRange);
        ////ppe.GetComponent<SpecialPipe>().PrepareCurrentPipeBadge();

        //pipe[pipeIndex].GetComponent<PipeScript>().SetDownValue(false);

        pipeIndex++;


    }


    public void DieBird(GameObject inputBird, bool immediately)
    {
        lostBirdsCount++;

        lostBird = true;


        inputBird.GetComponent<BirdScript>().DieBird(immediately);

        SetNumbersToUI();

        //failMarkImage[birdIndex - 1].SetActive(true);

        birdsBGOnScreen[birdIndex - 1].SetActive(false);
        birdsOnScreen[birdIndex - 1].SetActive(false);


        pipe[pipeIndex - 1].GetComponentInChildren<PipeScript>().SetDownValue(true);


        //print("Die");
        if (birdIndex < bird.Length)
        {
            //print(birdIndex);


            MakeBird();

            MakePipe();

            //ActiveDirectionChangers();
        }
        else
        {
            if (gatteredTarget >= targets.Length)
            {
                //progress = 50;
                //print("TARGET *** ACHIVED");
                OpenLevelFinishedPanel(false , false);
            }

            else
            {
                //print(121211);
                //progress = 10;

                //DiePlayer();

                OpenLevelFinishedPanel(false,true);

                print("TARGET *** NOT *** ACHIVED");

               
            }
        }


        //DiePlayer();
    }

    public void Won(int score , GameObject currentBirdInPipe)
    {

        //GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdScript>().DestroyBird(true);

        int index = 0;

        foreach(var brd in targetBirds)
        {

            //print(brd.Key);

            if(brd.Key.gameObject.GetComponent<BirdScript>().CurrentBird == currentBirdInPipe.GetComponent<BirdScript>().CurrentBird)
            {
                if (!targetBirds[brd.Key])
                {
                    targetBirds[brd.Key] = true;
                    targetCheckMarkImage[index].SetActive(true);
                    gatteredTarget++;
                    break;
                }
            }
            index++;
        }

        //print("ENTERED BIRD : "+currentBird.GetComponent<BirdScript>().CurrentBird);

        currentBirdInPipe.GetComponent<BirdScript>().DestroyBird(true);

        //checkMarkImage[birdIndex - 1].SetActive(true);

        birdsBGOnScreen[birdIndex - 1].SetActive(false);
        birdsOnScreen[birdIndex - 1].SetActive(false);

        //if (gatteredTarget < targets.Length)
        //{
        //    targetCheckMarkImage[gatteredTarget].SetActive(true);
        //}
        //gatteredTarget++;

        //print(gatteredTarget);
        //print(targets.Length);


        //SaveAllValues();
        //SetNumberToUI();

        if (birdIndex < bird.Length)
        {
            MakeBird();
            //ActiveDirectionChangers();
            MakePipe();
        }
        else
        {
            bool full = false;
            if (gatteredTarget >= targets.Length && gatteredTarget < bird.Length)
            {
                //print("TARGET ACHIVEDdd");

                full = false;
                if (gatteredTarget == bird.Length)
                {
                    full = true;
                }
                OpenLevelFinishedPanel(full , false);
            }
            else if (gatteredTarget == bird.Length)
            {
                //print("FULL GAME");
                OpenLevelFinishedPanel(true , false);
            }
            else
            {
                OpenLevelFinishedPanel(false, true);
            }
        }
    }

    //public void SetHeartFull()
    //{
    //    countOfLifes= 3;
        
    //}

    void LevelDoneAndSaveAll()
    {
        List<LevelManager.Level> levels = new List<LevelManager.Level>();
        levels.AddRange(GameController.instance.levels);

        // SET STAR IF BETTER
        int lastDoneProgress = levels.Find(foundLevel => foundLevel.id == levelId).progress;

        //print("current progress: " + progress);
        //print("starAlreadyCollected: " + lastDoneProgress);
        //if (progress > lastDoneProgress)
        //{
        levels.Find(foundLevel => foundLevel.id == levelId).progress = levelId;
        //}

        if (!lastLevel)
        {
            levels.Find(foundLevel => foundLevel.id == levelId + 1).unlocked = true;
        }
        

        GameController.instance.Save();
    }



    public void ShowFeathers(GameController.BirdsColor birdColor)
    {
        //switch (birdColor)
        //{
        //    case GameController.BirdsColor.BLUE:
        //        StartCoroutine(ShowBlueFeather());
        //        break;

        //    case GameController.BirdsColor.GREEN:
        //        StartCoroutine(ShowGreenFeather());
        //        break;

        //    case GameController.BirdsColor.ORANGE:
        //        StartCoroutine(ShowOrangeFeather());
        //        break;

        //    case GameController.BirdsColor.PURPLE:
        //        StartCoroutine(ShowPurpleFeather());
        //        break;

        //    case GameController.BirdsColor.RED:
        //        StartCoroutine(ShowRedFeather());
        //        break;

        //    case GameController.BirdsColor.YELLOW:
        //        StartCoroutine(ShowYellowFeather());
        //        break;

        //    default:
        //        print(0);
        //        break;
        //}
    }

    //private IEnumerator ShowBlueFeather()
    //{
    //    //print(1);
    //    blueFeather.SetActive(true);
    //    yield return new WaitForSeconds(1.0f);
    //    blueFeather.SetActive(false);
    //    //print(2);
    //}
    //private IEnumerator ShowGreenFeather()
    //{
    //    //print(1);
    //    greenFeather.SetActive(true);
    //    yield return new WaitForSeconds(1.0f);
    //    greenFeather.SetActive(false);
    //    //print(2);
    //}
    //private IEnumerator ShowOrangeFeather()
    //{
    //    //print(1);
    //    orangeFeather.SetActive(true);
    //    yield return new WaitForSeconds(1.0f);
    //    orangeFeather.SetActive(false);
    //    //print(2);
    //}
    //private IEnumerator ShowPurpleFeather()
    //{
    //    //print(1);
    //    purpleFeather.SetActive(true);
    //    yield return new WaitForSeconds(1.0f);
    //    purpleFeather.SetActive(false);
    //    //print(2);
    //}
    //private IEnumerator ShowRedFeather()
    //{
    //    //print(1);
    //    redFeather.SetActive(true);
    //    yield return new WaitForSeconds(1.0f);
    //    redFeather.SetActive(false);
    //    //print(2);
    //}
    //private IEnumerator ShowYellowFeather()
    //{
    //    //print(1);
    //    yellowFeather.SetActive(true);
    //    yield return new WaitForSeconds(1.0f);
    //    yellowFeather.SetActive(false);
    //    //print(2);
    //}

    public void LoadAllValues()
    {
        //currentScores = GameController.instance.maxScore;
        currentLives = GameController.instance.lives;
        //maxScore = GameController.instance.maxScore;
    }























    public void DiePlayer()
    {
        //progress = (int)CalculateProgress();

        if (!gameClosed)
        {

            //player.gameObject.GetComponent<PlayerScript>().Die();
            ++deadCount;

            //Time.timeScale = 0;
            //MusicController.instance.PlayBadJumpClip();
            //PrepareLevelStars(progress);
            //OpenLevelFinishedPanel(false);
        }


    }

    //public void LevelCompleted()
    //{
    //    if (!gameClosed)
    //    {
    //        Time.timeScale = 0;

    //        progress = 100;

    //        PrepareLevelStars(progress);
    //        OpenLevelFinishedPanel();
    //    }
    //}

    public void SetScreenTouchedTrue()
    {
        screenTouched = true;
        screenUnTouched = false;
    }

    public void SetScreenUnTouchedTrue()
    {
        screenTouched = false;
        screenUnTouched = true;
    }

    public float CalculateProgress()
    {
        //float prgrs = (player.gameObject.transform.position.x - startPlace.gameObject.transform.position.x) / (endLevel.gameObject.transform.position.x) * 100;
        float prgrs = 0;

        if (prgrs > 100)
        {
            print(prgrs + "%");
            prgrs = 100;
        }

        return prgrs;
    }

    public void PrepareLevelStars(int prgrs)
    {
        bool star1, star2, star3;
        star1 = star2 = star3 = false;

        if (progress == 100)
        {
            //print(" 100 **********");
            star1 = true;
            star2 = true;
            star3 = true;
        }
        else if (progress >= 85)
        {
            //print(" 85 **********");

            star1 = true;
            star2 = true;
            star3 = false;
        }
        else if (progress >= 50)
        {
            //print(" 50 **********");

            star1 = true;
            star2 = false;
            star3 = false;
        }


        firstStar.SetActive(star1);
        secondStar.SetActive(star2);
        thirdStar.SetActive(star3);

    }


    #region LEVEL_MANAGER_FUNCTIONS


    



    public void ForwardButton()
    {
        DeactiveAllPanels();

        //print("Forwardddddd" + Time.time);

        if (openNextWorld)
        {
            GoToWorldScene();
        }
        else
        {
            GoToTheNextLevel();
        }
    }

    void LevelDoneAndSaveAll(bool unlockeNextLevel)
    {
        List<LevelManager.Level> levels = new List<LevelManager.Level>();
        levels.AddRange(GameController.instance.levels);

        // SET STAR IF BETTER
        int lastDoneProgress = levels.Find(foundLevel => foundLevel.id == levelId).progress;

        //print("current progress: " + progress);
        //print("starAlreadyCollected: " + lastDoneProgress);
        if (progress > lastDoneProgress)
        {
            levels.Find(foundLevel => foundLevel.id == levelId).progress = progress;
        }

        if (!lastLevel && unlockeNextLevel)
        {
            levels.Find(foundLevel => foundLevel.id == levelId + 1).unlocked = true;
        }

        //print("BB "+GameController.instance.lives);
        
        //print("AA " + GameController.instance.lives);

        SetNumbersToUI();
        GameController.instance.Save();
    }

    
    public void ShareButton()
    {
        MusicController.instance.PlayButtonClip();
        NativeShare.instance.ShareScreenshotWithText("Save the little Perky Bird. Download the game for free " + System.Environment.NewLine + "https://play.google.com/store/apps/details?id=com.parsveda.PerkyBirds");
    }

    #endregion


    #region NAVIGATION_FUNCTIONS

    public void RestartGame()
    {
        MusicController.instance.PlayButtonClip();
        StartCoroutine(GoToLevel(SceneManager.GetActiveScene().name));
    }

    public void OpenSurvivalMode()
    {
        DeactiveAllPanels();
        //print("GO TO NEXT LVL");
        MusicController.instance.PlayButtonClip();
        LoadingController.instance.LoadLevel("SurvivalLevel1");
    }

    public void GoToWorldScene()
    {
        MusicController.instance.PlayButtonClip();
        StartCoroutine(GoToLevel("WorldsScene"));

    }

    public void GoToTheNextLevel()
    {
        //print("GO TO NEXT LVL");
        MusicController.instance.PlayButtonClip();
        levelId++;
        //print(levelId);
        //StartCoroutine(GoToLevel("Level" + levelId));
        LoadingController.instance.LoadLevel("Level" + levelId);
    }
    public IEnumerator BeginFade(bool value, float addedTime)
    {
        //print("begin Fade");
        yield return new WaitForSeconds(addedTime);
        float fadeTime = fader.GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        LoadingController.instance.SetAllowSceneActivationTrue();
    }
    IEnumerator GoToLevel(string name)
    {

        gameClosed = true;

        if (Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;
        }
        float fadeTime = fader.GetComponent<Fading>().BeginFade(1);

        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds((fadeTime)));
        SceneManager.LoadScene(name);

    }


    public void QuitGame()
    {
        deadCount = 0;
        MusicController.instance.PlayButtonClip();
        StartCoroutine(GoToLevel("MainMenu"));
    }


    void PrepareSoundButton()
    {
        //print(GameController.instance.isMusicOn);
        if (GameController.instance.isMusicOn)
        {
            //print(1);
            muteButton.SetActive(false);
        }
        else
        {
            //print(2);
            muteButton.SetActive(true);
        }
    }

    public void SoundButton()
    {
        if (!GameController.instance.isMusicOn)
        {
            GameController.instance.isMusicOn = true;
            MusicController.instance.ForcePlayButtonClip();
            MusicController.instance.PlayBGMusic();
            muteButton.SetActive(false);
        }
        else
        {
            GameController.instance.isMusicOn = false;
            MusicController.instance.StopBGMusic();
            muteButton.SetActive(true);
        }
        GameController.instance.Save();
        //PrepareSoundButton();
    }


    void DeactiveAllPanels()
    {
        foreach (var obj in panelsToDeactive)
        {
            obj.SetActive(false);
        }
    }


    #endregion

    #region PANELS_CONTROLLER

    public void SetNumbersToUI()
    {
        //currentScoreTextForScreen.text = currentScores.ToString() + "/" + neededScore.ToString();
        //currentScoreTextForDeadPanelHaveLife.text = currentScores.ToString();
        //currentScoreTextForDeadPanelDontHaveLife.text = currentScores.ToString();

        //BestScoreTextForDeadPanelDontHaveLife.text = maxScore.ToString();
        //BestScoreTextForDeadPanelHaveLife.text = maxScore.ToString();

        //currentScores.text = currentScores.ToString();

        //livesText.text = currentLives.ToString();
        livesText.text = (currentLives-1268).ToString();

        GameController.instance.lives = currentLives;
        GameController.instance.Save();
    }



    void ActiveObjectsWhilePaused()
    {
        pauseButton.SetActive(true);
        //scoreBox.SetActive(true);
        //touchPanel_1.SetActive(true);
        //touchPanel_2.SetActive(true);

        //touchPanel.SetActive(true);

        Time.timeScale = 1.0f;
    }

    void DeactiveObjectsWhilePaused()
    {
        //pauseButton.SetActive(false);
        //scoreBox.SetActive(false);

        //touchPanel.SetActive(false);

        Time.timeScale = 0;
    }

    void CloseAllPanels()
    {
        pausedPanel.SetActive(false);
        quitAlertPanel.SetActive(false);
        freeLifePanel.SetActive(false);
        deadPanelHaveLife.SetActive(false);
        deadPanelDontHaveLife.SetActive(false);

    }


    public void OpenPausedPanel()
    {
        //if(currentLives < 1)
        if(currentLives < 1269)
        {
            restartButtonInPausePanel.SetActive(false);
        }
        else
        {
            restartButtonInPausePanel.SetActive(true);
        }

        DeactiveObjectsWhilePaused();
        MusicController.instance.PlayButtonClip();
        CloseAllPanels();

        pausedPanel.SetActive(true);
    }
    public void ClosePausedPanel()
    {
        ActiveObjectsWhilePaused();
        MusicController.instance.PlayCloseClip();
        CloseAllPanels();
    }


    public void OpenQuitAlertPanel()
    {
        CloseAllPanels();
        MusicController.instance.PlayButtonClip();
        quitAlertPanel.SetActive(true);
    }
    public void CloseQuitAlertPanel()
    {
        CloseAllPanels();
        MusicController.instance.PlayCloseClip();
        pausedPanel.SetActive(true);
    }

    public void OpenLevelFinishedPanel(bool full , bool openFail)
    {
        if(lostBirdsCount == 0)
        {
            progress = 100;
        }

        else if(lostBirdsCount == 1 && gatteredTarget == targets.Length)
        {
            progress = 85;
        }
        else if(lostBirdsCount>1 && gatteredTarget == targets.Length)
        {
            progress = 50;
        }
        else if (lostBirdsCount > 1 && gatteredTarget < targets.Length)
        {
            progress = 10;
        }

        PrepareLevelStars(progress);

        starsPanel.SetActive(true);


        if (!openFail)
        {
            //print(1);

            bool unlockeNextLevel = false;

            if (full)
            {
                //progress = 100;
                currentLives++;
                levelText.text = "Level " + levelId + " Fully Completed You Earned 1 Life";
            }
            else
            {

                levelText.text = "Level " + levelId + " Completed!";
            }

            unlockeNextLevel = true;
            if (lastLevel)
            {
                levelText.text = "Complete Game ... Wait For Update";

                unlockeNextLevel = false;
            }

            /*
            if (progress >= 85)
            {

                levelText.text = "Level " + levelId + " Finished";

                unlockeNextLevel = true;
                if (lastLevel)
                {
                    unlockeNextLevel = false;
                }


                levelText.text = "Level " + levelId + " Failed";

                unlockeNextLevel = false;

            }

            progressText.text = progress + " %";
            */

            //print(progress);

            CloseAllPanels();

            //DeactiveObjectsWhilePaused();
            MusicController.instance.PlayButtonClip();
            levelFinishedPanel.SetActive(true);

            nextLevelButton.SetActive(unlockeNextLevel);

            LevelDoneAndSaveAll(unlockeNextLevel);
        }
        else
        {
            print(2);
            if (currentLives > 1268)
            {
                OpenDeadPanelHaveLife();
            }
            else
            {
                OpenDeadPanelDontHaveLife();
            }
            Time.timeScale = 1.0f;
        }
        //Time.timeScale = 1.0f;
    }
    

    public void OpenFreeLifePanel()
    {
        
        CloseAllPanels();
        //DeactiveObjectsWhilePaused();

        freeLifePanel.SetActive(true);
    }

    public void CloseFreeLifePanel()
    {
        currentLives = GameController.instance.lives;
        SetNumbersToUI();


        CloseAllPanels();
        MusicController.instance.PlayCloseClip();
        ActiveObjectsWhilePaused();
        if (currentLives > 1268)
        {
            print("Have");
            OpenDeadPanelHaveLife();
        }
        else
        {
            print("Dont Have");
            OpenDeadPanelDontHaveLife();
        }
        //pausedPanel.SetActive(true);
    }

    public void OpenDeadPanelHaveLife()
    {

        CloseAllPanels();
        //DeactiveObjectsWhilePaused();
        deadPanelHaveLife.SetActive(true);
    }

    public void OpenDeadPanelDontHaveLife()
    {

        CloseAllPanels();
        //DeactiveObjectsWhilePaused();

        deadPanelDontHaveLife.SetActive(true);
    }


    #endregion


}
