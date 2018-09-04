using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 작 성 날 : 2018-06-12
/// 작 성 자 : 전민수
/// 작성내용 : 약점을 관리해주는 매니져
/// </summary>

[System.Serializable]
public struct WeakPointData
{
    public Transform parentTransform;
    [HideInInspector]public bool isThisPosWeakPoint;
}

public class WeakPointManager : Singleton<WeakPointManager>
{
    [SerializeField]
    private WeakPoint _weakPoint;
    public WeakPoint GetWeakPoint { get { return _weakPoint; } }

    [SerializeField] private WeakPointData[] _weakPointDatas;

    [SerializeField] private int _extinctParrentCount;
    [SerializeField] private int _destroyParrentCount;

    private int _currentPatternCount;
    public int CurrentPatternCount { set { _currentPatternCount = value; } get { return _currentPatternCount; } }

    private int _currentParnetIndex;

    private void Awake()
    {
        CreateWeakPoint();
    }

    private void Start()
    {
        if (Application.isPlaying)
        {
            EventManager.Instance.IsWeakPointHit = IsHit;
        }
    }

    private void Update()
    {
        if (_weakPoint.CurKeepTime <= 0.0f)
        {
            _weakPoint.CurKeepTime = _weakPoint.KeepTime;
            _weakPoint.IsExtinct = true;
            DestroyWeakPoint();
        }

        if (!_weakPoint.IsHit && !_weakPoint.IsExtinct)
        {
            _weakPoint.CurKeepTime -= Time.deltaTime;
        }
        else if (_weakPoint.IsExtinct)
        {
            if (_currentPatternCount >= _extinctParrentCount)
            {
                _weakPoint.IsExtinct = false;
                _weakPoint.CurKeepTime = _weakPoint.KeepTime;
                CreateWeakPoint();
            }
        }
        else if (_weakPoint.IsHit)
        {

            if (_currentPatternCount >= _destroyParrentCount)
            {
                _weakPoint.IsHit = false;
                _weakPoint.CurKeepTime = _weakPoint.KeepTime;
                CreateWeakPoint();
            }
        }

    }

    private bool IsHit(Collider col ,out float damage)
    {
        foreach (WeakPointData Data in _weakPointDatas)
        {
            if (Data.isThisPosWeakPoint)
            {
                Collider WeakCol = Data.parentTransform.GetComponent<Collider>();
                if (col == WeakCol)
                {
                    if (!_weakPoint.IsHit)
                    {
                        _weakPoint.IsHit = true;
                        damage = _weakPoint.Damage;
                        DestroyWeakPoint();
                        return true;
                    }
                }
            }
        }

        damage = float.NaN;
        return false;
    }

    public void CreateWeakPoint()
    {
        List<int> except = new List<int>();

        _currentPatternCount = 0;

        ParticleObject CreateEffect = _weakPoint.CreateWeakPointEffect;
        ParticleObject DestroyEffect = _weakPoint.DestroyWeakPointEffect;

        for (int i = 0; i < _weakPointDatas.Length; i++)
        {
            if (_weakPointDatas[i].isThisPosWeakPoint)
            {
                ParticleManager.Instance.PoolParticleEffectOff(DestroyEffect);
                except.Add(i);
            }
        }

        HashSet<int> excluding = new HashSet<int>(except);

        var range = Enumerable.Range(0, _weakPointDatas.Length-1).Where(i => !excluding.Contains(i));

        System.Random rand = new System.Random();
        int Index = rand.Next(0, _weakPointDatas.Length-1 - excluding.Count);

        Index = range.ElementAt(Index);


        _weakPointDatas[_currentParnetIndex].isThisPosWeakPoint = false;
        _currentParnetIndex = Index;
        _weakPointDatas[_currentParnetIndex].isThisPosWeakPoint = true;

        Transform parent = _weakPointDatas[_currentParnetIndex].parentTransform;

        _weakPoint.transform.rotation = parent.rotation;
        _weakPoint.transform.parent = parent;
        _weakPoint.transform.localPosition = Vector3.zero;

        ParticleManager.Instance.PoolParticleEffectOn(CreateEffect);

    }

    public void DestroyWeakPoint()
    {
        _weakPoint.DestroyFunc();
    }

}
