using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성일 : 2018-06-10
/// 작성자 : 전민수
/// 작성내용 : 파티클들을 관리해주는 매니져
/// 
/// 수정일 : 2018-06-11
/// 수정자 : 전민수
/// 수정내용 : 내부적으로 가지고 있는 파티클 및 외부적으로 있는 파티클까지 관리 가능해짐
/// </summary>

public class ParticleManager : Singleton<ParticleManager>
{
    [SerializeField]
    private List<PoolObject> _particlesDatas;

    /// <summary>
    /// 파티클오브젝트들 사용하는 PoolObject의 태그값이 키
    /// </summary>
    private Dictionary<string, PoolObject> _particles = new Dictionary<string, PoolObject>();

    private void Start()
    {
        //파티클들 초기화
        PacticleEffectInit();
    }

    /// <summary>
    /// 파티클이펙트 초기화
    /// </summary> 
    private void PacticleEffectInit()
    {
        foreach (PoolObject obj in _particlesDatas)
        {
            if (obj == null)
            {
                Debug.LogWarning("particles Object Null");
                continue;
            }
            if (!_particles.ContainsKey(obj.pooltag))
                _particles.Add(obj.pooltag, obj);
            else
                _particles[obj.pooltag] = obj;

            PoolManager.Instance.PushObject(obj.gameObject);
        }
    }
    
    /// <summary>
    /// 오브젝트 풀링으로 관리해주는 파티클을 켜주는 함수
    /// </summary>
    /// <param name="ParticleTag">파티클 태그</param>
    public void PoolParticleEffectOn(string ParticleTag)
    {
        PoolObject Particle;
        _particles.TryGetValue(ParticleTag, out Particle);

        if (Particle != null)
        {
            PoolParticleEffectOn(Particle);
            return;
        }
        Debug.LogWarning("Not Found any particles in the object.");
    }

    /// <summary>
    /// 오브젝트 풀링으로 관리해주는 파티클을 켜주는 함수
    /// </summary>
    /// <param name="ParticleTag">파티클 태그</param>
    /// <param name="createPos">생성될 파티클의 부모 트랜스폼</param>
    public void PoolParticleEffectOn(string ParticleTag, Transform parent)
    {
        PoolObject Particle;
        _particles.TryGetValue(ParticleTag, out Particle);

        if (Particle != null)
        {
            Particle.transform.rotation = parent.rotation;
            Particle.transform.parent = parent;
            Particle.transform.localPosition = Vector3.zero;

            PoolParticleEffectOn(Particle);
            return;
        }
        Debug.LogWarning("Not Found any particles in the object.");
    }

    /// <summary>
    /// 오브젝트 풀링으로 관리해주는 파티클을 켜주는 함수
    /// </summary>
    /// <param name="ParticleTag">파티클 태그</param>
    /// <param name="createPos">생성될 파티클 포지션</param>
    /// <param name="createDir">생성될 파티클 방향</param>
    public void PoolParticleEffectOn(string ParticleTag, Vector3 createPos, Vector3 createDir)
    {
        PoolObject Particle;
        _particles.TryGetValue(ParticleTag, out Particle);

        if (Particle != null)
        {
            Quaternion rot = Quaternion.LookRotation(createDir, Vector3.up);
            Particle.transform.position = createPos;
            Particle.transform.rotation = rot;
            PoolParticleEffectOn(Particle);
            return;
        }
        Debug.LogWarning("Not Found any particles in the object.");
    }

    /// <summary>
    /// 오브젝트 풀링으로 관리해주는 파티클을 껴주는 함수
    /// </summary>
    /// <param name="ParticleTag">파티클 태그</param>
    public void PoolParticleEffectOff(string ParticleTag)
    {
        PoolObject Particle;
        _particles.TryGetValue(ParticleTag, out Particle);

        if (Particle != null)
        {
            PoolManager.Instance.PushObject(Particle.gameObject);
            return;
        }
        Debug.LogWarning("Not Found any particles in the object.");

    }

    /// <summary>
    /// 오브젝트 풀링으로 관리해주는 파티클들을 켜주는 함수
    /// </summary>
    /// <param name="obj">파티클 오브젝트</param>
    public void PoolParticleEffectOn(PoolObject obj)
    {
        if (_particles.ContainsKey(obj.pooltag))
        {
            GameObject Particle;
            PoolManager.Instance.PopObject(obj.pooltag, out Particle);

            if (Particle != null)
            {
                Particle.GetComponent<PoolObject>().Init();
                return;
            }
        }
        Debug.LogWarning("Not Found any particles in the object.");
    }

    /// <summary>
    /// 오브젝트 풀링으로 관리해주는 파티클들을 켜주는 함수
    /// </summary>
    /// <param name="obj">파티클 오브젝트</param>
    /// <param name="createPos">생성될 파티클의 부모 트랜스폼</param>
    public void PoolParticleEffectOn(PoolObject obj, Transform parent)
    {
        if (_particles.ContainsKey(obj.pooltag))
        {
            GameObject Particle;
            PoolManager.Instance.PopObject(obj.pooltag, out Particle);

            if (Particle != null)
            {
                Particle.transform.rotation = parent.rotation;
                Particle.transform.parent = parent;
                Particle.transform.localPosition = Vector3.zero;

                Particle.GetComponent<PoolObject>().Init();
                return;
            }
        }
        Debug.LogWarning("Not Found any particles in the object.");
    }

