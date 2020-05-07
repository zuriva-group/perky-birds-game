using UnityEngine;
using System.Collections;

public class EnemyBirdScript : MonoBehaviour {

    public class Enemy
    {
        public enum EnemyTypes
        {
            STATIC,
            DESTROYABLE,
            KILLER
        }

        public EnemyTypes type;

    }

    [SerializeField]
    private Enemy.EnemyTypes type;


    [SerializeField]
    [Range(-8.9f,8.9f)]
    private float movementSpeed;

    [SerializeField]
    [Range(0.0f, 11.9f)]
    private float movementRange;

    [SerializeField]
    private bool vertical, horizontal;

    private float deltaValue;

    private bool turnLeft , turnRight;

    private Vector3 startPosition;

    

    // Use this for initialization
    void Start () {
        deltaValue = 0;
        startPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        VerticalMovement();
        HorizontalMovement();

        //VerticalMoveBird();
        //HorizontalMoveBird();

        //Move();
    }

    void Move()
    {
        Vector3 v = startPosition;
        v.y += movementRange * Mathf.Sin(Time.time * movementSpeed);
        transform.position = v;
    }
    

    void VerticalMovement()
    {
        if (vertical)
        {
            Vector3 temp = startPosition;
            temp.y += movementRange * Mathf.Sin(Time.time * movementSpeed);
            transform.position = temp;
        }
    }




    void HorizontalMovement()
    {
        if (horizontal)
        {
            Vector3 temp = startPosition;
            temp.x += movementRange * Mathf.Sin(Time.time * movementSpeed);

            if (Mathf.Sin(Time.time * movementSpeed) >= 0.99f)
            {
                //print("LOOK L");
                Vector3 tempScale = transform.localScale;
                tempScale.x = -(0.22f);
                transform.localScale = tempScale;
            }

            if (Mathf.Sin(Time.time * movementSpeed) <= -0.99f)
            {
                //print("LOOK R");
                Vector3 tempScale = transform.localScale;
                tempScale.x = (0.22f);
                transform.localScale = tempScale;
            }

            transform.position = temp;
        }
    }


    void VerticalMoveBird()
    {
        Vector3 tempPossition = transform.position;
        Vector3 tempScale = transform.localScale;

        if (vertical)
        {
            if (transform.position.x >= startPosition.x + movementRange)
            {
                movementSpeed = -movementSpeed;

                tempScale.x = -(0.22f);

            }
            else if (transform.position.x <= startPosition.x - movementRange)
            {
                movementSpeed = Mathf.Abs(movementSpeed);
                tempScale.x = Mathf.Abs(0.22f);
            }
            tempPossition.x += movementSpeed * Time.deltaTime;

        }
        transform.position = tempPossition;
        transform.localScale = tempScale;

    }

    void HorizontalMoveBird()
    {
        Vector3 tempPossition = transform.position;
        
        if (horizontal)
        {
            if (transform.position.y >= startPosition.y + movementRange)
            {
                movementSpeed = -movementSpeed;
            }
            else if (transform.position.y <= startPosition.y - movementRange)
            {
                movementSpeed = Mathf.Abs(movementSpeed);

            }
            tempPossition.y += movementSpeed * Time.deltaTime;
        }
        transform.position = tempPossition;
    }



    void ChangePossition()
    {
        if (turnLeft)
        {
            print("turn Left");
            Vector3 tempScale = transform.localScale;
            tempScale.x *= (-1);
            transform.localScale = tempScale;
        }

        if (turnRight)
        {
            print("turn Right");
            Vector3 tempScale = transform.localScale;
            Mathf.Abs(tempScale.x *= (-1));
            transform.localScale = tempScale;
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        print("Col");
        if (target.gameObject.tag == "Bird")
        {
            if (type == Enemy.EnemyTypes.KILLER)
            {
                //Destroy(gameObject);
                GameSceneControllerLevel.instance.DieBird(target.gameObject, true);
            }

        }
    }

}
