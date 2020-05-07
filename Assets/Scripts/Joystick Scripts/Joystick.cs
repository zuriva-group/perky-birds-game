using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class Joystick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    //[SerializeField]
    //private GameObject gameSceneController;

    [HideInInspector]
    public long touchCount;

    private bool canTouch;

    void Awake()
    {
        touchCount = 0;
        canTouch = true;
    }

    public void OnPointerDown(PointerEventData data)
    {
        //print("down");
        if (canTouch)
        {
            touchCount++;
            if (GameSceneController.instance != null)
            {
                GameSceneController.instance.DropBird();
            }
            else if (GameSceneControllerLevel.instance != null)
            {
                GameSceneControllerLevel.instance.DropBird();
            }
            //print("if");

            StartCoroutine(WaitAndEnableTouch(0.4f));
        }
    }


    public void OnPointerUp(PointerEventData data)
    {
       
    }

    IEnumerator WaitAndEnableTouch(float value)
    {
        canTouch = false;
        yield return new WaitForSeconds(value);
        canTouch = true;

    }
    //void Update()
    //{
    //    print(canTouch);
    //}
}
