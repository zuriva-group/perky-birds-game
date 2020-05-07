using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class LeaderboardsController : MonoBehaviour
{

    private string textOfStatus;

    public static LeaderboardsController instance;

    void Awake()
    {
        MakeSingleton();
    }


    // Use this for initialization
    void Start()
    {
        textOfStatus = "Started";
        PlayGamesPlatform.Activate();

        //if (GameController.instance.isConnectionAvailible)
        //{
        //    textOfStatus += "Have Connection";
        StartCoroutine(StartLogIn());
        //}
    }

    //void OnGUI()
    //{
        
    //    int w = Screen.width, h = Screen.height;

    //    GUIStyle style = new GUIStyle();

    //    Rect rect = new Rect(0, 0, w, h * 2 / 100);
    //    style.alignment = TextAnchor.UpperLeft;
    //    style.fontSize = h * 2 / 30;
    //    style.normal.textColor = new Color(0.0f, 1.0f, 1.0f, 1.0f);

    //    //print("onGUI");
         
    //    GUI.Label(rect, textOfStatus, style);
        
    //}

    void OnLevelWasLoaded()
    {
        //textOfStatus = "OnLevelWasLoaded";

        //Splash Screen Scene
        //Main Menu Scene

        //StartCoroutine(GameController.instance.CheckInternetConnection((isConnected) =>
        //{
        //    if (isConnected)
        //    {
        //        textOfStatus = "isConnected";

        //        if (SceneManager.GetActiveScene().name == "Splash Screen Scene" || SceneManager.GetActiveScene().name == "Main Menu Scene")
        //        {
        //            textOfStatus = "Splash Screen Scene";
        //            LogIn();
        //        }
        //        else
        //        {
        //            textOfStatus = "Not LogIn";
        //        }
        //    }
        //}));

        //StartCoroutine(StartLogIn());

        PostScore();
    }

    void MakeSingleton()
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

    // Update is called once per frame
    void Update()
    {

    }


    public void OpenLeaderboardsScore()
    {
        PostScore();

        if (Social.localUser.authenticated)
        {
            //connected to goolePlay Services
            Social.ShowLeaderboardUI();
        }
        else
        {
            Social.localUser.Authenticate((bool success) =>
            {
                //Handle success or falture 
                if (success)
                {
                    //logged In
                    Social.ShowLeaderboardUI();
                }
                else
                {
                    //Fail To Log In
                }
            });
        }
    }


    public void PostScore()
    {
        long score = GameController.instance.maxScore;

        if (Social.localUser.authenticated)
        {
            // if we r connected to googlePlay Services...
            // we should Post our Score
            Social.ReportScore(score, GPGSIds.leaderboard_test, (bool success) =>
            {
                if (success)
                {
                    // success Post Score To LeaderBord
                    textOfStatus = "Succes Post";
                }
                else
                {
                    // Fail 
                }
            });
        }
    }

    IEnumerator StartLogIn()
    {
        yield return new WaitForSeconds(0.1f);
        LogIn();

    }

    public void LogIn()
    {

        textOfStatus = "LogIn Started";

        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool success) =>
            {
                if (success)
                {
                    textOfStatus = "You've successfully logged in";
                }
                else
                {
                    textOfStatus = "Login failed for some reason";
                }
            });
        }
        else
        {
            textOfStatus = "Stil LogIn";
        }
    }

    public void SignOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
        Debug.Log("SignOut");
    }
}
