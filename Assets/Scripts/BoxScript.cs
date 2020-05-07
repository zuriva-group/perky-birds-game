using UnityEngine;
using System.Collections;

public class BoxScript : MonoBehaviour
{
    [System.Serializable]
    public class Box
    {
        public enum BoxTypes
        {
            STATIC,
            DESTROYABLE,
            KILLER
        }

        public BoxTypes type;
       
    }

    [SerializeField]
    private Box.BoxTypes type;


    private AudioSource killerSFX;

    void Awake()
    {
        killerSFX = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Bird")
        {
            target.gameObject.GetComponent<BirdScript>().SetDive(false);

            if (type == Box.BoxTypes.DESTROYABLE)
            {
                Destroy(gameObject);
                GameSceneControllerLevel.instance.DieBird(target.gameObject, true);
            }
            if (type == Box.BoxTypes.KILLER)
            {
                //Destroy(gameObject);
                GameSceneControllerLevel.instance.DieBird(target.gameObject, true);

                if (GameController.instance.isMusicOn)
                {
                    killerSFX.Play();
                }
            }

        }
    }
}
