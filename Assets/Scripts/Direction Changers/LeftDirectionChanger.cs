using UnityEngine;
using System.Collections;

public class LeftDirectionChanger : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Bird")
        {
            float forwardSpeed = target.gameObject.GetComponent<BirdScript>().forwardSpeed;
            forwardSpeed = Mathf.Abs(forwardSpeed);
            target.gameObject.GetComponent<BirdScript>().forwardSpeed = forwardSpeed;
        }
    }
}
