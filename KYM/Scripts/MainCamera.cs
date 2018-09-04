using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    /// <summary>
    /// 이 스크립트가 있는 카메라가 따라댕기는 놈
    /// </summary>
    [SerializeField] private Transform socket;

    private Vector3 cameraShakeRotation;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.CapsLock))
        {
            StartCoroutine("CorShakeCamera");
        }
	}

    private void FixedUpdate()
    {
        this.transform.position = socket.position;
        this.transform.rotation = socket.rotation * Quaternion.Euler(cameraShakeRotation);
    }

    IEnumerator CorShakeCamera()
    {

        float _shakingPlayTime = 0.2f;
        float _shakingRadius = 3.0f;
        float _shkaingWaitTime = 0.02f;
        float _time = _shakingPlayTime;
        while (_time > 0)
        {
            Vector3 shakingPos = Random.insideUnitSphere * _shakingRadius;
            cameraShakeRotation.x = shakingPos.x * _time / _shakingPlayTime;
            cameraShakeRotation.y = shakingPos.y * _time / _shakingPlayTime;
            _time -= _shkaingWaitTime;
            yield return new WaitForSeconds(_shkaingWaitTime);
        }
        cameraShakeRotation = Vector3.zero;
        yield return null;
    }

}
