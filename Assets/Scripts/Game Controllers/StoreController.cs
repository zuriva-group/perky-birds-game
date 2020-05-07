using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
//using Soomla.Store;

public class StoreController : MonoBehaviour
{
    public static StoreController instance;

    //public static StoreEventHandler handler;


    // [SerializeField]
    // private Button buy20Lives, buy50Lives, buy100Lives, buy200Lives, buy500Lives, buy1000Lives;

    private long LivesCount;

    // Use this for initialization

    void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        /*
        if (handler == null)
        {
            handler = new StoreEventHandler();
            //SoomlaStore.Initialize(new StoreAssets());
        }
        */
        LivesCount = GameController.instance.lives;
        //print(LivesCount);
    }

    // Update is called once per frame
    void Update()
    {
        //print("Uuuuuuuuu    " + LivesCount);
    }
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }

    }


    public void Buy3LivesFromStore()
    {
        MusicController.instance.PlayButtonClip();
        LivesCount += 3;
        RefreshUI();
    }

    public void Get1LiveFromPipe()
    {
        //MusicController.instance.PlayButtonClip();
        LivesCount ++;
        RefreshUI();
    }

    public void Buy20LivesFromStore()
    {
        MusicController.instance.PlayButtonClip();

        //StoreInventory.BuyItem("Life_20");

        /*LivesCount += 20;
        RefreshUI();*/
    }

    public void Buy50LivesFromStore()
    {
        MusicController.instance.PlayButtonClip();

        //StoreInventory.BuyItem("Life_50");


        /*LivesCount += 50;
        RefreshUI();*/
    }
    public void Buy100LivesFromStore()
    {
        MusicController.instance.PlayButtonClip();

        //StoreInventory.BuyItem("Life_100");


        /*LivesCount += 100;
        RefreshUI();*/
    }
    public void Buy200LivesFromStore()
    {
        MusicController.instance.PlayButtonClip();

        //StoreInventory.BuyItem("Life_200");


        /*LivesCount += 200;
        RefreshUI();*/

    }
    public void Buy500LivesFromStore()
    {
        MusicController.instance.PlayButtonClip();

        //StoreInventory.BuyItem("Life_500");


        /*LivesCount += 500;
        RefreshUI();*/
    }
    public void Buy1000LivesFromStore()
    {
        MusicController.instance.PlayButtonClip();

        //StoreInventory.BuyItem("Life_1000");


        /*LivesCount += 1000;
        RefreshUI();*/
    }

    public void Get3lifeFromVideoAd()
    {
        AdsController.instance.ShowChartboostVideo();
    }

    private void RefreshUI()
    {

        //print(LivesCount);
        GameController.instance.lives = LivesCount;
        GameController.instance.Save();

        if (GameSceneController.instance!=null)
        {
            //print("GameScene");
            GameSceneController.instance.LoadAllValues();
            GameSceneController.instance.SetNumbersToUI();
            //GameSceneController.instance.SetHeartFull();

        }
        else if (GameSceneControllerLevel.instance != null)
        {
            //print("GameSceneControllerLevel");
            GameSceneControllerLevel.instance.LoadAllValues();
            GameSceneControllerLevel.instance.SetNumbersToUI();
        }
        else if (LevelSelectSceneController.instance != null)
        {
            //print("LevelSelectSceneController");
            LevelSelectSceneController.instance.LoadAllValues();
            LevelSelectSceneController.instance.SetNumbersToUI();
        }
        else if (SurvivalLevelSelectSceneController.instance != null)
        {
            //print("SurvivalLevelSelectSceneController");
            SurvivalLevelSelectSceneController.instance.LoadAllValues();
            SurvivalLevelSelectSceneController.instance.SetNumbersToUI();
        }
        else if (WorldSceneController.instance != null)
        {
            //print("WorldSceneController");
            WorldSceneController.instance.LoadAllValues();
            WorldSceneController.instance.SetNumbersToUI();
        }
        else if (MainMenuController.instance != null)
        {
            //print("WorldSceneController");
            MainMenuController.instance.LoadAllValues();
            MainMenuController.instance.SetNumbersToUI();
        }


    }

    public void SetLivesZero()
    {
        LivesCount = 0;
    }

    public void LivePlusValue(int value)
    {
        LivesCount += value;
        RefreshUI();
    }
}