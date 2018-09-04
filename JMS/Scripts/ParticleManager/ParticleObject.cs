using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleObject : PoolObject
{
    [SerializeField]
    private float _endTime;
    public float EndTime { get { return _endTime; } }

    private float _currentTime;
    public float CurrentTime { get { return _currentTime; } }

    private void Awake()
    {
        this.Init = Initialization;
    }

    private void Update()
    {
        if (_currentTime >= EndTime)
            DestoryObject();

        _currentTime += Time.deltaTime;
    }

    public void Initialization()
    {
        _currentTime = 0.0f;
    }

    public void DestoryObject()
    {
        Initialization();
        PoolManager.Instance.PushObject(this.gameObject);
    }
	
}
