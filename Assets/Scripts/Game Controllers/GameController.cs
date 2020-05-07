using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;


public class GameController : MonoBehaviour
{
    public static GameController instance;

    public enum BirdsColor
    {
        BLUE,
        GREEN,
        ORANGE,
        PURPLE,
        RED,
        YELLOW
    };

    private GameData data;

    //public int numberOfTurnForBird;

    public long lives;
    public long maxScore;
    public bool isGameStartedForTheFirstTime;
    public bool isMusicOn;
    public bool fillFromLevelManager;


    public long gatheredRedBirds, gatheredOrangeBirds, gatheredYellowBirds, gatheredBlueBirds, gatheredPurpleBirds, gatheredGreenBirds;



    public long blueBirdsShouldGathered, greenBirdsShouldGathered, orangeBirdsShouldGathered, purpleBirdsShouldGathered, redBirdsShouldGathered, yellowBirdsShouldGathered;


    public long currentScore;

    public bool isConnectionAvailible;

    public List<LevelManager.Level> levels;


    public int selectedWorld;


    void Awake()
    {
        Application.targetFrameRate = 60;
        MakeSingleTone();
        InitializeGameVariables();

        //print(Application.persistentDataPath);
    }

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        
        redBirdsShouldGathered = 1000;
        orangeBirdsShouldGathered = 900;
        yellowBirdsShouldGathered = 800;
        blueBirdsShouldGathered = 700;
        purpleBirdsShouldGathered = 600;
        greenBirdsShouldGathered = 500;

        selectedWorld = 1;

        /*
        redBirdsShouldGathered = 1000;
        orangeBirdsShouldGathered = 900;
        yellowBirdsShouldGathered = 800;
        blueBirdsShouldGathered = 700;
        purpleBirdsShouldGathered = 600;
        greenBirdsShouldGathered = 500;
        */
    }
    void OnLevelWasLoaded()
    {
        StartCoroutine(CheckInternetConnection((isConnected) =>
        {
            if (isConnected)
            {
                //print("HAVE INTERNET");
            }
            else
            {
                //print("DONT HAVE INTERNET");

            }

            isConnectionAvailible = isConnected;
        }));
        
        AdsController.instance.Initialize();

    }


    void MakeSingleTone()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public IEnumerator CheckInternetConnection(Action<bool> action)
    {
        WWW www = new WWW("http://google.com");
        yield return www;
        if (www.error != null)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }

    void InitializeGameVariables()
    {
        Load();

        if (data != null)
        {
            isGameStartedForTheFirstTime = data.GetIsGameStartedForTheFirstTime();
        }
        else
        {
            isGameStartedForTheFirstTime = true;
            //print(isGameStartedForTheFirstTime);
        }


        //
        if (isGameStartedForTheFirstTime)
        {
            isGameStartedForTheFirstTime = false;
            isMusicOn = true;
            
            lives = 1278;
            //lives = 2268;

            maxScore = 0;
            currentScore = 0;

            levels = new List<LevelManager.Level>();


            gatheredBlueBirds = 0;
            gatheredGreenBirds = 0;
            gatheredOrangeBirds = 0;
            gatheredPurpleBirds = 0;
            gatheredRedBirds = 0;
            gatheredYellowBirds = 0;

            fillFromLevelManager = true;

            data = new GameData();

            data.SetIsGameStartedForTheFirstTime(isGameStartedForTheFirstTime);
            data.SetIsMusicOn(isMusicOn);
            data.SetLives(lives);
            data.SetMaxScore(maxScore);

            data.SetGatheredBlueBirds(gatheredBlueBirds);
            data.SetGatheredGreenBirds(gatheredGreenBirds);
            data.SetGatheredOrangeBirds(gatheredOrangeBirds);
            data.SetGatheredPurpleBirds(gatheredPurpleBirds);
            data.SetGatheredRedBirds(gatheredRedBirds);
            data.SetGatheredYellowBirds(gatheredYellowBirds);

            Save();

            Load();
        }
        else
        {
            lives = data.GetLives();
            maxScore = data.GetMaxScore();
            isGameStartedForTheFirstTime = data.GetIsGameStartedForTheFirstTime();
            isMusicOn = data.GetIsMusicOn();

            levels = data.GetLevelLists();

            fillFromLevelManager = data.GetFillFromLevelManager();

            gatheredBlueBirds = data.GetGatheredBlueBirds();
            gatheredGreenBirds = data.GetGatheredGreenBirds();
            gatheredOrangeBirds = data.GetGatheredOrangeBirds();
            gatheredPurpleBirds = data.GetGatheredPurpleBirds();
            gatheredRedBirds = data.GetGatheredRedBirds();
            gatheredYellowBirds = data.GetGatheredYellowBirds();

        }
    }

    public void Save()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + "/GameData.dat");

            if (data != null)
            {
                // set Object From This Class to GameData object
                data.SetIsGameStartedForTheFirstTime(isGameStartedForTheFirstTime);
                data.SetIsMusicOn(isMusicOn);
                data.SetLives(lives);
                data.SetMaxScore(maxScore);

                data.SetFillFromLevelManager(fillFromLevelManager);

                data.SetFillFromLevelManager(fillFromLevelManager);

                data.SetGatheredBlueBirds(gatheredBlueBirds);
                data.SetGatheredGreenBirds(gatheredGreenBirds);
                data.SetGatheredOrangeBirds(gatheredOrangeBirds);
                data.SetGatheredPurpleBirds(gatheredPurpleBirds);
                data.SetGatheredRedBirds(gatheredRedBirds);
                data.SetGatheredYellowBirds(gatheredYellowBirds);


                data.SetLevelToList(levels);


                bf.Serialize(file, data);
            }

        }
        catch (Exception ex)
        {
            print(ex.Message);
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }

    }

    public void Load()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);
            data = (GameData)bf.Deserialize(file);
        }
        catch (Exception ex)
        {
            //print(ex.Message);
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }
}

