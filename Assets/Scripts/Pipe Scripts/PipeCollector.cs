using UnityEngine;
using System.Collections;

public class PipeCollector : MonoBehaviour
{

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
        //print("Coll");
        if (target.gameObject.tag == "Pipe")
        {
            //print("Detected Pipe");
            Destroy(target.gameObject);
        }

    }
    void OnTriggerEnter2D(Collider2D target)
    {
        //print("Trig");
        if (target.gameObject.tag == "Pipe")
        {
            //print("Detected Pipe");
            Destroy(target.gameObject);
        }
    }

}