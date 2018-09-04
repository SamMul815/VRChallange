using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : Bullet {

    [SerializeField]
    PoolObject collisionParticle;

    public override void Init()
    {
        moveDir = this.transform.forward;
        prevPosition = this.transform.position;
    }

    protected override void Move()
    {
        this.transform.position += moveDir * Time.fixedUnscaledDeltaTime * moveSpeed;
    }

    protected override void OnCollisionEvent()
    {
        for (int i = 0; i < hitInfo.Length; i++)
        {
            Collider _col = hitInfo[i].collider;
            if (_col.tag == "BulletHoming")
            {
                _col.gameObject.GetComponent<BulletHoming>().GetDamage(Damage);
                Debug.Log("HitIce");
                //break;
            }
            if(_col.tag == "DragonHit")
            {
                EventManager.Instance.DragonHit(_col, damage);
            }
        }
        if(hitInfo.Length > 0)
        EventManager.Instance.EventBulletExplosion(collisionParticle,hitInfo[0].point,hitInfo[0].normal);

        PoolManager.Instance.PushObject(this.gameObject);
    }

    protected override void Reset()
    {
        base.Reset();
        //Debug.Log("BulletBase Reset");
    }

}
