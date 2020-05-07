using UnityEngine;
using System.Collections;

public class DeadZone : MonoBehaviour
{

    [SerializeField]
    private GameObject explosion;

    private int counter;

    private AudioSource GroundSFX;

    /*
    [SerializeField]
    private GameObject feather;

    private float minX = -10.0f;
    private float maxX = +10.0f;
    private float minY = 2.0f;
    private float maxY = 4.0f;
    */

    // Use this for initialization

    void Awake()
    {
        if (GetComponent<AudioSource>() != null)
        {
            GroundSFX = GetComponent<AudioSource>();
        }
    }

    void Start()
    {
        counter = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnCollisionEnter2D(Collision2D target)
    {
        //print(Time.time);
        if (target.gameObject.tag == "Bird")
        {
            //print("PRINT: "+target.gameObject.GetComponent<Rigidbody2D>().velocity);
            //target.gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;

            target.gameObject.GetComponent<BirdScript>().SetDive(false);
            
            StartCoroutine(MakeExplosion(target.gameObject));


            --counter;
            if (counter < 1)
            {
                //print("1");
                // if (target.gameObject.GetComponent<BirdScript>().alive)
                //  {
                //print("T--->" + counter);

                if (GameSceneController.instance != null)
                {
                    GameSceneController.instance.DieBird(target.gameObject, true , false);
                }
                else if (GameSceneControllerLevel.instance != null)
                {
                    GameSceneControllerLevel.instance.DieBird(target.gameObject, true);

                }
                // }
            }
            if (GameController.instance.isMusicOn)
            {
                if (GroundSFX != null)
                {
                    GroundSFX.Play();
                }
            }

            if (target.gameObject.GetComponent<BirdScript>().alive)
            {
                //print("2");
                //target.gameObject.GetComponent<BirdScript>().alive = false;
                if (GameSceneController.instance != null)
                {
                    GameSceneController.instance.DieBird(target.gameObject, false , false);
                }
                else if (GameSceneControllerLevel.instance != null)
                {
                    GameSceneControllerLevel.instance.DieBird(target.gameObject, true);

                }
            }
        }
    }


    IEnumerator MakeExplosion(GameObject targetObj)
    {
        //print(1);
        GameObject obj =  Instantiate(explosion, targetObj.gameObject.transform.position, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(0.5f);
        Destroy(obj);
        //print(2);
    }
}
