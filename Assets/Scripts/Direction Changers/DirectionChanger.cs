using UnityEngine;
using System.Collections;

public class DirectionChanger : MonoBehaviour {

   
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //void OnTriggerEnter2D(Collider2D target)
    //{
    //    //print("TRIG");
    //    if (target.gameObject.tag == "Bird")
    //    {
    //        //print("Bird");
    //        if (gameObject.name=="Left")
    //        {
    //            if (target.gameObject.GetComponent<BirdScript>().GetDive())
    //            {
    //                //print("DIVINGGGGGGS");
    //                target.gameObject.GetComponent<BirdScript>().forwardSpeed *= 0.55f;
    //            }
    //            //print("Turn Left");
    //            target.gameObject.GetComponent<BirdScript>().TurnLeft();
    //        }
    //        else if (gameObject.name == "Right")
    //        {
    //            if (target.gameObject.GetComponent<BirdScript>().GetDive())
    //            {
    //                //print("DIVINGGGGGGS");
    //                target.gameObject.GetComponent<BirdScript>().forwardSpeed *= 0.55f;
    //            }
    //            //print("turn Right");
    //            target.gameObject.GetComponent<BirdScript>().TurnRight();
    //        }

    //        if (GameSceneController.instance != null)
    //        {
    //            GameSceneController.instance.isBirdTurnedYet = true;
    //        }
    //        else if (GameSceneControllerLevel.instance != null)
    //        {
    //            GameSceneControllerLevel.instance.isBirdTurnedYet = true;
    //        }
    //    }
    //}


    void OnCollisionEnter2D(Collision2D target)
    {
        //print("Col");
        if (target.gameObject.tag == "Bird")
        {
            //print("Bird");
            if (gameObject.name == "Left")
            {
                if (target.gameObject.GetComponent<BirdScript>().GetDive())
                {
                    //print("DIVINGGGGGGS");
                    target.gameObject.GetComponent<BirdScript>().forwardSpeed *= 0.55f;
                }
                //print("Turn Left");
                target.gameObject.GetComponent<BirdScript>().TurnLeft();
            }
            else if (gameObject.name == "Right")
            {
                if (target.gameObject.GetComponent<BirdScript>().GetDive())
                {
                    //print("DIVINGGGGGGS");
                    target.gameObject.GetComponent<BirdScript>().forwardSpeed *= 0.55f;
                }
                //print("turn Right");
                target.gameObject.GetComponent<BirdScript>().TurnRight();
            }

            if (GameSceneController.instance != null)
            {
                GameSceneController.instance.isBirdTurnedYet = true;
            }
            else if (GameSceneControllerLevel.instance != null)
            {
                GameSceneControllerLevel.instance.isBirdTurnedYet = true;
            }
        }
    }
}
