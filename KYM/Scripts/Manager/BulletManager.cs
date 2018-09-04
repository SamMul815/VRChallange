using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : Singleton<BulletManager>
{
    [SerializeField] private PoolObject bulletBasePlayer;
    [SerializeField] private PoolObject bulletBaseDragon;
    [SerializeField] private PoolObject bulletHomingDragon;

    public void CreateDragonBaseBullet(Vector3 _position, Vector3 _dir)
    {
        Quaternion rot = Quaternion.LookRotation(_dir, Vector3.up);
        GameObject bullet;
        PoolManager.Instance.PopObject(bulletBaseDragon.pooltag, out bullet);
        if(bullet != null)
        {
            bullet.transform.position = _position;
            bullet.transform.rotation = rot;
            bullet.transform.GetComponent<BulletHoming>().Init();
        }

    }

    public void CreateDragonBaseBullet(Transform _firePos)
    {
        GameObject bullet;
        PoolManager.Instance.PopObject(bulletBaseDragon.pooltag, out bullet);
        if (bullet != null)
        {
            bullet.transform.position = _firePos.position;
            bullet.transform.rotation = _firePos.rotation;
            bullet.transform.GetComponent<BulletHoming>().Init();
        }
    }

    public void CreateDragonHomingBullet(Vector3 _position, Vector3 _dir)
    {
        Quaternion rot = Quaternion.LookRotation(_dir, Vector3.up);
        GameObject bullet;
        PoolManager.Instance.PopObject(bulletHomingDragon.pooltag, out bullet);
        if(bullet != null)
        {
            bullet.transform.position = _position;
            bullet.transform.rotation = rot;
            bullet.GetComponent<BulletHoming>().Init();
        }
    }

    public void CreateDragonHomingBullet(Transform _firepos)
    {
        GameObject bullet;
        PoolManager.Instance.PopObject(bulletHomingDragon.pooltag, out bullet);
        if(bullet != null)
        {
            bullet.transform.position = _firepos.position;
            bullet.transform.rotation = _firepos.rotation;
            bullet.GetComponent<BulletHoming>().Init();
        }
    }

    public void CreatePlayerBaseBullet(Vector3 _position, Vector3 _dir)
    {
        Quaternion rot = Quaternion.LookRotation(_dir, Vector3.up);
        GameObject bullet;
        PoolManager.Instance.PopObject(bulletBasePlayer.pooltag, out bullet);
        if (bullet != null)
        {
            bullet.transform.position = _position;
            bullet.transform.rotation = rot;
            bullet.GetComponent<BulletBase>().Init();
        }
    }

    public void CreatePlayerBaseBullet(Transform _firepos)
    {
        GameObject bullet;
        PoolManager.Instance.PopObject(bulletBasePlayer.pooltag, out bullet);
        if (bullet != null)
        {
            bullet.transform.position = _firepos.position;
            bullet.transform.rotation = _firepos.rotation;
            bullet.GetComponent<BulletBase>().Init();
        }
    }

}
