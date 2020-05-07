using UnityEngine;
using System;
using System.Collections;

public class BirdScript : MonoBehaviour
{
    [Serializable]
    public class Bird
    {
        public enum birdsType
        {
            NORMAL,FLAPPY
        }
        public GameObject bird;
        //public GameObject location;
        public int turnCount;

        [Range(6.0f, 9.0f)]
        public float birdForwardSpeed;

        public birdsType type;

    }

    public Bird me;

    [HideInInspector]
    public string CurrentBird;

    public static BirdScript instance;

    [HideInInspector]
    public Rigidbody2D mybody;

    [HideInInspector]
    public Animator anim;

    private AudioSource flySFX, dieSFX;

    [HideInInspector]
    public float forwardSpeed = 10f;

    [HideInInspector]
    public Bird.birdsType type;

    private bool dive;

    private bool canFly;

    private bool flappy;

    private int flappyCount;

    private bool isRight, isLeft;

    private bool canChangeDirection;

    [HideInInspector]
    public bool alive;

    [HideInInspector]
    public int numberOfTurnForBird;

    void Awake()
    {
        CreateInstance();
        flySFX = GetComponent<AudioSource>();
        dieSFX = GetComponent<AudioSource>();
        if (anim == null)
        {
            anim = this.gameObject.GetComponent<Animator>();
        }
        numberOfTurnForBird = 0;
    }



