using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTimer : MonoBehaviour {

    public bool isUnscale = true;
    public float time;
    private float currentTime;

	// Use this for initialization
	void Awake ()
    {
        currentTime = 0.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(isUnscale)
        {
            currentTime += Time.unscaledDeltaTime;
        }
        else
        {
            currentTime += Time.deltaTime;
        }

        if(currentTime >= time)
        {
            currentTime = 0.0f;
            this.gameObject.SetActive(false);
        }

	}
}
