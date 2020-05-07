using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicController : MonoBehaviour
{

    public static MusicController instance;

    //[SerializeField]
    private AudioSource bgMusic, clickClip, closeClip;

    //private AudioSource diveClip1 , diveClip2, diveClip3, diveClip4;

    // private  AudioSource  birdsClip1, birdsClip2, birdsClip3, birdsClip4, birdsClip5, birdsClip6, birdsClip7;
    //private ArrayList<AudioSource>() birdsClips;

    private ArrayList diveClips = new ArrayList();

    private ArrayList birdClips = new ArrayList();




    private static float time;

    AudioSource[] musics;

    void Awake()
    {
        MakeSingleTone();


        musics = GetComponents<AudioSource>();
        bgMusic = musics[0];
        clickClip = musics[1];
        closeClip = musics[2];
        /*
                diveClip1 = musics[3];
                diveClip2 = musics[4];
                diveClip3 = musics[5];
                diveClip4 = musics[6];

                birdsClip1 = musics[7];
                birdsClip2 = musics[8];
                birdsClip3 = musics[9];
                birdsClip4 = musics[10];
                birdsClip5 = musics[11];
                birdsClip6 = musics[12];
                birdsClip7 = musics[13];
                */

        for (int i = 3; i < 7; i++)
        {
            diveClips.Add(musics[i]);
        }



        for (int i = 7; i < musics.Length; i++)
        {
            birdClips.Add(musics[i]);
        }
        /*AudioSource[] musics = GetComponents<AudioSource>();
        bgMusic = musics[0];*/
    }


    // Use this for initialization
    void Start()
    {
        //bgMusic.seta


    }

    // Update is called once per frame
    void Update()
    {
        //time = bgMusic.time;
        //print(time);

        // PlayDiveClip();
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

    void OnLevelWasLoaded()
    {
        PlayMusicFromTime();

    }


    void PlayMusicFromTime()
    {
        bool isplay = false;

        //print("NAMEEEEEEEEEEEEEEEEEEEEEEEE: " + musics.Length);


        if (SceneManager.GetActiveScene().name == "MainMenu")
        {

            //print("---------->>"+GameController.instance.isMusicOn);
            if (GameController.instance.isMusicOn)
            {
                if (!bgMusic.isPlaying)
                {
                    bgMusic.time = time;
                    bgMusic.Play();
                    //print(bgMusic.isPlaying);
                    isplay = bgMusic.isPlaying;

                }
            }
        }
        //print("---------->>" + GameController.instance.isMusicOn);
        //print(isplay + "");

    }



    public void LevelIsLoadedTurnOfMusic()
    {

        if (bgMusic.isPlaying)
        {
            //print("M2");

            time = bgMusic.time;
            bgMusic.Stop();

            //print(bgMusic.time);
            //print("M3");

        }
        //print("bgTm: " + bgMusic.time);
        //print("Time: "+time);
    }


    public void StopBGMusic()
    {
        if (bgMusic.isPlaying)
        {
            bgMusic.Stop();
        }
    }

    public void PlayBGMusic()
    {
        if (!bgMusic.isPlaying)
        {
            bgMusic.Play();
            //PlayMusicFromTime();
        }
    }


    public void PlayButtonClip()
    {
        if (GameController.instance.isMusicOn)
        {
            clickClip.Play();
        }
    }

    public void ForcePlayButtonClip()
    {
        //if (GameController.instance.isMusicOn)
        //{
        clickClip.Play();
        //}
    }


    public void PlayCloseClip()
    {
        if (GameController.instance.isMusicOn)
        {
            closeClip.Play();
        }
    }




    public void PlayDiveClip()
    {
        int a = Random.Range(0, diveClips.Count);
        //print(diveClips.Length);
        //print(a);
        AudioSource diveClip = diveClips[a] as AudioSource;
        diveClip.Play();
        //print("///////////////////////////////////////////");


        //print(birdClips);


        // print("///////////////////////////////////////////");
    }
    public void PlayBirdClip()
    {
        int a = Random.Range(0, birdClips.Count);
        //print(diveClips.Length);
        //print(a);
        AudioSource birdClip = birdClips[a] as AudioSource;
        birdClip.Play();
        //print("///////////////////////////////////////////");


        //print(birdClips);


        // print("///////////////////////////////////////////");
    }


}
