using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRambo : MonoBehaviour {

    public GameObject left;
    public GameObject right;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 dir = left.transform.position - right.transform.position;

        this.transform.forward = dir;
        //this.transform = Quaternion.LookRotation(lookTarget.transform.position, Vector3.up);
	}
}
