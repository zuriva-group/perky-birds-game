using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;


public class LevelManager : MonoBehaviour
{

    [System.Serializable]
    public class Level
    {

        public int id;
        public bool unlocked;
        public int progress;

        public Level(int id, bool unlocked, int progress)
        {
            this.id = id;
            this.unlocked = unlocked;
            this.progress = progress;
        }
    }

    public GameObject levelButton;
    public Transform spacer;

    public List<Level> levelList;



    // Use this for initialization
    void Start()
    {
        if (GameController.instance.fillFromLevelManager)
        {
            GameController.instance.levels.Clear();
            GameController.instance.levels.AddRange(levelList);
            GameController.instance.fillFromLevelManager = false;
        }
        else
        {
            levelList = new List<Level>();
            levelList.AddRange(GameController.instance.levels);
        }

        GameController.instance.Save();
        FillList();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FillList()
    {
        int selectedWorld = GameController.instance.selectedWorld;

        
        //print(levelList.Count);
        /*
        *
        *       SHOULD FIX
        *                   NOT GOOD FORMULA
        *
        *
        */
        for (int i = ((selectedWorld - 1) * 15); i < (15 * selectedWorld); i++)
        {
            GameObject btn = Instantiate(levelButton) as GameObject;

            LevelButton button = btn.GetComponent<LevelButton>();
            button.SetLevelText(levelList[i].id.ToString());

            button.SetStars(levelList[i].progress);
                        
            button.SetUnlocked(levelList[i].unlocked);
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (GameController.instance.lives > 1268)
                {
                    LevelSelectSceneController.instance.OpenScene("Level" + button.GetLevelText(), true);
                }
                else
                {
                    LevelSelectSceneController.instance.OpenFreeLifePanel();
                }
                //SceneManager.LoadScene("Level" + button.GetLevelText());
            });

            btn.transform.SetParent(spacer);
        }

        /*
        int i = 0;
        foreach (var level in levelList)
        {

            GameObject btn = Instantiate(levelButton) as GameObject;

            LevelButton button = btn.GetComponent<LevelButton>();
            button.SetLevelText(level.id.ToString());

            button.SetStars(level.stars);

            i++;

            button.SetUnlocked(level.unlocked);
            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                LevelSelectSceneController.instance.OpenScene("Level" + button.GetLevelText(), true);

            });

            btn.transform.SetParent(spacer);
        }*/
    }

}