    // Use this for initialization
    void Start()
    {

        flappy = false;
        flappyCount = 3;

        CheckDirection();
        dive = false;
        canFly = true;
        canChangeDirection = true;
        alive = true;

        SetName();

        Fly();

        if (GameSceneControllerLevel.instance != null)
        {
            //print(transform.position.x);

            numberOfTurnForBird = me.turnCount;
            forwardSpeed = me.birdForwardSpeed;
            type = me.type;

            //SetNumberOfTurnForBird(numberOfTurnForBird);


            if (transform.position.x > 0)
            {
                //print("transform.position.x > 0");
                TurnLeft();

                if (transform.position.x>0 && forwardSpeed <0 &&transform.localScale.x>0)
                {
                    //print("FORCE   T L");
                    Vector3 tempScale = transform.localScale;
                    //print(tempScale);
                    if (tempScale.x > 0)
                    {
                        tempScale.x = -(tempScale.x);
                    }
                    //print(tempScale);

                    isRight = false;
                    isLeft = true;

                    transform.localScale = tempScale;
                    Fly();
                }
            }
        }

        /*
        numberOfTurnForBird = GameController.instance.numberOfTurnForBird;
        GameSceneController.instance.RefreshnumberOfTurnForBirdText(numberOfTurnForBird, this.transform.position);
        GameSceneController.instance.ActiveNumberOfTurnForBirdText();
        */

        //if(transform.position.x < 0)
        //{
        //    Vector3 tempScale = transform.localScale;
        //    tempScale.x = -(tempScale.x);
        //    isRight = false;
        //    isLeft = true;
        //    transform.localScale = tempScale;
        //    Fly();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //print(forwardSpeed);

        if (dive && mybody.velocity.y <= 0)
        {

            if (isRight)
            {
                float angle = 0.0f;
                angle = Mathf.Lerp(0, -90, -mybody.velocity.y / 7);
                transform.rotation = Quaternion.Euler(0, 0, angle);

            }
            if (isLeft)
            {

                float angle = 0.0f;
                angle = Mathf.Lerp(0, +90, -mybody.velocity.y / 7);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }

        }
        else if (mybody.velocity.y >= 0 && flappy)
        {
            if (isRight)
            {
                float angle = 0.0f;
                angle = Mathf.Lerp(0, +90, mybody.velocity.y / 7);
                transform.rotation = Quaternion.Euler(0, 0, angle);

            }
            if (isLeft)
            {
                float angle = 0.0f;
                angle = Mathf.Lerp(0, -90, mybody.velocity.y / 7);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
        //else
        //{
        //    //if (alive)
        //    //{
        //    //    if (GameSceneController.instance != null)
        //    //    {
        //    //        GameSceneController.instance.RefreshnumberOfTurnForBirdText(numberOfTurnForBird, this.transform.position);
        //    //    }
        //    //    if (GameSceneControllerLevel.instance != null)
        //    //    {
        //    //        GameSceneControllerLevel.instance.RefreshnumberOfTurnForBirdText(numberOfTurnForBird, this.transform.position);
        //    //    }
        //    //}
        //}
    }


    void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void DoNotRotate()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void StartFly()
    {
        Fly();

    }
    void Fly()
    {
        //print(Time.time);
        mybody.isKinematic = true;
        mybody.isKinematic = false;


        if (canFly)
        {
            //if (flyValue)
            //{
            //mybody.AddForce(new Vector2(50 * forwardSpeed, 0));
            mybody.velocity = new Vector2(1 * forwardSpeed, 0);
            //}

            //if (!flyValue)
            //{

            //}
        }
    }

    public void ForceTurnLeft()
    {
        //print("FTR");
        //print(transform.localScale);
        //print(forwardSpeed);

        Vector3 tempScale = transform.localScale;
        tempScale.x = -(tempScale.x);
        isRight = false;
        isLeft = true;
        forwardSpeed = -forwardSpeed;
        transform.localScale = tempScale;
        Fly();

        //print("FTR");
        //print(transform.localScale);
        //print(forwardSpeed);

        /*
        numberOfTurnForBird--;
        GameSceneController.instance.RefreshnumberOfTurnForBirdText(numberOfTurnForBird);
        CheckForNumberOfTurnForBird();
        */
    }

    public void TurnLeft()
    {
        //print("TL");
        Vector3 tempScale = transform.localScale;

        if (forwardSpeed > 0)
        {
            tempScale.x = -(tempScale.x);

            isRight = false;
            isLeft = true;

            forwardSpeed = -forwardSpeed;
        }

        transform.localScale = tempScale;
        Fly();

        //numberOfTurnForBird--;

        //if (GameSceneController.instance != null)
        //{
        //    GameSceneController.instance.RefreshnumberOfTurnForBirdText(numberOfTurnForBird, this.transform.position);
        //}
        //if (GameSceneControllerLevel.instance != null)
        //{
        //    GameSceneControllerLevel.instance.RefreshnumberOfTurnForBirdText(numberOfTurnForBird, this.transform.position);
        //}

        //CheckForNumberOfTurnForBird();

        //print(forwardSpeed);
        //print(transform.localScale);

    }

    public void TurnRight()
    {
        //print("TR");
        Vector3 tempScale = transform.localScale;

        if (forwardSpeed < 0)
        {
            tempScale.x = Mathf.Abs(tempScale.x);

            isRight = true;
            isLeft = false;

            forwardSpeed = Mathf.Abs(forwardSpeed);
        }

        transform.localScale = tempScale;
        Fly();
        //numberOfTurnForBird--;
        //if (GameSceneController.instance != null)
        //{
        //    GameSceneController.instance.RefreshnumberOfTurnForBirdText(numberOfTurnForBird, this.transform.position);
        //}
        //if (GameSceneControllerLevel.instance != null)
        //{
        //    GameSceneControllerLevel.instance.RefreshnumberOfTurnForBirdText(numberOfTurnForBird, this.transform.position);
        //}
        //CheckForNumberOfTurnForBird();
    }





    // NOT USE ANYMORE !!
    // NOT USE ANYMORE !!
    // NOT USE ANYMORE !!
    void ChangeDirection()
    {
        if (canChangeDirection)
        {
            if (transform.position.x > 11.5f || transform.position.x < -11.5f)
            {

                Vector3 tempScale = transform.localScale;

                if (transform.position.x > 11.5f)
                {
                    //turn left
                    //print("T L");
                    tempScale.x = -(tempScale.x);

                    isRight = false;
                    isLeft = true;

                    forwardSpeed = -forwardSpeed;
                }
                else if (transform.position.x < -11.5f)
                {
                    //turn right
                    //print("T R");
                    tempScale.x = Mathf.Abs(tempScale.x);

                    isRight = true;
                    isLeft = false;

                    forwardSpeed = Mathf.Abs(forwardSpeed);
                }
                transform.localScale = tempScale;
                Fly();
            }
        }


    }

    public void DropBird()
    {


        if (type == Bird.birdsType.NORMAL)
        {
            if (GameController.instance.isMusicOn && !dive)
            {
                flySFX.Play();
            }
        }

        //print("T");
        mybody.isKinematic = false;
        anim.Play("Idle Animation");
        dive = true;

        canChangeDirection = false;

        mybody.constraints = RigidbodyConstraints2D.None;

        if (type == Bird.birdsType.FLAPPY)
        {
            if (flappyCount > 0)
            {
                if (GameController.instance.isMusicOn)
                {
                    flySFX.Play();
                }
                flappy = true;
                //print("flap");

                mybody.velocity = new Vector2(0.7f * mybody.velocity.x, Mathf.Abs(0.9f * forwardSpeed));

                flappyCount--;
            }
        }

    }

    public void SetDive(bool value)
    {
        dive = value;
    }

    public bool GetDive()
    {
        return dive;
    }

    public void DieBird(bool immediately)
    {
        if (GameController.instance.isMusicOn)
        {

        }

        alive = false;
        canFly = false;
        anim.Play("Dead Animation");
        if (immediately)
        {
            DestroyBird(false);
        }
        else
        {
            //StartCoroutine(WaitAndDead(UnityEngine.Random.Range(0.3f, 0.7f)));
            StartCoroutine(WaitAndDead(UnityEngine.Random.Range(0.3f, 0.7f)));
        }

    }

    public void DestroyBird(bool isWon)
    {
        //print(1);
        if (!isWon)
        {
            if (GameSceneController.instance != null)
            {
                GameSceneController.instance.DecreaseHeart();
            }
            //GameSceneController.instance.DecreaseLive();
        }
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    IEnumerator WaitAndDead(float seconds)
    {
        //print(1);
        yield return new WaitForSeconds(seconds);
        DestroyBird(false);
        //print(2);
    }


    void CheckDirection()
    {
        if (transform.localScale.x > 0)
        {
            //RIGHT
            isRight = true;
            isLeft = false;
        }
        else if (transform.localScale.x < 0)
        {
            //LEFT
            isRight = false;
            isLeft = true;
        }
    }

    void CheckForNumberOfTurnForBird()
    {
        //print(numberOfTurnForBird);
        if (numberOfTurnForBird < 1)
        {
            //print("*****************");
            if (GameSceneController.instance != null)
            {
                GameSceneController.instance.DropBird();
            }
            else if (GameSceneControllerLevel.instance != null)
            {
                GameSceneControllerLevel.instance.DropBird();
            }
        }
    }

    public void SetName()
    {
        string name = gameObject.name;

        if (name.Contains("Blue"))
        {
            CurrentBird = GameController.BirdsColor.BLUE.ToString();
        }
        else if (name.Contains("Green"))
        {
            CurrentBird = GameController.BirdsColor.GREEN.ToString();
        }
        else if (name.Contains("Orange"))
        {
            CurrentBird = GameController.BirdsColor.ORANGE.ToString();
        }
        else if (name.Contains("Purple"))
        {
            CurrentBird = GameController.BirdsColor.PURPLE.ToString();
        }
        else if (name.Contains("Red"))
        {
            CurrentBird = GameController.BirdsColor.RED.ToString();
        }
        else if (name.Contains("Yellow"))
        {
            CurrentBird = GameController.BirdsColor.YELLOW.ToString();
        }

    }


    public void SetNumberOfTurnForBird(int value)
    {
        numberOfTurnForBird = value;
        if (GameSceneController.instance != null)
        {
            GameSceneController.instance.RefreshnumberOfTurnForBirdText(numberOfTurnForBird, this.transform.position);
            GameSceneController.instance.ActiveNumberOfTurnForBirdText();
        }
        if (GameSceneControllerLevel.instance != null)
        {
            GameSceneControllerLevel.instance.RefreshnumberOfTurnForBirdText(numberOfTurnForBird, this.transform.position);
            GameSceneControllerLevel.instance.ActiveNumberOfTurnForBirdText();
        }

        //print("Random Val "+ numberOfTurnForBird);
    }
}