[Serializable]
class GameData
{
    private bool isGameStartedForTheFirstTime;

    private bool isMusicOn;

    private bool fillFromLevelManager;

    private long lives;

    private long maxScore;

    private long gatheredRedBirds, gatheredOrangeBirds, gatheredYellowBirds, gatheredBlueBirds, gatheredPurpleBirds, gatheredGreenBirds;

    private List<LevelManager.Level> levels = new List<LevelManager.Level>();



    public void SetIsGameStartedForTheFirstTime(bool isGameStartedForTheFirstTime)
    {
        this.isGameStartedForTheFirstTime = isGameStartedForTheFirstTime;
    }

    public bool GetIsGameStartedForTheFirstTime()
    {
        return isGameStartedForTheFirstTime;
    }

    public void SetIsMusicOn(bool isMusicOn)
    {
        this.isMusicOn = isMusicOn;
    }

    public bool GetIsMusicOn()
    {
        return isMusicOn;
    }

    public void SetLives(long lives)
    {
        this.lives = lives;
    }

    public long GetLives()
    {
        return lives;
    }

    public void SetMaxScore(long maxScore)
    {
        this.maxScore = maxScore;
    }

    public long GetMaxScore()
    {
        return maxScore;
    }




    public void SetGatheredBlueBirds(long gatheredBlueBirds)
    {
        this.gatheredBlueBirds = gatheredBlueBirds;
    }
    public long GetGatheredBlueBirds()
    {
        return gatheredBlueBirds;
    }

    public void SetGatheredGreenBirds(long gatheredGreenBirds)
    {
        this.gatheredGreenBirds = gatheredGreenBirds;
    }
    public long GetGatheredGreenBirds()
    {
        return gatheredGreenBirds;
    }
    public void SetGatheredOrangeBirds(long gatheredOrangeBirds)
    {
        this.gatheredOrangeBirds = gatheredOrangeBirds;
    }
    public long GetGatheredOrangeBirds()
    {
        return gatheredOrangeBirds;
    }


    public void SetGatheredPurpleBirds(long gatheredPurpleBirds)
    {
        this.gatheredPurpleBirds = gatheredPurpleBirds;
    }
    public long GetGatheredPurpleBirds()
    {
        return gatheredPurpleBirds;
    }
    public void SetGatheredRedBirds(long gatheredRedBirds)
    {
        this.gatheredRedBirds = gatheredRedBirds;
    }
    public long GetGatheredRedBirds()
    {
        return gatheredRedBirds;
    }
    public void SetGatheredYellowBirds(long gatheredYellowBirds)
    {
        this.gatheredYellowBirds = gatheredYellowBirds;
    }
    public long GetGatheredYellowBirds()
    {
        return gatheredYellowBirds;
    }

    public void SetFillFromLevelManager(bool fillFromLevelManager)
    {
        this.fillFromLevelManager = fillFromLevelManager;
    }

    public bool GetFillFromLevelManager()
    {
        return fillFromLevelManager;
    }

    public void SetLevelToList(List<LevelManager.Level> levelss)
    {
        //this.levels.Clear();
        //this.levels.AddRange(levelss);

        levels = new List<LevelManager.Level>();
        levels.AddRange(levelss);
    }

    public List<LevelManager.Level> GetLevelLists()
    {
        return levels;
    }

}