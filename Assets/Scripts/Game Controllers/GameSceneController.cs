using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameSceneController : MonoBehaviour
{

    public static GameSceneController instance;

    public const string path = "items";

    ItemContainer loadedItems;

    [SerializeField]
    private bool lastLevel;

    [SerializeField]
    public GameObject fader;

    [SerializeField]
    private GameObject star, arrow, scoreContainer, messagePanel, quitAlertPanel, pausePanel, storePanel, deadPanelHaveLife, deadPanelDontHaveLife, freeLifePanel;

    [SerializeField]
    private Text livesText, currentScoreTextForScreen, currentScoreTextForDeadPanelHaveLife, currentScoreTextForDeadPanelDontHaveLife, BestScoreTextForDeadPanelDontHaveLife, BestScoreTextForDeadPanelHaveLife;

    [SerializeField]
    private GameObject deactiveSprite;

    [SerializeField]
    private Button pauseButton;


    [SerializeField]
    private GameObject[] stepZeroBirds;

    [SerializeField]
    private GameObject[] birds;

    [SerializeField]
    private GameObject pipe;

    [SerializeField]
    private GameObject hiddenGround;

    [SerializeField]
    private GameObject leftDirectionChanger, rightDirectionChanger;

    [SerializeField]
    private AnimationClip arrowClip, starClip;

    [SerializeField]
    private GameObject blueFeather, greenFeather, redFeather, purpleFeather, yellowFeather, orangeFeather;

    //[SerializeField]
    //private Text numberOfTurn_ForBirdText;

    [SerializeField]
    private GameObject heart_1, heart_2, heart_3, heart_4, heart_5;

    //public long ;

    private long heartCount,lives, currentScores, maxScore;

    //for bird
    private float firstX = -15f;
    private float secondX = 15f;
    private float minY = -4f;
    private float maxY = 5f;

    //fpr pipe
    private float pipeMinX = -14.0f;
    private float pipeMaxX = 14.0f;
    //private float pipeY = -4.8f;
    private float pipeY = -12.8f;


    //LEFT  -> true
    //RIGHT -> false
    private bool isLeftSpawnSide;


    //private bool step_01, step_02, step_03, step_04, step_05, step_06, step_07, step_08, step_09, step_10, step_11;
    public bool[] steps;


    [HideInInspector]
    public bool isBirdTurnedYet;

    [SerializeField]
    private Slider movePipe, pipeMoveSpeed;

    [SerializeField]
    private Text pipeMoveRangeText, pipeMoveSpeedText;


    private long currentLevel;

    private float pipeCount , scoreToPass , birdMaxTurn;
    private SpecialPipe.SpecialPipeType pipe_1_Type, pipe_2_Type;
    private float pipe_1_Speed, pipe_1_MovementRange, pipe_2_Speed, pipe_2_MovementRange;


    //private int levelId;
    //private int neededScore;

    void Awake()
    {
        CreateInstance();
        loadedItems = LoadXML();

        /*
        foreach(var ic in loadedItems.items)
        {
            print(ic.id);
        }
        */
        //steps[0] = true;
    }


    // Use this for initialization
    void Start()
    {


        print(SceneManager.GetActiveScene().name);

        //levelId = int.Parse(SceneManager.GetActiveScene().name.Remove(0, 5));
        //print(levelId);
        //neededScore = levelId * 5;

        currentScores = 0;
        currentLevel = 0;
        steps = new bool[10];
        steps[0] = true;

        //GameController.instance.lives = 3;

        heartCount = 5;
        SetHearsActivation(true, true, true, true, true);

        Time.timeScale = 1.0f;
        //print(steps.Length);
        //SetNumberToUI();

        //StartCoroutine(IncreaseLevel());

        MakeBird();
        ActiveDirectionChangers();
        MakePipe();


        LoadAllValues();
        SetNumbersToUI();

        if (GameController.instance.isMusicOn)
        {
            deactiveSprite.SetActive(true);
        }
        else
        {
            deactiveSprite.SetActive(false);
        }


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

        PrepareSoundButton();


    }

    void OnLevelWasLoaded()
    {
        AdsController.instance.ShowChartboostInterstitial();
    }

    // Update is called once per frame
    void Update()
    {
        //print(Random.Range(1,3));

    }

    private void CreateInstance()
    {
        if (instance == null)
            instance = this;
    }

    /* private IEnumerator IncreaseLevel()
     {
         yield return new WaitForSeconds(60.0f);
         currentLevel++;
         StartCoroutine(IncreaseLevel());
     }*/

    private void SetHearsActivation(bool first , bool second , bool third , bool fourth, bool fifth)
    {
        heart_1.SetActive(first);
        heart_2.SetActive(second);
        heart_3.SetActive(third);
        heart_4.SetActive(fourth);
        heart_5.SetActive(fifth);
    }

    private ItemContainer LoadXML()
    {
        ItemContainer ic = ItemContainer.Load(path);

        /*
        foreach (Item item in ic.items)
        {
            print(item.id + " " + item.bird + " " + item.pipeCountProbability + " " + item.pipeMovement);
        }
        */

        return ic;
    }

    private bool MakeRandomTrueOrFalse()
    {
        // MAKE Random true or false
        bool boolean = (Random.value > 0.5f);
        return boolean;
    }

    public void LoadAllValues()
    {
        //currentScores = GameController.instance.maxScore;
        lives = GameController.instance.lives;
        maxScore = GameController.instance.maxScore;
    }

    private void SaveAllValues()
    {
        GameController.instance.lives = lives;
        if (maxScore < currentScores)
        {
            GameController.instance.maxScore = currentScores;
            maxScore = currentScores;
        }
        GameController.instance.Save();
    }

    public void SetNumbersToUI()
    {
        //currentScoreTextForScreen.text = currentScores.ToString() + "/" +neededScore.ToString();
        currentScoreTextForScreen.text = currentScores.ToString();
        currentScoreTextForDeadPanelHaveLife.text = currentScores.ToString();
        currentScoreTextForDeadPanelDontHaveLife.text = currentScores.ToString();

        BestScoreTextForDeadPanelDontHaveLife.text = maxScore.ToString();
        BestScoreTextForDeadPanelHaveLife.text = maxScore.ToString();

        //currentScores.text = currentScores.ToString();

        //livesText.text = lives.ToString();
        livesText.text = (lives-1268).ToString();
    }


    public void RefreshnumberOfTurnForBirdText(int value, Vector3 pos)
    {

        //numberOfTurnForBirdText.text = value.ToString();
        //numberOfTurnForBirdText.transform.position = new Vector3(pos.x, pos.y + 2, pos.z);
    }

    private void MakeBird()
    {
        //SetHeartFull();

        //GameObject bird = new GameObject();


        GameObject bird;
        
        
        //LEFT  -> true
        //RIGHT -> false
        isBirdTurnedYet = false;

        isLeftSpawnSide = MakeRandomTrueOrFalse();
        float x;
        x = isLeftSpawnSide ? firstX : secondX;

        // print(isLeftSpawnSide);

        Vector3 coordinates = new Vector3(x, Random.Range(minY, maxY));

        if (currentScores <= 2)
        {
            //print("Step 0 Bird");
            coordinates = new Vector3(x, -5);
        }

        //Vector3 coordinates = new Vector3(x,2.0f);

        ActiveHiddenGround(coordinates);
        if (currentScores <= 2)
        {
            //print("Steps 0 Make bird");
            bird = Instantiate(stepZeroBirds[0], coordinates, Quaternion.identity) as GameObject;
        }
        else
        {
            //print("Make bird");
            //GameObject bird = Instantiate(birds[Random.Range(0, birds.Length)], coordinates, Quaternion.identity) as GameObject;
            bird = Instantiate(birds[Random.Range(0, birds.Length)], coordinates, Quaternion.identity) as GameObject;

            //bird.GetComponent<BirdScript>().me.birdForwardSpeed = Random.Range(6.1f, 18.9f);
            bird.GetComponent<BirdScript>().forwardSpeed = Random.Range(6.1f, 10.9f);

            float randomValue = Random.Range(0.18f, 0.22f);

            bird.transform.localScale = new Vector3(randomValue, randomValue, 1);

            bird.GetComponent<BirdScript>().StartFly();

        }
        if (!isLeftSpawnSide)
        {
            //print("HORRRRRRRRRRRRA");
            bird.GetComponent<BirdScript>().ForceTurnLeft();
        }

        //bird.GetComponent<BirdScript>().SetNumberOfTurnForBird(CalculateBirdTurnCount());

        

        bird.GetComponent<BirdScript>().SetName();

        
    }

    void ActiveHiddenGround(Vector3 pos)
    {
        pos.x = 0f;
        pos.y -= 0.1f;

        hiddenGround.transform.position = pos;
        hiddenGround.SetActive(true);
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
        leftDirectionChanger.SetActive(false);
        rightDirectionChanger.SetActive(false);
    }

    void ActiveDirectionChangers()
    {
        leftDirectionChanger.SetActive(true);
        rightDirectionChanger.SetActive(true);
    }

    public void DropBird()
    {
        DeactiveHiddenGround();
        DeactiveDirectionChangers();

        GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdScript>().DropBird();

        DeactiveNumberOfTurnForBirdText();
    }

    private void MakePipe()
    {

        //print("Start");

        CalculatePipeCountAndScroe();

        Vector3 coordinates;
        GameObject myPipe_1 = null;
        GameObject myPipe_2 = null;


        if (pipeCount == 1)
        {
            SetFirstPipeVariebles();

            

            if (isLeftSpawnSide)
            {
                coordinates = new Vector3(Random.Range(-5.0f, pipeMaxX), pipeY);
                if (steps[0])
                {
                    coordinates = new Vector3(Random.Range(-5.0f, pipeMaxX - 5.0f), pipeY);
                }
            }
            else
            {
                coordinates = new Vector3(Random.Range(pipeMinX + 5.0f, 5.0f), pipeY);
                if (steps[0])
                {
                    coordinates = new Vector3(Random.Range(pipeMinX, -5.0f), pipeY);
                }
            }
            myPipe_1 = Instantiate(pipe, coordinates, Quaternion.identity) as GameObject;
            myPipe_2 = null;

            myPipe_1.GetComponent<SpecialPipe>().SetCurrentPipeType(pipe_1_Type);
            myPipe_1.GetComponent<PipeScript>().ActiveMovingPipeOnX(false, pipe_1_Speed, pipe_2_MovementRange);
            myPipe_1.GetComponent<SpecialPipe>().PrepareCurrentPipeBadge();

            myPipe_1.transform.localScale = new Vector3(2.2f, 2, 1);
        }

        else if (pipeCount == 2)
        {
            SetFirstAndSecondPipeVariebles();

            coordinates = new Vector3(Random.Range(2.0f, pipeMaxX), pipeY);
            myPipe_1 = Instantiate(pipe, coordinates, Quaternion.identity) as GameObject;

            myPipe_1.GetComponent<SpecialPipe>().SetCurrentPipeType(pipe_1_Type);
            myPipe_1.GetComponent<PipeScript>().ActiveMovingPipeOnX(false, pipe_1_Speed, pipe_2_MovementRange);
            myPipe_1.GetComponent<SpecialPipe>().PrepareCurrentPipeBadge();

            coordinates = new Vector3(Random.Range(pipeMinX, -2.0f), pipeY);
            myPipe_2 = Instantiate(pipe, coordinates, Quaternion.identity) as GameObject;

            myPipe_2.GetComponent<SpecialPipe>().SetCurrentPipeType(pipe_2_Type);
            myPipe_2.GetComponent<PipeScript>().ActiveMovingPipeOnX(false, pipe_2_Speed, pipe_2_MovementRange);
            myPipe_2.GetComponent<SpecialPipe>().PrepareCurrentPipeBadge();

            myPipe_1.transform.localScale = new Vector3(2.2f, 2, 1);
            myPipe_2.transform.localScale = new Vector3(2.1f, 2, 1);


        }
        /*
        if (steps[0])
        {
            myPipe_1.transform.localScale = new Vector3(2f, myPipe_1.transform.localScale.y, myPipe_1.transform.localScale.z);
            if (myPipe_2 != null)
            {
                myPipe_2.transform.localScale = new Vector3(2f, myPipe_2.transform.localScale.y, myPipe_2.transform.localScale.z);
            }
        }
        else if (steps[1])
        {
            myPipe_1.transform.localScale = new Vector3(1.9f, myPipe_1.transform.localScale.y, myPipe_1.transform.localScale.z);
            if (myPipe_2 != null)
            {
                myPipe_2.transform.localScale = new Vector3(1.9f, myPipe_2.transform.localScale.y, myPipe_2.transform.localScale.z);
            }
        }
        else if (steps[2])
        {
            myPipe_1.transform.localScale = new Vector3(1.9f, myPipe_1.transform.localScale.y, myPipe_1.transform.localScale.z);
            if (myPipe_2 != null)
            {
                myPipe_2.transform.localScale = new Vector3(1.9f, myPipe_2.transform.localScale.y, myPipe_2.transform.localScale.z);
            }
        }
        else if (steps[3])
        {
            myPipe_1.transform.localScale = new Vector3(1.9f, myPipe_1.transform.localScale.y, myPipe_1.transform.localScale.z);
            if (myPipe_2 != null)
            {
                myPipe_2.transform.localScale = new Vector3(1.9f, myPipe_2.transform.localScale.y, myPipe_2.transform.localScale.z);
            }
        }
        else if (steps[4])
        {
            myPipe_1.transform.localScale = new Vector3(1.8f, myPipe_1.transform.localScale.y, myPipe_1.transform.localScale.z);
            if (myPipe_2 != null)
            {
                myPipe_2.transform.localScale = new Vector3(1.8f, myPipe_2.transform.localScale.y, myPipe_2.transform.localScale.z);
            }
        }
        else if (steps[5])
        {
            myPipe_1.transform.localScale = new Vector3(1.8f, myPipe_1.transform.localScale.y, myPipe_1.transform.localScale.z);
            if (myPipe_2 != null)
            {
                myPipe_2.transform.localScale = new Vector3(1.8f, myPipe_2.transform.localScale.y, myPipe_2.transform.localScale.z);
            }
        }
        else if (steps[6])
        {
            myPipe_1.transform.localScale = new Vector3(1.8f, myPipe_1.transform.localScale.y, myPipe_1.transform.localScale.z);
            if (myPipe_2 != null)
            {
                myPipe_2.transform.localScale = new Vector3(1.8f, myPipe_2.transform.localScale.y, myPipe_2.transform.localScale.z);
            }
        }
        else if (steps[7])
        {
            myPipe_1.transform.localScale = new Vector3(1.7f, myPipe_1.transform.localScale.y, myPipe_1.transform.localScale.z);

            if (myPipe_2 != null)
            {
                myPipe_2.transform.localScale = new Vector3(1.7f, myPipe_2.transform.localScale.y, myPipe_2.transform.localScale.z);
            }
        }*/
    }

    private void CalculatePipeCountAndScroe()
    {
        //print("Calculate");
        float doublePipeProbability = 0;
        float scoreToPassT = 0;
        foreach (Item item in loadedItems.items)
        {
            if (long.Parse(item.id) == currentLevel)
            {
                //print("EEEEE");

                doublePipeProbability = item.DoublePipeProbability;
                scoreToPassT = item.scoreToPass;
                //print("doublePipeProbability " + doublePipeProbability);
                //print("scoreToPassT " + scoreToPassT);
            }
        }
        SetDoublePipeProbability(doublePipeProbability);
        scoreToPass = scoreToPassT;
        //print(scoreToPass);
        //print(pipeCount);
        //print(scoreToPass);

    }

    private int CalculateBirdTurnCount()
    {
        long maxTurn = 0;
        foreach (Item item in loadedItems.items)
        {
            if (long.Parse(item.id) == currentLevel)
            {
                maxTurn = (long)(item.birdMaxTurn);
            }
        }
        //print("maxTurn "+maxTurn);
        birdMaxTurn =Random.Range(Mathf.Ceil((maxTurn/2)), maxTurn);
        return (int)birdMaxTurn;
    }

    private void SetStepTrueAndOthersFalse(int step)
    {
        for (int i = 0; i < steps.Length; i++)
        {
            steps[i] = false;
        }
        steps[step] = true;
    }

    private void SetDoublePipeProbability(float value)
    {
        bool isDouble = (Random.value < value / 100);
        if (isDouble)
        {
            pipeCount = 2;
        }
        else
        {
            pipeCount = 1;
        }
    }

    private void SetMinXAndMaxXValuesForPipe(float value)
    {
        pipeMinX = -value;
        pipeMaxX = value;
    }

    void SetFirstPipeVariebles()
    {

        float normalTypeProbability, colorTypeProbability, lifeTypeProbability;

        normalTypeProbability = colorTypeProbability = lifeTypeProbability = 0.0f;


        foreach (Item item in loadedItems.items)
        {
            if (long.Parse(item.id) == currentLevel)
            {
                pipe_1_Speed = item.pipe1Speed;
                pipe_1_MovementRange = item.pipe1MoveRange;
                normalTypeProbability = item.pipe1NormalTypeProbability;
                colorTypeProbability = item.pipe1ColorTypeProbability;
                lifeTypeProbability = item.pipe1LifeTypeProbability;

            }
        }

        int temp = CalculatePipeTypeProbability(colorTypeProbability, lifeTypeProbability ,false);
        switch (temp)
        {
            case 1:
                pipe_1_Type = SpecialPipe.SpecialPipeType.NORMAL;
                break;

            case 2:
                CalculateRandomColoryPipe(true);
                break;

            case 3:
                //pipe_1_Type = SpecialPipe.SpecialPipeType.LIFE;
                if (MakeRandomTrueOrFalse())
                {
                    pipe_1_Type = SpecialPipe.SpecialPipeType.LIFE;
                }
                else
                {
                    int rnd = Random.Range(1, 4);
                    print("RANDOM VALUE -> "+rnd);
                    switch (rnd)
                    {
                        case 1:
                            pipe_1_Type = SpecialPipe.SpecialPipeType.WING_1;
                            break;
                        case 2:
                            pipe_1_Type = SpecialPipe.SpecialPipeType.WING_2;
                            break;
                        case 3:
                            pipe_1_Type = SpecialPipe.SpecialPipeType.WING_3;
                            break;
                    }
                }
                break;

        }

    }


    void SetFirstAndSecondPipeVariebles()
    {
        float pipe_1_NormalTypeProbability, pipe_1_ColorTypeProbability, pipe_1_LifeTypeProbability;
        float pipe_2_NormalTypeProbability, pipe_2_ColorTypeProbability, pipe_2_LifeTypeProbability;

        pipe_1_NormalTypeProbability = pipe_1_ColorTypeProbability = pipe_1_LifeTypeProbability = 0.0f;
        pipe_2_NormalTypeProbability = pipe_2_ColorTypeProbability = pipe_2_LifeTypeProbability = 0.0f;


        foreach (Item item in loadedItems.items)
        {
            if (long.Parse(item.id) == currentLevel)
            {
                pipe_1_Speed = item.pipe1Speed;
                pipe_1_MovementRange = item.pipe1MoveRange;

                pipe_1_NormalTypeProbability = item.pipe1NormalTypeProbability;
                pipe_1_ColorTypeProbability = item.pipe1ColorTypeProbability;
                pipe_1_LifeTypeProbability = item.pipe1LifeTypeProbability;

                pipe_2_NormalTypeProbability = item.pipe2NormalTypeProbability;
                pipe_2_ColorTypeProbability = item.pipe2ColorTypeProbability;
                pipe_2_LifeTypeProbability = item.pipe2LifeTypeProbability;

            }
        }

        int temp = CalculatePipeTypeProbability(pipe_1_ColorTypeProbability, pipe_1_LifeTypeProbability , false);
        switch (temp)
        {
            case 1:
                pipe_1_Type = SpecialPipe.SpecialPipeType.NORMAL;
                break;

            case 2:
                CalculateRandomColoryPipe(true);
                break;

            case 3:
                //pipe_1_Type = SpecialPipe.SpecialPipeType.LIFE;
                if (MakeRandomTrueOrFalse())
                {
                    pipe_1_Type = SpecialPipe.SpecialPipeType.LIFE;
                }
                else
                {
                    int rnd = Random.Range(1, 4);
                    print("RANDOM VALUE -> " + rnd);
                    switch (rnd)
                    {
                        case 1:
                            pipe_1_Type = SpecialPipe.SpecialPipeType.WING_1;
                            break;
                        case 2:
                            pipe_1_Type = SpecialPipe.SpecialPipeType.WING_2;
                            break;
                        case 3:
                            pipe_1_Type = SpecialPipe.SpecialPipeType.WING_3;
                            break;
                    }
                }
                break;

        }

        temp = CalculatePipeTypeProbability(pipe_2_ColorTypeProbability, pipe_2_LifeTypeProbability , true);
        switch (temp)
        {
            case 1:
                pipe_2_Type = SpecialPipe.SpecialPipeType.NORMAL;
                break;

            case 2:
                CalculateRandomColoryPipe(false);
                break;

            case 3:
                //pipe_2_Type = SpecialPipe.SpecialPipeType.LIFE;
                if (MakeRandomTrueOrFalse())
                {
                    pipe_2_Type = SpecialPipe.SpecialPipeType.LIFE;
                }
                else
                {
                    int rnd = Random.Range(1, 4);
                    //print("RANDOM VALUE -> " + rnd);
                    switch (rnd)
                    {
                        case 1:
                            pipe_2_Type = SpecialPipe.SpecialPipeType.WING_1;
                            break;
                        case 2:
                            pipe_2_Type = SpecialPipe.SpecialPipeType.WING_2;
                            break;
                        case 3:
                            pipe_2_Type = SpecialPipe.SpecialPipeType.WING_3;
                            break;
                    }
                }
                break;

        }

    }



    int CalculatePipeTypeProbability(float colorProbability ,float lifeProbability , bool calculateColor)
    {
        int normalP, colorP, lifeP;
        normalP = 1;
        colorP = 0;
        lifeP = 0;

        
        bool isColor = (Random.value < colorProbability / 100);
        //print("isColor " + isColor + "colorProbability " + colorProbability);
        if (isColor)
        {
            colorP++;
        }
        else
        {
            normalP++;
        }
        

        bool isLife = (Random.value < lifeProbability/ 100);
        //print("isLife " + isLife + "lifeProbability " + lifeProbability);
        if (isLife)
        {
            lifeP++;
        }
        else
        {
            normalP++;
        }
        isLife = false;
        isLife = MakeRandomTrueOrFalse();
        //print("isLife " + isLife);
        if (isLife)
        {
            lifeP++;
        }
        else
        {
            colorP++;
        }

        if(normalP == 3)
        {
            return 1;
        }
        if (colorP == 2 && calculateColor)
        {
            return 2;
        }
        if(lifeP == 2)
        {
            return 3;
        }
        return 1;
    }

    void CalculateRandomColoryPipe(bool isFirst)
    {
        if (isFirst)
        {
            int random = Random.Range(0, 6);
            switch (random)
            {
                case 0:
                    pipe_1_Type = SpecialPipe.SpecialPipeType.BLUE;
                    break;

                case 1:
                    pipe_1_Type = SpecialPipe.SpecialPipeType.GREEN;
                    break;

                case 2:
                    pipe_1_Type = SpecialPipe.SpecialPipeType.ORANGE;
                    break;

                case 3:
                    pipe_1_Type = SpecialPipe.SpecialPipeType.PURPLE;
                    break;

                case 4:
                    pipe_1_Type = SpecialPipe.SpecialPipeType.RED;
                    break;

                case 5:
                    pipe_1_Type = SpecialPipe.SpecialPipeType.YELLOW;
                    break;

            }

        }
        else
        {
            int random = Random.Range(0, 6);
            switch (random)
            {
                case 0:
                    pipe_2_Type = SpecialPipe.SpecialPipeType.BLUE;
                    break;

                case 1:
                    pipe_2_Type = SpecialPipe.SpecialPipeType.GREEN;
                    break;

                case 2:
                    pipe_2_Type = SpecialPipe.SpecialPipeType.ORANGE;
                    break;

                case 3:
                    pipe_2_Type = SpecialPipe.SpecialPipeType.PURPLE;
                    break;

                case 4:
                    pipe_2_Type = SpecialPipe.SpecialPipeType.RED;
                    break;

                case 5:
                    pipe_2_Type = SpecialPipe.SpecialPipeType.YELLOW;
                    break;

            }
        }

        ///////////////////////////////////////////////////////////////////////////////

        

    }

    public void DieBird(GameObject bird, bool immediately , bool makePipe)
    {
        if (makePipe)
        {
            MakePipe();
        }
        bird.GetComponent<BirdScript>().DieBird(immediately);
    }

    public void Won(int score)
    {
        if (isBirdTurnedYet)
        {
            currentScores += score;
            //print(1);
        }
        else
        {
            currentScores += (++score);
            showStar();
            //print(2);
        }

        //BirdScript.instance.DestroyBird();
        GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdScript>().DestroyBird(true);


        SaveAllValues();
        SetNumbersToUI();

        if (currentScores > scoreToPass)
        {

            currentLevel++;
            //print("currentLevel " + currentLevel);
        }

        MakeBird();
        ActiveDirectionChangers();
        MakePipe();


        //if (currentScores < neededScore)
        //{
        //    MakeBird();
        //    ActiveDirectionChangers();
        //    MakePipe();
        //}
        //else
        //{
        //    levelText.text ="Level " + System.Environment.NewLine +
        //                    levelId + System.Environment.NewLine +
        //                    "Finished";

        //    LevelDoneAndSaveAll();


        //    OpenLevelFinishedPanel();
        //}
    }

    //void LevelDoneAndSaveAll()
    //{
    //    List<LevelManager.Level> levels = new List<LevelManager.Level>();
    //    levels.AddRange(GameController.instance.levels);

    //    // SET STAR IF BETTER
    //    int lastDoneProgress = levels.Find(foundLevel => foundLevel.id == levelId).stars;

    //    //print("current progress: " + progress);
    //    //print("starAlreadyCollected: " + lastDoneProgress);
    //    //if (progress > lastDoneProgress)
    //    //{
    //        levels.Find(foundLevel => foundLevel.id == levelId).stars = levelId;
    //    //}

    //    if (!lastLevel)
    //    {
    //        levels.Find(foundLevel => foundLevel.id == levelId + 1).unlocked = true;
    //    }

    //    GameController.instance.Save();
    //}




    public void SetHeartFull()
    {
        heartCount = 5;
        SetHearsActivation(true, true, true,true, true);
    }

    public void PlusHeartValue(int value)
    {
        heartCount += value;
        if (heartCount > 5)
        {
            heartCount = 5;
        }
        switch (heartCount)
        {
            case 5:
                SetHearsActivation(true, true, true, true, true);
                break;
            case 4:
                SetHearsActivation(true, true, true, true, false);
                break;
            case 3:
                SetHearsActivation(true, true, true, false, false);
                break;
            case 2:
                SetHearsActivation(true, true, false, false, false);
                break;
            case 1:
                SetHearsActivation(true, false, false, false, false);
                break;
        }
    }

    public void DecreaseHeart()
    {
        heartCount--;
        switch (heartCount)
        {
            case 5:
                SetHearsActivation(true, true, true, true, true);
                MakeBird();
                ActiveDirectionChangers();
                break;
            case 4:
                SetHearsActivation(true, true, true, true, false);
                MakeBird();
                ActiveDirectionChangers();
                break;
            case 3:
                SetHearsActivation(true, true, true, false, false);
                MakeBird();
                ActiveDirectionChangers();
                break;
            case 2:
                //AdsController.instance.ShowChartboostInterstitial();
                SetHearsActivation(true, true, false, false, false);
                MakeBird();
                ActiveDirectionChangers();
                break;
            case 1:
                SetHearsActivation(true, false, false, false, false);
                MakeBird();
                ActiveDirectionChangers();
                break;
            case 0:
                SetHearsActivation(false, false, false, false, false);

                /*
                SetHeartFull();
                MakeBird();
                ActiveDirectionChangers();
                OpenPausePanel();
                DecreaseLive();
                */

                SaveAllValues();
                SetNumbersToUI();
                //AdsController.instance.ShowChartboostInterstitial();

                OpenDeadPanelHaveLife();
                break;
        }

    }

    public void DecreaseLive()
    {
        //--lives;
        SaveAllValues();
        SetNumbersToUI();
        
        if (lives > 0)
        {

            //AdsController.instance.ShowChartboostInterstitial();

            //OpenPausePanel();
            OpenDeadPanelHaveLife();

            //MakeBird();
            //ActiveDirectionChangers();
            
            //SetHearsActivation(true, true, true);
            //heartCount = 3;

            //DestroyPipe();
            //MakePipe();

            //OpenDeadPanelHaveLife();

        }
        else if (lives <= 0)
        {
            StoreController.instance.SetLivesZero();
            StartCoroutine(ShowArrow(5));
            OpenDeadPanelDontHaveLife();

        }

    }


    void DestroyPipe()
    {
        //GameObject.FindGameObjectWithTag("Pipe").GetComponent<PipeScript>().SetDownTrue();
        //pipe.GetComponent<PipeScript>().SetDownTrue();
    }

    public void showStar()
    {
        StartCoroutine(ShowStarAnim());
    }

    IEnumerator ShowStarAnim()
    {
        star.SetActive(true);
        yield return new WaitForSeconds(starClip.length);
        star.SetActive(false);
    }


    IEnumerator ShowArrow(int time)
    {
        arrow.SetActive(true);
        float animLenght = arrowClip.length;
        yield return new WaitForSeconds(time * animLenght);
        arrow.SetActive(false);
    }

    void PrepareSoundButton()
    {
        if (GameController.instance.isMusicOn)
        {
            //print("FALSE");
            deactiveSprite.SetActive(false);
        }
        else
        {
            //print("TRUE");

            deactiveSprite.SetActive(true);
        }
    }
    
    public void SoundButton()
    {
        //MusicController.instance.PlayButtonClip();

        if (!GameController.instance.isMusicOn)
        {
            GameController.instance.isMusicOn = true;
            MusicController.instance.ForcePlayButtonClip();
            MusicController.instance.PlayBGMusic();
            //deactiveSprite.SetActive(false);
        }
        else
        {
            GameController.instance.isMusicOn = false;
            MusicController.instance.StopBGMusic();
            //deactiveSprite.SetActive(true);
        }
        PrepareSoundButton();
        GameController.instance.Save();
    }

    public void ShowFeathers(GameController.BirdsColor birdColor)
    {
        switch (birdColor)
        {
            case GameController.BirdsColor.BLUE:
                StartCoroutine(ShowBlueFeather());
                break;

            case GameController.BirdsColor.GREEN:
                StartCoroutine(ShowGreenFeather());
                break;

            case GameController.BirdsColor.ORANGE:
                StartCoroutine(ShowOrangeFeather());
                break;

            case GameController.BirdsColor.PURPLE:
                StartCoroutine(ShowPurpleFeather());
                break;

            case GameController.BirdsColor.RED:
                StartCoroutine(ShowRedFeather());
                break;

            case GameController.BirdsColor.YELLOW:
                StartCoroutine(ShowYellowFeather());
                break;

            default:
                print(0);
                break;
        }
    }

    private IEnumerator ShowBlueFeather()
    {
        //print(1);
        blueFeather.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        blueFeather.SetActive(false);
        //print(2);
    }
    private IEnumerator ShowGreenFeather()
    {
        //print(1);
        greenFeather.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        greenFeather.SetActive(false);
        //print(2);
    }
    private IEnumerator ShowOrangeFeather()
    {
        //print(1);
        orangeFeather.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        orangeFeather.SetActive(false);
        //print(2);
    }
    private IEnumerator ShowPurpleFeather()
    {
        //print(1);
        purpleFeather.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        purpleFeather.SetActive(false);
        //print(2);
    }
    private IEnumerator ShowRedFeather()
    {
        //print(1);
        redFeather.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        redFeather.SetActive(false);
        //print(2);
    }
    private IEnumerator ShowYellowFeather()
    {
        //print(1);
        yellowFeather.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        yellowFeather.SetActive(false);
        //print(2);
    }



    #region PANEL_FUNCTIONS

    public void OpenMessagePanel()
    {

        MusicController.instance.PlayButtonClip();


        // Close Others
        messagePanel.SetActive(false);
        quitAlertPanel.SetActive(false);
        pausePanel.SetActive(false);
        storePanel.SetActive(false);
        deadPanelDontHaveLife.SetActive(false);
        deadPanelHaveLife.SetActive(false);
        // end Close Others

        //touchButton.SetActive(false);

        messagePanel.SetActive(true);
    }

    public void OpenQuitAlertPanel()
    {

        MusicController.instance.PlayButtonClip();


        // Close Others
        messagePanel.SetActive(false);
        quitAlertPanel.SetActive(false);
        pausePanel.SetActive(false);
        storePanel.SetActive(false);
        deadPanelDontHaveLife.SetActive(false);
        deadPanelHaveLife.SetActive(false);
        // end Close Others
        //touchButton.SetActive(false);

        quitAlertPanel.SetActive(true);
    }

    public void OpenPausePanel()
    {

        MusicController.instance.PlayButtonClip();

        // Close Others
        messagePanel.SetActive(false);
        quitAlertPanel.SetActive(false);
        pausePanel.SetActive(false);
        storePanel.SetActive(false);
        deadPanelDontHaveLife.SetActive(false);
        deadPanelHaveLife.SetActive(false);
        // end Close Others
        //touchButton.SetActive(false);
        pauseButton.interactable = false;

        Time.timeScale = 0.0f;

        pausePanel.SetActive(true);
    }

    public void OpenStorePanel()
    {

        MusicController.instance.PlayButtonClip();

        // Close Others
        messagePanel.SetActive(false);
        quitAlertPanel.SetActive(false);
        pausePanel.SetActive(false);
        storePanel.SetActive(false);
        deadPanelDontHaveLife.SetActive(false);
        deadPanelHaveLife.SetActive(false);
        // end Close Others
        //touchButton.SetActive(false);

        storePanel.SetActive(true);

    }
    public void OpenDeadPanelDontHaveLife()
    {

        // Close Others
        messagePanel.SetActive(false);
        quitAlertPanel.SetActive(false);
        pausePanel.SetActive(false);
        storePanel.SetActive(false);
        deadPanelDontHaveLife.SetActive(false);
        deadPanelHaveLife.SetActive(false);
        // end Close Others
        //touchButton.SetActive(false);
        scoreContainer.SetActive(false);
        pauseButton.interactable = false;

        deadPanelDontHaveLife.SetActive(true);
    }

    public void OpenDeadPanelHaveLife()
    {

        // Close Others
        messagePanel.SetActive(false);
        quitAlertPanel.SetActive(false);
        pausePanel.SetActive(false);
        storePanel.SetActive(false);
        deadPanelDontHaveLife.SetActive(false);
        deadPanelHaveLife.SetActive(false);
        // end Close Others
        //touchButton.SetActive(false);
        scoreContainer.SetActive(false);
        pauseButton.interactable = false;

        deadPanelHaveLife.SetActive(true);
    }
    public void OpenLevelFinishedPanel()
    {

        // Close Others
        messagePanel.SetActive(false);
        quitAlertPanel.SetActive(false);
        pausePanel.SetActive(false);
        storePanel.SetActive(false);
        deadPanelDontHaveLife.SetActive(false);
        deadPanelHaveLife.SetActive(false);
        // end Close Others
        //touchButton.SetActive(false);
        scoreContainer.SetActive(false);
        pauseButton.interactable = false;
        
    }

    //public void NextLevelButton()
    //{
    //    ++levelId;
    //    SceneManager.LoadScene("Level"+levelId);
    //}

    //


    public void CloseMessagePanel()
    {
        
        MusicController.instance.PlayCloseClip();

        messagePanel.SetActive(false);
        if (lives >= 1)
        {
            OpenDeadPanelHaveLife();
        }
        else
        {
            OpenDeadPanelDontHaveLife();
        }

    }

    public void CloseQuitAlertPanel()
    {
        MusicController.instance.PlayCloseClip();


        quitAlertPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ClosePausePanel()
    {

        MusicController.instance.PlayCloseClip();

        pausePanel.SetActive(false);
        pauseButton.interactable = true;

        Time.timeScale = 1.0f;

        //touchButton.SetActive(true);

    }

    public void CloseStorePanel()
    {

        MusicController.instance.PlayCloseClip();


        storePanel.SetActive(false);
        messagePanel.SetActive(true);

    }
    public void CloseDeadPanelDontHaveLife()
    {
        deadPanelDontHaveLife.SetActive(false);
        pauseButton.interactable = true;
    }

    public void CloseDeadPaneHaveLife()
    {
        deadPanelHaveLife.SetActive(false);
        pauseButton.interactable = true;
    }


    public void OpenFreeLifePanel()
    {
        MusicController.instance.PlayButtonClip();

        // Close Others
        messagePanel.SetActive(false);
        quitAlertPanel.SetActive(false);
        pausePanel.SetActive(false);
        storePanel.SetActive(false);
        deadPanelDontHaveLife.SetActive(false);
        deadPanelHaveLife.SetActive(false);
        // end Close Others
        //touchButton.SetActive(false);
        //freeLifePanel.SetActive(false);
        // end close Other

        freeLifePanel.SetActive(true);
    }

    public void CloseFreeLifePanel()
    {
        MusicController.instance.PlayCloseClip();
        //freeLifePanel.SetActive(false);
        //messagePanel.SetActive(true);

        freeLifePanel.SetActive(false);
        CloseMessagePanel();
    }

    void DeactiveNumberOfTurnForBirdText()
    {
        //numberOfTurnForBirdText.gameObject.SetActive(false);
    }


    public void ActiveNumberOfTurnForBirdText()
    {
        //numberOfTurnForBirdText.gameObject.SetActive(true);
    }
    #endregion 

    public void GoToMainMenu()
    {
        MusicController.instance.PlayButtonClip();

        StartCoroutine(GoToLevel("MainMenu"));
        //SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {

        MusicController.instance.PlayButtonClip();

        StartCoroutine(GoToLevel(SceneManager.GetActiveScene().name));

        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator GoToLevel(string name)
    {
         if (Time.timeScale == 0.0f)
         {
             Time.timeScale = 1.0f;
         }
        float fadeTime = fader.GetComponent<Fading>().BeginFade(1);
        //yield return new WaitForSeconds(fadeTime);
        //yield return new WaitForSeconds(fadeTime);
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds((fadeTime)));
        SceneManager.LoadScene(name);

    }



}
