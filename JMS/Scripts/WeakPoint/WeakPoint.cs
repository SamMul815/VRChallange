using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

/// <summary>
/// 작 성 날 : 2018-06-12
/// 작 성 자 : 전민수
/// 작성내용 : 약점 오브젝트
/// </summary>

public class WeakPoint : MonoBehaviour
{
    public delegate void CreateFunction();
    public CreateFunction CreateFunc;

    public delegate void DestroyFunction();
    public DestroyFunction DestroyFunc;

    [SerializeField]
    private ParticleObject _createWeakPointEffect;
    public ParticleObject CreateWeakPointEffect { get { return _createWeakPointEffect; } }

    [SerializeField]
    private ParticleObject _destroyWeakPointEffect;
    public ParticleObject DestroyWeakPointEffect { get { return _destroyWeakPointEffect; } }

    [SerializeField]
    private float _keepTime;
    public float KeepTime { get { return _keepTime; } }

    private float _curKeepTime;
    public float CurKeepTime { set { _curKeepTime = value; } get { return _curKeepTime; } }

    [Range(0.0f, 1.0f)]
    [SerializeField] private float _weakPointDamagePercent;

    private float _damage;
    public float  Damage { get { return _damage; } }

    //소멸
    [HideInInspector] public bool IsExtinct;

    //파괴
    [HideInInspector] public bool IsHit;
    
    private void Awake()
    {
        _damage = DragonManager.Stat.MaxHP * _weakPointDamagePercent;
        _curKeepTime = _keepTime;

        DestroyFunc = WeakPointDestroy;
    }

    private void WeakPointDestroy()
    {
        ParticleManager.Instance.PoolParticleEffectOff(_createWeakPointEffect);
        ParticleManager.Instance.PoolParticleEffectOn(_destroyWeakPointEffect);
    }


}
