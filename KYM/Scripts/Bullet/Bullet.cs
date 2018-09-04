using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(PoolObject))]
public class Bullet : MonoBehaviour {

    [SerializeField]
    protected float moveSpeed;             //이동 속도
    [SerializeField]
    protected float damage;                //데미지
    public float Damage
    {
        get { return damage; }
    }

    [SerializeField]
    LayerMask hitLayer;

    protected CapsuleCollider col;         //컬라이더 정보

    protected Vector3 moveDir;             //이동 방향
    protected Vector3 prevPosition;        //이전 위치
    protected RaycastHit[] hitInfo;          //충돌 정보              


    //초기화
    private void Awake()
    {
        prevPosition = this.transform.position;
        col = GetComponent<CapsuleCollider>();
        GetComponent<PoolObject>().Reset = Reset;
    }

    public virtual void Init() { }
    //초기화
    //public  virtual void Init(Vector3 _moveDir){ moveDir = _moveDir; }

    //이동
    protected virtual void Move(){ }

    Vector3 _p1;
    Vector3 _p2;
    //충돌 체크
    protected virtual bool CollisionCheck()
    {
        Vector3 _dir = transform.position - prevPosition;
        Vector3 _colDir;
        //Vector3 _p1;
        //Vector3 _p2;

        if (col.direction == 0) _colDir = transform.right;
        else if (col.direction == 1) _colDir = transform.up;
        else _colDir = transform.forward;

        _p1 = prevPosition + Matrix4x4.Scale(transform.localScale).MultiplyPoint3x4(_colDir * col.height * 0.5f); 
        _p2 = prevPosition - Matrix4x4.Scale(transform.localScale).MultiplyPoint3x4( _colDir * col.height * 0.5f);

        hitInfo = Physics.CapsuleCastAll(_p1, _p2,col.radius * transform.localScale.y, _dir.normalized,_dir.magnitude, hitLayer);
        return hitInfo.Length > 0 ? true : false;
    }

    //충돌시 이벤트
    protected virtual void OnCollisionEvent() {  }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    DestoryObject();
    //}


    protected virtual void Reset(){ }

    //강제 삭제 
    public virtual void DestoryObject()
    {
        PoolManager.Instance.PushObject(this.gameObject);
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        prevPosition = this.transform.position;
        Move();
        bool _isCollision = CollisionCheck();
        if (_isCollision) OnCollisionEvent();

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_p1, 0.2f);
        Gizmos.DrawSphere(_p2, 0.2f);
    }

}
