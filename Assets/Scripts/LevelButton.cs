using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelButton : MonoBehaviour
{


    [SerializeField]
    private Sprite bgON, bgOFF;

    [SerializeField]
    private GameObject labelPanel, starsPanel;

    [SerializeField]
    private Text levelText;

    [SerializeField]
    private bool unlocked;

    [SerializeField]
    private GameObject star1, star2, star3;


    void Start()
    {

        transform.localScale = new Vector3(1, 1, 1);
        gameObject.GetComponent<Button>().interactable = unlocked;
        if (unlocked)
        {
            gameObject.GetComponent<Button>().image.sprite = bgON;
            starsPanel.SetActive(true);
            labelPanel.SetActive(true);
        }
        else
        {
            gameObject.GetComponent<Button>().image.sprite = bgOFF;
            starsPanel.SetActive(false);
            labelPanel.SetActive(false);
        }
    }


    public void SetLevelText(string levelText)
    {
        this.levelText.text = levelText;
    }

    public string GetLevelText()
    {
        return levelText.text;
    }

    public void SetUnlocked(bool value)
    {
        unlocked = value;
    }


    public bool GetUnlocked()
    {
        return unlocked;
    }
    public void SetStars(int progress)
    {
        //print(levelText.text +" "+ stars);

        if (progress == 100)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }
        else if (progress >= 85)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(false);
        }
        else if (progress >= 50)
        {
            star1.SetActive(true);
            star2.SetActive(false);
            star3.SetActive(false);
        }
    }
}
