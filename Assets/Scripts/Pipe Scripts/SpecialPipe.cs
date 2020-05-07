using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpecialPipe : MonoBehaviour
{

    [SerializeField]
    private GameObject blue, green, orange, purple, red, yellow, life , wing_1, wing_2, wing_3;

    public enum SpecialPipeType
    {
        NORMAL,
        BLUE,
        GREEN,
        ORANGE,
        PURPLE,
        RED,
        YELLOW,
        LIFE,
        WING_1,
        WING_2,
        WING_3

    }

    [HideInInspector]
    public List<SpecialPipeType> pipeList = new List<SpecialPipeType>();

    void Awake()
    {

    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCurrentPipeType(SpecialPipeType value)
    {
        pipeList.Add(value);
    }

    public void PrepareCurrentPipeBadge()
    {
        SetSpecialbadge();
    }

    void SetSpecialbadge()
    {
        DeactiveAll();


        switch (pipeList[0])
        {
            case SpecialPipeType.BLUE:
                blue.SetActive(true);
                break;
            case SpecialPipeType.GREEN:
                green.SetActive(true);
                break;
            case SpecialPipeType.LIFE:
                life.SetActive(true);
                break;
            case SpecialPipeType.NORMAL:
                break;
            case SpecialPipeType.ORANGE:
                orange.SetActive(true);
                break;
            case SpecialPipeType.PURPLE:
                purple.SetActive(true);
                break;
            case SpecialPipeType.RED:
                red.SetActive(true);
                break;
            case SpecialPipeType.YELLOW:
                yellow.SetActive(true);
                break;
            case SpecialPipeType.WING_1:
                wing_1.SetActive(true);
                break;
            case SpecialPipeType.WING_2:
                wing_2.SetActive(true);
                break;
            case SpecialPipeType.WING_3:
                wing_3.SetActive(true);
                break;
        }

        gameObject.GetComponentInChildren<PipeTargetScript>().SetSpecialTarget(pipeList[0]);

    }

    void DeactiveAll()
    {
        blue.SetActive(false);
        green.SetActive(false);
        orange.SetActive(false);
        purple.SetActive(false);
        red.SetActive(false);
        yellow.SetActive(false);
        life.SetActive(false);
        wing_1.SetActive(false);
        wing_2.SetActive(false);
        wing_3.SetActive(false);
    }
}
