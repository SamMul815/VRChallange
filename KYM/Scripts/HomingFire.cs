using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingFire : MonoBehaviour {

    public float time;
    public float fireTime;
    // Use this for initialization
	void Start ()
    {
        fireTime = time;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(fireTime <= 0.0f)
        {
            BulletManager.Instance.CreateDragonHomingBullet(transform);
            fireTime = time;
        }
        fireTime -= Time.deltaTime;

	}
}
