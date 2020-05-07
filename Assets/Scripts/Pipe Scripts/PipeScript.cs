using UnityEngine;
using System.Collections;

public class PipeScript : MonoBehaviour
{

    [System.Serializable]
    public class Pipe
    {
        public GameObject self;
        //public GameObject location;
        public SpecialPipe.SpecialPipeType type;
        public bool isSingularBirdInput;

        [Range(0.0f, 9.0f)]
        public float speed;

        [Range(0.0f, 9.0f)]
        public float movementRange;

        public bool isFullMoveActive;
        //public int turnCount;

    }

    public static PipeScript instance;

    public Pipe me;
    

    private AudioSource pipeSFX;

    private bool up, down;

    private float minY = -10.2f;

    private float maxY = -9f;

    private Vector3 startPosition;

    private bool fullMovingOnX, limitedMovingOnX;

    private float forwardSpeed, moveRange;

    private float pipeMinX = -14.0f;

    private float pipeMaxX = 14.0f;

    Vector3 tempPossition;

    void Awake()
    {
        pipeSFX = GetComponent<AudioSource>();
        CreateInstance();
    }

    void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    void Start()
    {

        up = true;
        down = false;

        maxY = Random.Range(minY, maxY);

        if (GameSceneController.instance != null)
        {
            if (GameSceneController.instance.steps[0])
            {
                //print("Pipe Steps 0");
                maxY = -9.0f;
            }
        }
        //startPosition = this.transform.position;
        startPosition = me.self.transform.position;

        if (GameSceneControllerLevel.instance != null)
        {

            transform.position = new Vector3(transform.position.x, -14.0f ,transform.position.z);

            //print("START minY " + minY + " maxY " + maxY);
            // SET APROPRIATE VALUES

            //print("PIPE");

            //startPosition = new Vector3(transform.position.x, transform.position.y - 5, transform.position.y);

            //pipeMinX = me.self.transform.position.x;
            //pipeMaxX = me.self.transform.position.x;

            //minY = me.self.transform.position.y;
            //maxY = me.self.transform.position.y+2;

            maxY = startPosition.y; 

            fullMovingOnX = me.isFullMoveActive;
            limitedMovingOnX = !(me.isFullMoveActive);

            forwardSpeed = me.speed;

            moveRange = me.movementRange;

            gameObject.GetComponent<SpecialPipe>().SetCurrentPipeType(me.type);
            gameObject.GetComponent<SpecialPipe>().PrepareCurrentPipeBadge();

            ActiveMovingPipeOnX(me.isFullMoveActive, forwardSpeed, moveRange);

            transform.position = new Vector3(transform.position.x, transform.position.y-2, transform.position.z);

            //print("END minY " + minY + " maxY " + maxY);

        }

        MoveByItween();
    }

    void Update()
    {
        //print(transform.position + " ** " + maxY);
        if (transform.position.y > maxY)
        {
            up = false;
        }

        if (up)
        {
            Up();
        }
        if (down)
        {
            Down();
        }


        if (fullMovingOnX)
        {
            MovingPipeOnX();
        }

        if (limitedMovingOnX)
        {
            //MovePipeOnXWithRadius(moveRange);
        }


    }

    public void MoveByItween()
    {
        if(transform.position.x < 0)
        {
            moveRange *= -1;
        }

        if (GameSceneControllerLevel.instance != null)
        {
            iTween.MoveAdd(transform.parent.gameObject, iTween.Hash("x", 0, "speed", forwardSpeed, "amount", new Vector3(moveRange, 0, 0), "easetype", iTween.EaseType.easeInOutQuad, "looptype", iTween.LoopType.pingPong));
        }
        else if (GameSceneController.instance != null)
        {
            iTween.MoveAdd(gameObject, iTween.Hash("x", 0, "speed", forwardSpeed, "amount", new Vector3(moveRange, 0, 0), "easetype", iTween.EaseType.easeInOutQuad, "looptype", iTween.LoopType.pingPong));
        }

        //iTween.Resume();

    }

    public void RemoveMoveByItween()
    {
        //iTween.Pause(transform.parent.gameObject);
        if (GameSceneControllerLevel.instance != null)
        {
            iTween.Stop(transform.parent.gameObject);
        }
        else if (GameSceneController.instance != null)
        {
            iTween.Stop(gameObject);
        }
    }

    void Up()
    {
        this.gameObject.transform.position += new Vector3(0f, 0.35f, 0f);
        //print("Up");
        //print("Up " + transform.position);
    }

    void Down()
    {
        this.gameObject.transform.position -= new Vector3(0f, 0.3f, 0f);
        //print("Down");
        //print(transform.position);
    }

    public void SetDownValue(bool value)
    {
        //print("Set Down");
        down = value;
        if (value)
        {
            RemoveMoveByItween();
        }
        //StartCoroutine(DestroyGameobject(gameObject));
    }

    IEnumerator DestroyGameobject(GameObject obj)
    {
        yield return new WaitForSeconds(2);
        Destroy(obj);
        //print("DONE______________");
    }


    void OnCollisionEnter2D(Collision2D target)
    {
        
        if (GameController.instance.isMusicOn)
        {
            pipeSFX.Play();
        }


    }

    


    void MovingPipeOnX()
    {
        if (transform.position.x > pipeMaxX)
        {
            forwardSpeed = -forwardSpeed;
        }
        else if (transform.position.x < pipeMinX)
        {
            forwardSpeed = Mathf.Abs(forwardSpeed);
        }

        tempPossition = this.transform.position;
        tempPossition.x += forwardSpeed * Time.deltaTime;
        this.transform.position = tempPossition;

    }

    void MovePipeOnXWithRadius(float radius)
    {

        if (transform.position.x > startPosition.x + radius)
        {
            forwardSpeed = -forwardSpeed;
            if (transform.position.x > pipeMaxX)
            {
                forwardSpeed = -forwardSpeed;
            }
        }
        else if (transform.position.x < startPosition.x - radius)
        {
            forwardSpeed = Mathf.Abs(forwardSpeed);
            if (transform.position.x < pipeMinX)
            {
                forwardSpeed = Mathf.Abs(forwardSpeed);
            }
        }
        Vector3 temp = this.transform.position;
        temp.x += forwardSpeed * Time.deltaTime;
        this.transform.position = temp;


        transform.Translate(radius*forwardSpeed*Time.deltaTime, 0, 0);
                
    }


    public void ActiveMovingPipeOnX(bool fullMove, float speed, float range)
    {
        fullMovingOnX = fullMove;
        limitedMovingOnX = !fullMove;
        forwardSpeed = speed;
        moveRange = range;
    }

}
