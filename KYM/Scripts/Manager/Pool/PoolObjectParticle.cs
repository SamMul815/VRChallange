using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjectParticle : PoolObject
{
    [SerializeField] private float waitTime = 5.0f;
    float currentTime;
	// Use this for initialization
	void Awake ()
    {
        Reset = PoolParticleReset;
        PoolParticleReset();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(currentTime <= 0.0f)
        {
            PoolManager.Instance.PushObject(this.gameObject);
        }
        else
        {
            currentTime -= Time.deltaTime;
        }

	}
    void PoolParticleReset()
    {
        currentTime = waitTime;
    }


}
