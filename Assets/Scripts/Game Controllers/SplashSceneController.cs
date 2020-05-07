using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashSceneController : MonoBehaviour {


    //[SerializeField]
    //private GameObject fader;

    [SerializeField]
    private float waitSeconds = 2;

    // Use this for initialization
    void Start () {
        StartCoroutine(GoToLevel("MainMenu"));
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}


    IEnumerator GoToLevel(string name)
    {
        yield return new WaitForSeconds(waitSeconds);
        //float fadeTime = fader.GetComponent<Fading>().BeginFade(1);
        //yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(name);
    }


}
