using UnityEngine;
using System.Collections;

public class BadgeSizeFixer : MonoBehaviour {

    private Vector3 pipeScale;

	// Use this for initialization
	void Start () {
        pipeScale=GetComponentInParent<PipeScript>().gameObject.transform.localScale;
        //print(pipeScale.x);
        //print(pipeScale.y);
        //transform.localScale = new Vector3(transform.localScale.x, pipeScale.x / pipeScale.y);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
