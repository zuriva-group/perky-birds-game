using UnityEngine;
using System.Collections;

public class HiddenGroundScript : MonoBehaviour
{
    private Vector3 startPos;
    private bool up, down;


    // Use this for initialization
    void Start()
    {
        //StartVerticalMove();
    }

    // Update is called once per frame
    void Update()
    {
        if (up)
        {
            Up();
        }
        if (down)
        {
            Down();
        }

        //Move();
    }


    void Move()
    {
        if (transform.position.y < (startPos.y + 0.2f))
        {
            print("UP");
            gameObject.transform.position += new Vector3(0f, 0.02f, 0f);
        }
        else if (transform.position.y > (startPos.y - 0.2f))
        {
            print("DOWN");
            gameObject.transform.position -= new Vector3(0f, 0.02f, 0f);
        }
    }

    void Up()
    {
        if(gameObject.transform.position.y >= (startPos.y + 0.1))
        {
            up = false;
            down = true;
        }
        gameObject.transform.position += new Vector3(0f, 0.025f, 0f);
        //print("Up");
    }

    void Down()
    {
        if (gameObject.transform.position.y <= (startPos.y - 0.1))
        {
            up = true;
            down = false;
        }
        gameObject.transform.position -= new Vector3(0f, 0.025f, 0f);
        //print("DOWN");
    }


    public void StartVerticalMove(Vector3 pos)
    {
        startPos = pos;
        down = true;
        up = false;
    }
}
