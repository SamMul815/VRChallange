using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoming : Bullet
{
    enum TargetName
    {
        TargetNull,
        TargetPlayer,
        TargetDragon
    }

    [SerializeField]
    private float homingPower;  //유도력
    [SerializeField]
    private TargetName target;
    [SerializeField]
    private float maxHP;

    private float currentHP;
    //ParticleSystem[] particles;
    List<ParticleSystem> particles = new List<ParticleSystem>();

    private Vector3 targetPosition;   //유도 타겟

    public override void Init()
    {        
        moveDir = this.transform.forward;
        currentHP = maxHP;
        if (particles.Count <= 0)
        {
            ParticleSystem[] p = GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < p.Length; i++)
                particles.Add(p[i]);
        }
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
            particles[i].Play();
        }
    }

    private void Update()
    {
        if (target == TargetName.TargetPlayer)
            targetPosition = UtilityManager.Instance.PlayerPosition();
        else if (target == TargetName.TargetDragon)
            targetPosition = UtilityManager.Instance.DragonPosition();
        else
            targetPosition = Vector3.zero;

        if (currentHP <= 0.0f)
            DestoryObject();
    }

    protected override void Move()
    {
        Vector3 _targetDir = (targetPosition - this.transform.position).normalized;
        moveDir = Vector3.Lerp(moveDir, _targetDir, homingPower * Time.fixedDeltaTime);
        transform.position += moveDir * Time.fixedDeltaTime * moveSpeed;
        transform.rotation = Quaternion.LookRotation(moveDir, Vector3.up);
    }

    protected override void OnCollisionEvent()
    {
        bool _isHit = false;
        for(int i = 0; i<hitInfo.Length; i++)
        {
            Collider _col = hitInfo[i].collider;
            if(_col.CompareTag("BulletHoming"))
                continue;
            if (_col.CompareTag("Boss"))
                continue;
            
            if (_col.CompareTag("PlayerBullet"))
            {
                GetDamage(hitInfo[i].collider.gameObject.GetComponent<BulletBase>().Damage);
                hitInfo[i].collider.gameObject.GetComponent<BulletBase>().DestoryObject();
                continue;
            }

            _isHit = true;
            break;
        }

        if(_isHit)
            PoolManager.Instance.PushObject(this.gameObject);
        //얼음기둥 생성
    }

    protected override void Reset()
    {
        base.Reset(); 
        //Debug.Log("homing Bullet Reset");
    }

    public void GetDamage(float damage) { currentHP -= damage; }


}
