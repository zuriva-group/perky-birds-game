using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {

    [SerializeField]
    private bool isRight;


    [SerializeField]
    private GameObject right, left;


    // Use this for initialization
    void Start () {

        right.SetActive(!isRight);
        left.SetActive(isRight);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
