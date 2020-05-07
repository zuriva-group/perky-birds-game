using UnityEngine;
using System.Collections;

public class PipeTargetScript : MonoBehaviour
{

    private string pipeSpecialName;

    private AudioSource targetSFX, achievementSFX , deadSfX;


    private bool singleSFX, doubleSFX, tripleSFX , dead;
    void Awake()
    {
        //pipeSpecialName="";
        targetSFX = GetComponents<AudioSource>()[0];
        achievementSFX = GetComponents<AudioSource>()[1];
        deadSfX = GetComponents<AudioSource>()[2];
    }

    void Update()
    {
        //print(pipeSpecialName);
    }
    void OnCollisionEnter2D(Collision2D target)
    {
        //print("B->"+GameObject.FindGameObjectWithTag("Bird").GetComponent<BirdScript>().alive);
        if (target.gameObject.tag == "Bird" && target.gameObject.GetComponent<BirdScript>().alive)
        {

            //print(target.gameObject.name);
            if (GameSceneController.instance != null)
            {
                if (GameSceneController.instance.isBirdTurnedYet)
                {
                    SetSFXValues(true, false, false,false);
                }
                else
                {
                    SetSFXValues(false, true, false, false);

                }
            }
            if (GameSceneControllerLevel.instance != null)
            {
                if (GameSceneControllerLevel.instance.isBirdTurnedYet)
                {
                    SetSFXValues(true, false, false, false);
                }
                else
                {
                    SetSFXValues(false, true, false, false);

                }

            }

            /*
            *
            *
            *       CONDITION -----> CHECK SINGULARITY TARGET TYPE TRUE
            *
            *
            */
            if (GetCurrentPipeSingularitytarget())
            {
                //print("SINGULARRRRRRRRRR");
                if (pipeSpecialName == target.gameObject.GetComponent<BirdScript>().CurrentBird)
                {

                    //print("SIMILAR");
                    IncreaseGatheredBirdCount(target, 4);
                    if (GameSceneController.instance != null)
                    {
                        GameSceneController.instance.Won(4);
                    }
                    else if (GameSceneControllerLevel.instance != null)
                    {
                        GameSceneControllerLevel.instance.Won(4, target.gameObject);
                    }
                    //gameObject.GetComponentInParent<PipeScript>().SetDownTrue();
                }
                else
                {
                    GameSceneControllerLevel.instance.DieBird(target.gameObject, true);
                    SetSFXValues(false, false, false, true);

                }
            }
            /*
            *
            *
            *       CONDITION -----> CHECK SINGULARITY TARGET TYPE FALSE
            *
            *
            */
            else
            {
                //print("NOT SINGULARRRRRRRRRR");

                if (pipeSpecialName == target.gameObject.GetComponent<BirdScript>().CurrentBird)
                {

                    print("SIMILAR");
                    IncreaseGatheredBirdCount(target, 4);
                    if (GameSceneController.instance != null)
                    {
                        GameSceneController.instance.Won(4);
                    }
                    else if (GameSceneControllerLevel.instance != null)
                    {
                        GameSceneControllerLevel.instance.Won(4, target.gameObject);
                    }
                }




                else if (pipeSpecialName == SpecialPipe.SpecialPipeType.NORMAL.ToString())
                {
                    //print("NORMAL");
                    IncreaseGatheredBirdCount(target, 1);
                    if (GameSceneController.instance != null)
                    {
                        GameSceneController.instance.Won(1);
                    }
                    else if (GameSceneControllerLevel.instance != null)
                    {
                        GameSceneControllerLevel.instance.Won(1, target.gameObject);
                    }
                }
                else if (pipeSpecialName == SpecialPipe.SpecialPipeType.LIFE.ToString())
                {
                    //print("LIFE");
                    IncreaseGatheredBirdCount(target, 1);
                    if (GameSceneController.instance != null)
                    {
                        GameSceneController.instance.Won(1);
                        //StoreController.instance.Get1LiveFromPipe();
                        //GameSceneController.instance.SetHeartFull();
                        StoreController.instance.Get1LiveFromPipe();
                        GameSceneController.instance.SetNumbersToUI();
                    }
                    else if (GameSceneControllerLevel.instance != null)
                    {
                        GameSceneControllerLevel.instance.Won(1, target.gameObject);
                        //StoreController.instance.Get1LiveFromPipe();
                        //GameSceneControllerLevel.instance.SetHeartFull();
                        GameSceneControllerLevel.instance.SetNumbersToUI();
                    }
                }
                else if (pipeSpecialName == SpecialPipe.SpecialPipeType.WING_1.ToString())
                {
                    //print("LIFE");
                    IncreaseGatheredBirdCount(target, 1);
                    if (GameSceneController.instance != null)
                    {
                        GameSceneController.instance.Won(1);
                        GameSceneController.instance.PlusHeartValue(1);
                        GameSceneController.instance.SetNumbersToUI();
                    }
                    else if (GameSceneControllerLevel.instance != null)
                    {
                        GameSceneControllerLevel.instance.Won(1, target.gameObject);
                        //StoreController.instance.Get1LiveFromPipe();
                        //GameSceneControllerLevel.instance.SetHeartFull();
                        GameSceneControllerLevel.instance.SetNumbersToUI();
                    }
                }
                else if (pipeSpecialName == SpecialPipe.SpecialPipeType.WING_2.ToString())
                {
                    //print("LIFE");
                    IncreaseGatheredBirdCount(target, 1);
                    if (GameSceneController.instance != null)
                    {
                        GameSceneController.instance.Won(1);
                        GameSceneController.instance.PlusHeartValue(2);
                        GameSceneController.instance.SetNumbersToUI();
                    }
                    else if (GameSceneControllerLevel.instance != null)
                    {
                        GameSceneControllerLevel.instance.Won(1, target.gameObject);
                        //StoreController.instance.Get1LiveFromPipe();
                        //GameSceneControllerLevel.instance.SetHeartFull();
                        GameSceneControllerLevel.instance.SetNumbersToUI();
                    }
                }
                else if (pipeSpecialName == SpecialPipe.SpecialPipeType.WING_3.ToString())
                {
                    //print("LIFE");
                    IncreaseGatheredBirdCount(target, 1);
                    if (GameSceneController.instance != null)
                    {
                        GameSceneController.instance.Won(1);
                        GameSceneController.instance.PlusHeartValue(3);
                        GameSceneController.instance.SetNumbersToUI();
                    }
                    else if (GameSceneControllerLevel.instance != null)
                    {
                        GameSceneControllerLevel.instance.Won(1, target.gameObject);
                        //StoreController.instance.Get1LiveFromPipe();
                        //GameSceneControllerLevel.instance.SetHeartFull();
                        GameSceneControllerLevel.instance.SetNumbersToUI();
                    }
                }
                else
                {
                    //print("NOT SAME");

                    //IncreaseGatheredBirdCount(target, 1);
                    if (GameSceneController.instance != null)
                    {

                        //GameSceneController.instance.Won(1);
                        GameSceneController.instance.DieBird(target.gameObject, true, true);
                        SetSFXValues(false, false, false, true);
                        GameSceneController.instance.SetNumbersToUI();
                    }
                    else if (GameSceneControllerLevel.instance != null)
                    {
                        GameSceneControllerLevel.instance.Won(1, target.gameObject);
                        //StoreController.instance.Get1LiveFromPipe();
                        //GameSceneControllerLevel.instance.SetHeartFull();
                        //GameSceneControllerLevel.instance.SetNumberToUI();
                    }
                }

                PlayAppropriateSound();
                //transform.parent.gameObject.GetComponent<PipeScript>().SetDownTrue();
                /*
                *
                *
                *
                *   TRY CATCH 
                *               HAS CAME TO AVOID FROM AN UNEXPECTED ERROR
                *
                *
                
                try
                {
                    GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
                    pipes[0].gameObject.GetComponent<PipeScript>().SetDownTrue();
                    if (pipes[1] != null)
                    {
                        pipes[1].gameObject.GetComponent<PipeScript>().SetDownTrue();
                    }
                }
                catch
                {
                    print("ERRROR CAME");
                }
                */

                this.GetComponentInParent<PipeScript>().SetDownValue(true);

                if (GameSceneController.instance != null)
                {
                    GameObject[] allPipes = GameObject.FindGameObjectsWithTag("Pipe");
                    foreach (var pipe in allPipes)
                    {
                        //print(pipe);
                        pipe.GetComponent<PipeScript>().SetDownValue(true);
                    }
                }
            }
        }
    }

