using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShakePlayer : MonoBehaviour {

    [SerializeField]
    private Transform VRCamera;
    private Vector3 cameraShakePos;
    
	// Use this for initialization
	void Start ()
    {
        cameraShakePos = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(CorShakeCamera(2.5f, 0.75f, 0.02f));
        }
        this.transform.localPosition = cameraShakePos;
	}


    public void PlayerShake(float _playTime = 0.3f, float _radius = 0.5f, float _waitTime= 0.02f)
    {
        StartCoroutine(CorShakeCamera(_playTime, _radius, _waitTime));
    }

    IEnumerator CorShakeCamera(float _playTime, float _radius, float _waitTime)
    {

        yield return new WaitForSeconds(1.5f);

        float _shakingPlayTime = _playTime;
        float _shakingRadius = _radius;
        float _shkaingWaitTime = _waitTime;
        float _time = _shakingPlayTime;
        while (_time > 0)
        {
            Vector3 shakingPos = Random.insideUnitSphere * _shakingRadius;
            cameraShakePos = (VRCamera.up * shakingPos.y + VRCamera.right * shakingPos.x) * _time / _shakingPlayTime;
            //cameraShakePos.x = shakingPos.x * _time / _shakingPlayTime;
            //cameraShakePos.y = shakingPos.y * _time / _shakingPlayTime;
            _time -= _shkaingWaitTime;
            yield return new WaitForSeconds(_shkaingWaitTime);
        }
        cameraShakePos = Vector3.zero;
        yield return null;
    }
}
