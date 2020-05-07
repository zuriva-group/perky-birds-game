using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;

public class LoadingController : MonoBehaviour
{

    public static LoadingController instance;

    [SerializeField]
    private bool LoadImmediately;

    [SerializeField]
    private GameObject LoadingCanvas;

    //[SerializeField]
    //private GameObject ButtonCanvas, LoadingSprite, tapToStartSprite;

    [SerializeField]
    private Slider progressBar;

    //[SerializeField]
   //private Button TouchButton;

    AsyncOperation ao;

   



    // Use this for initialization
    void Start()
    {
        
        //print("1");
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void LoadLevel(string SceneName)
    {
        //print("2");
        //ButtonCanvas.SetActive(true);
        //print(22);
        LoadingCanvas.SetActive(true);

        StartCoroutine(LoadLevelWithRealProgress(SceneName));


        //print("3");

    }

    IEnumerator LoadLevelWithRealProgress(string name)
    {
        //print("4");

        yield return new WaitForSeconds(1);

        ao = SceneManager.LoadSceneAsync(name);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {

            progressBar.value = ao.progress;

            if (ao.progress == 0.9f)
            {
                progressBar.value = 1.0f;

                if (LoadImmediately)
                {
                    //TouchButton.GetComponent<Button>().interactable = false;
                    if (MainMenuController.instance != null)
                    {
                        StartCoroutine(MainMenuController.instance.BeginFade(true, 1.5f));
                    }
                    else if (LevelSelectSceneController.instance != null)
                    {
                        StartCoroutine(LevelSelectSceneController.instance.BeginFade(true, 1.5f));
                    }
                    else if (GameSceneControllerLevel.instance != null)
                    {
                        StartCoroutine(GameSceneControllerLevel.instance.BeginFade(true, 1.5f));
                    }
                    else if (SurvivalLevelSelectSceneController.instance != null)
                    {
                        StartCoroutine(SurvivalLevelSelectSceneController.instance.BeginFade(true, 1.5f));
                    }
                    else if (WorldSceneController.instance != null)
                    {
                        StartCoroutine(WorldSceneController.instance.BeginFade(true, 1.5f));
                    }
                }
                else
                {
                    //LoadingSprite.SetActive(false);
                    //tapToStartSprite.SetActive(true);
                    //TouchButton.GetComponent<Button>().interactable = true;
                }


                

                //if (Input.GetKeyDown(KeyCode.F))
                //if(Input.GetTouch(0).phase == TouchPhase.Began)
                //{
                    //print("TOUCHED");


                    //float fadeTime = fader.GetComponent<Fading>().BeginFade(1);
                    //yield return new WaitForSeconds(fadeTime);

                    //MainMenuController.instance.BeginFade();

                    //

                    //GameSceneController.instance.RestartGame();

                    //GoToLevel(name);

                //}
            }

            //print(ao.progress);
            yield return null;

        }

        print("End");
    }




    /*
    ------------------------- BEGIN ----------------------------------------
    DID NOT SHOW BOUTH FADES
    USE THIS WAY TO SOLVE IT
    SEND FROM LoadingController.cs TO mainMenuController.cs
    AND
    SEND FROM MainMenuController.cs TO LoadingController.cs
    */
    public void TouchButtonFunction()
    {
        //print("Yesss 1");
        MusicController.instance.PlayButtonClip();
        StartCoroutine(MainMenuController.instance.BeginFade(true , 0.0f));

    }


    public void SetAllowSceneActivationTrue()
    {
        //print(2);
        ao.allowSceneActivation = true;
        //print("Yes");
    }

    /*
    DID NOT SHOW BOUTH FADES
    USE THIS WAY TO SOLVE IT
    SEND FROM LoadingController.cs TO mainMenuController.cs
    AND
    SEND FROM MainMenuController.cs TO LoadingController.cs
    ------------------------- END ----------------------------------------
    */
}
