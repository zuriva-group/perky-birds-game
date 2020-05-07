using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ExampleAsync : MonoBehaviour {

    IEnumerator Start()
    {
        Debug.Log("Loading Start");
        //AsyncOperation async = Application.LoadLevelAsync("MyBigLevel");
        AsyncOperation async = SceneManager.LoadSceneAsync("GameScene");


        yield return async;
        Debug.Log("Loading complete");
    }
}

