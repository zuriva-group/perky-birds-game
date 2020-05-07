using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour {

    public static FPSDisplay instance;


    float deltaTime = 0.0f;

    void Awake()
    {
        MakeSingleTone();
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

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(10, 20, w, h * 2 / 100);
        
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = 2*h * 2 / 50;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        //string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        //string text = string.Format("PERKY BIRDS --> ({1:0.} fps)", msec, fps);
        string text = string.Format("PERKY BIRDS --> TEST VERSION :)");
        GUI.Label(rect, text, style);
    }

}