    private void PlayAppropriateSound()
    {
        if (GameController.instance.isMusicOn)
        {
            //print("*****************************");

            if (singleSFX)
            {
                //print("111111111111111111111");
                targetSFX.Play();
            }
            else if (doubleSFX)
            {
                //print("222222222222222222222222");

                StartCoroutine(PlaySoundViaWait());
            }
            else if (tripleSFX)
            {
                //print("333333333333333333333");

                achievementSFX.Play();
            }
            else if (dead)
            {
                //print("444444444444444444444444444");

                deadSfX.Play();
            }

            //print("*****************************");

        }
    }



    IEnumerator PlaySoundViaWait()
    {

        targetSFX.Play();
        yield return new WaitForSeconds(0.15f);
        targetSFX.Play();
    }


    private void IncreaseGatheredBirdCount(Collision2D target , int score)
    {
        switch (target.gameObject.name)
        {
            case "Red Bird(Clone)":
                //print("Red " + score);
                GameController.instance.gatheredRedBirds += score;
                if (GameController.instance.gatheredRedBirds == GameController.instance.redBirdsShouldGathered)
                {
                    if (GameSceneController.instance != null)
                    {
                        GameSceneController.instance.ShowFeathers(GameController.BirdsColor.RED);
                        //achievementSFX.Play();
                        SetSFXValues(false, false, true,false);
                        //print("GREAT JOB Red");
                    }else if (GameSceneController.instance != null)
                    {
                        GameSceneControllerLevel.instance.ShowFeathers(GameController.BirdsColor.RED);
                        //achievementSFX.Play();
                        SetSFXValues(false, false, true,false);
                        //print("GREAT JOB Red");
                    }
                }

                break;

            case "Orange Bird(Clone)":
                //print("Orange " + score);

                GameController.instance.gatheredOrangeBirds += score;
                if (GameController.instance.gatheredOrangeBirds == GameController.instance.orangeBirdsShouldGathered)
                {
                    GameSceneController.instance.ShowFeathers(GameController.BirdsColor.ORANGE);
                    SetSFXValues(false, false, true,false);
                    //print("GREAT JOB Orange");
                }
                break;

            case "Yellow Bird(Clone)":
                //print("Yellow " + score);

                GameController.instance.gatheredYellowBirds += score;
                if (GameController.instance.gatheredYellowBirds == GameController.instance.yellowBirdsShouldGathered)
                {
                    GameSceneController.instance.ShowFeathers(GameController.BirdsColor.YELLOW);
                    SetSFXValues(false, false, true,false);
                    //print("GREAT JOB Yellow");
                }
                break;

            case "Blue Bird(Clone)":

                GameController.instance.gatheredBlueBirds += score;

                if (GameController.instance.gatheredBlueBirds == GameController.instance.blueBirdsShouldGathered)
                {
                    GameSceneController.instance.ShowFeathers(GameController.BirdsColor.BLUE);
                    SetSFXValues(false, false, true,false);
                    //print("GREAT JOB Blue");
                }
                //print("Blue " + score);

                break;

            case "Purple Bird(Clone)":
                //print("Purple " + score);

                GameController.instance.gatheredPurpleBirds += score;
                if (GameController.instance.gatheredPurpleBirds == GameController.instance.purpleBirdsShouldGathered)
                {
                    GameSceneController.instance.ShowFeathers(GameController.BirdsColor.PURPLE);
                    SetSFXValues(false, false, true,false);
                    //print("GREAT JOB Purple");
                }
                break;

            case "Green Bird(Clone)":
                //print("Green " + score);

                GameController.instance.gatheredGreenBirds += score;
                if (GameController.instance.gatheredGreenBirds == GameController.instance.greenBirdsShouldGathered)
                {
                    GameSceneController.instance.ShowFeathers(GameController.BirdsColor.GREEN);
                    SetSFXValues(false, false, true,false);
                    //print("GREAT JOB Green");
                }
                break;

            default:
                //print("Default");
                break;

        }

        //print("*****************");
    }