    /// <summary>
    /// 오브젝트 풀링으로 관리해주는 파티클들을 켜주는 함수
    /// </summary>
    /// <param name="obj">파티클 오브젝트</param>
    /// <param name="createPos">생성될 파티클 포지션</param>
    /// <param name="createDir">생성될 파티클 방향</param>
    public void PoolParticleEffectOn(PoolObject obj, Vector3 createPos, Vector3 createDir)
    {

        if (_particles.ContainsKey(obj.pooltag))
        {
            GameObject Particle;
            PoolManager.Instance.PopObject(obj.pooltag, out Particle);

            if (Particle != null)
            {
                Particle.transform.position = createPos;
                Particle.transform.rotation = Quaternion.LookRotation(createDir, Vector3.up);
                Particle.GetComponent<PoolObject>().Init();
                return;
            }
        }
        Debug.LogWarning("Not Found any particles in the object.");
    }

    /// <summary>
    /// 오브젝트 풀링으로 관리해주는 파티클들을 껴주는 함수
    /// </summary>
    /// <param name="obj">파티클 오브젝트</param>
    public void PoolParticleEffectOff(PoolObject obj)
    {
        if (_particles.ContainsKey(obj.pooltag))
        {
            PoolManager.Instance.PushObject(_particles[obj.pooltag].gameObject);
            return;
        }
        Debug.LogWarning("Not Found any particles in the object.");
    }

    /// <summary>
    /// 오브젝트 풀링으로 관리해주는 파티클들을 켜주는 함수
    /// </summary>
    /// <param name="obj">파티클 오브젝트</param>
    public void PoolParticleEffectOn(GameObject obj)
    {
        PoolObject poolObj = obj.GetComponent<PoolObject>();
        
        if (poolObj != null)
        {
            if (_particles.ContainsKey(poolObj.pooltag))
            {
                GameObject Particle;
                PoolManager.Instance.PopObject(poolObj.pooltag, out Particle);

                if (Particle != null)
                {
                    Particle.GetComponent<PoolObject>().Init();
                    return;
                }
            }
        }
        Debug.LogWarning("Not Found any particles in the object.");
    }

    /// <summary>
    /// 오브젝트 풀링으로 관리해주는 파티클들을 켜주는 함수
    /// </summary>
    /// <param name="obj">파티클 오브젝트</param>
    /// <param name="parent">부모 트랜스폼</param>
    public void PoolParticleEffectOn(GameObject obj, Transform parent)
    {
        PoolObject poolObj = obj.GetComponent<PoolObject>();

        if (poolObj != null)
        {
            if (_particles.ContainsKey(poolObj.pooltag))
            {
                GameObject Particle;
                PoolManager.Instance.PopObject(poolObj.pooltag, out Particle);

                if (Particle != null)
                {
                    Particle.transform.rotation = parent.rotation;
                    Particle.transform.parent = parent;
                    Particle.transform.localPosition = Vector3.zero;

                    Particle.GetComponent<PoolObject>().Init();
                    return;
                }
            }
        }
        Debug.LogWarning("Not Found any particles in the object.");
    }

    /// <summary>
    /// 오브젝트 풀링으로 관리해주는 파티클들을 켜주는 함수
    /// </summary>
    /// <param name="obj">파티클 오브젝트</param>
    /// <param name="createPos">생성될 파티클 포지션</param>
    /// <param name="createDir">생성도리 파티클 방향</param>
    public void PoolParticleEffectOn(GameObject obj, Vector3 createPos, Vector3 createDir)
    {
        PoolObject poolObj = obj.GetComponent<PoolObject>();

        if (poolObj != null)
        {
            if (_particles.ContainsKey(poolObj.pooltag))
            {
                GameObject Particle;
                PoolManager.Instance.PopObject(poolObj.pooltag, out Particle);

                if (Particle != null)
                {
                    Particle.transform.position = createPos;
                    Particle.transform.rotation = Quaternion.LookRotation(createDir, Vector3.up);
                    Particle.GetComponent<PoolObject>().Init();
                    return;
                }
            }
        }
        Debug.LogWarning("Not Found any particles in the object.");
    }
    
    /// <summary>
    /// 오브젝트 풀링으로 관리해주는 파티클들을 껴주는 함수
    /// </summary>
    /// <param name="obj">파티클 오브젝트</param>
    public void PoolParticleEffectOff(GameObject obj)
    {
        PoolObject poolObj = obj.GetComponent<PoolObject>();

        if (poolObj != null)
        {
            if (_particles.ContainsKey(poolObj.pooltag))
            {
                PoolManager.Instance.PushObject(obj);
                return;
            }
        }
        Debug.LogWarning("Not Found any particles in the object.");

    }

}
