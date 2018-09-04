using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;
public class EventManager : Singleton<EventManager>
{
    public delegate bool WeakPointHitFunc(Collider col, out float damage);
    public WeakPointHitFunc IsWeakPointHit;

    public void DragonHit(Collider col, float damage)
    {

            if (IsWeakPointHit != null)
            {
                float WeakPointDamage;
                bool IsHit = IsWeakPointHit(col, out WeakPointDamage);
                if (IsHit)
                {
                    damage = WeakPointDamage;
                    BlackBoard.Instance.IsWeakPointAttack = true;
                }
            }

        DragonManager.Instance.Hit(damage);
    }

    private void Awake()
    {
        //Object[] all =  Resources.LoadAll("Prefabs");
    }

    public void EventBulletExplosion(PoolObject poolObj, Vector3 pos)
    {
        GameObject obj;
        PoolManager.Instance.PopObject(poolObj.pooltag, out obj);
        if(obj != null)
        {
            obj.transform.position = pos;
        }
    }

    public void EventBulletExplosion(PoolObject poolObj, Vector3 pos, Vector3 normal)
    {
        GameObject obj;
        PoolManager.Instance.PopObject(poolObj.pooltag, out obj);
        if (obj != null)
        {
            obj.transform.position = pos;
            obj.transform.rotation = Quaternion.LookRotation(normal, Vector3.up);
        }
    }

    public void EventCartridge(PoolObject poolObj, Vector3 pos, Quaternion rot)
    {
        GameObject obj;
        PoolManager.Instance.PopObject(poolObj.pooltag, out obj);
        if(obj != null)
        {
            obj.transform.position = pos;
            obj.transform.rotation = rot;
        } 
    }
    public void EventCartridge(PoolObject poolObj, Vector3 pos)
    {
        GameObject obj;
        PoolManager.Instance.PopObject(poolObj.pooltag, out obj);
        if (obj != null)
        {
            obj.transform.position = pos;
        }
    }
    //void EventIceBlockMake(Vector3 pos)
    //{

    //}

}