    void SetSFXValues(bool singleSFX, bool doubleSFX, bool tripleSFX , bool dead)
    {
        this.singleSFX = singleSFX;
        this.doubleSFX = doubleSFX;
        this.tripleSFX = tripleSFX;
        this.dead = dead;
    }


    public void SetSpecialTarget(SpecialPipe.SpecialPipeType value)
    {
        //print("SetSpecialTarget Start : " + value);
        switch (value)
        {
            case SpecialPipe.SpecialPipeType.BLUE:
                pipeSpecialName = SpecialPipe.SpecialPipeType.BLUE.ToString();
                break;
            case SpecialPipe.SpecialPipeType.GREEN:
                pipeSpecialName = SpecialPipe.SpecialPipeType.GREEN.ToString();
                break;
            case SpecialPipe.SpecialPipeType.LIFE:
                pipeSpecialName = SpecialPipe.SpecialPipeType.LIFE.ToString();
                break;
            case SpecialPipe.SpecialPipeType.NORMAL:
                pipeSpecialName = SpecialPipe.SpecialPipeType.NORMAL.ToString();
                break;
            case SpecialPipe.SpecialPipeType.ORANGE:
                pipeSpecialName = SpecialPipe.SpecialPipeType.ORANGE.ToString();
                break;
            case SpecialPipe.SpecialPipeType.PURPLE:
                pipeSpecialName = SpecialPipe.SpecialPipeType.PURPLE.ToString();
                break;
            case SpecialPipe.SpecialPipeType.RED:
                pipeSpecialName = SpecialPipe.SpecialPipeType.RED.ToString();
                break;
            case SpecialPipe.SpecialPipeType.YELLOW:
                pipeSpecialName = SpecialPipe.SpecialPipeType.YELLOW.ToString();
                break;
            case SpecialPipe.SpecialPipeType.WING_1:
                pipeSpecialName = SpecialPipe.SpecialPipeType.WING_1.ToString();
                break;
            case SpecialPipe.SpecialPipeType.WING_2:
                pipeSpecialName = SpecialPipe.SpecialPipeType.WING_2.ToString();
                break;
            case SpecialPipe.SpecialPipeType.WING_3:
                pipeSpecialName = SpecialPipe.SpecialPipeType.WING_3.ToString();
                break;
        }

        //print("SetSpecialTarget End : " + value);

    }

    bool GetCurrentPipeSingularitytarget()
    {
        return GetComponentInParent<PipeScript>().me.isSingularBirdInput;
    }
}
