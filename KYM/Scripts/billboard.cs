using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard : MonoBehaviour {


    public GameObject eye;
    public Vector3 offset;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        transform.forward = eye.transform.forward;
        //this.transform.rotation = Quaternion.LookRotation(eye.transform.position, Vector3.up);
        //this.transform.Rotate(offset);
        
	}
}
