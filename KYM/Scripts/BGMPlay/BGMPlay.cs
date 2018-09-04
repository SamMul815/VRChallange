using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlay : MonoBehaviour {

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.Instance.PlayAudio("MainBGM", this.transform.position);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            SoundManager.Instance.PlayAudio("Fire", this.transform.position);
        }
    }
}
