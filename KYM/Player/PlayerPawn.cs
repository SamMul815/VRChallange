using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class PlayerPawn : MonoBehaviour
{
    //이동
    [SerializeField] private float accelSpeed = 100.0f;
    [SerializeField] private float breakSpeed = 100.0f;
    [SerializeField] private float maxSpeed = 15.0f;

    //대쉬
    [SerializeField] private float flashDistance = 10.0f;
    [SerializeField] private float flashTime = 0.1f;
    [SerializeField] private float flashDelay = 3.0f;

    //점프
    [SerializeField] private float jumpPower = 20.0f;
    [SerializeField] private float jumpDelay = 0.2f;
    [SerializeField] private float airWalkSpeed = 5.0f;
    [SerializeField] private float gravityWeight = 0.0f;

    //회전
    [SerializeField] private float bodyTurnSpeed = 360.0f;
    [SerializeField] private float headTurnSpeed = 180.0f;
    [SerializeField] private Transform headTransform;

    [SerializeField] private Gun gunRight;
    [SerializeField] private Gun gunLeft;

    //딜레이
    private float jumpWaitTime = 0.0f;
    private float flashWaitTime = 0.0f;

    Rigidbody rigid;
    CapsuleCollider col;
    private bool isAir;

    void Start()
    {
        rigid = this.GetComponent<Rigidbody>();
        col = this.GetComponent<CapsuleCollider>();
        isAir = false;
    }

    /// <summary>
    /// 공중에 있는지 확인
    /// </summary>
    void AirCheck()
    {
        Ray _ray = new Ray(transform.position, Vector3.down);
        isAir = !Physics.SphereCast(_ray, col.radius, col.height / 2 + 0.001f);
        Debug.Log(isAir);
    }

    /// <summary>
    /// 쿨타임 확인
    /// </summary>
    void DelayCheck()
    {
        if (jumpWaitTime > 0.0f)
            jumpWaitTime -= Time.deltaTime;

        if (flashWaitTime > 0.0f)
            flashWaitTime -= Time.deltaTime;
    }

    /// <summary>
    /// 플레이어 행동에 관련된 것들 (회전 빼고)
    /// 점프, 움직임 등 
    /// </summary>
    void Move()
    {
        Vector3 _addDir = Vector3.zero;
        Vector3 _velocity = rigid.velocity;
        //이동 구문
        if (PlayerController.MoveLeft())
        {
            _addDir += -transform.right;
        }
        if (PlayerController.MoveRight())
        {
            _addDir += transform.right;
        }
        if (PlayerController.MoveForward())
        {
            _addDir += transform.forward;
        }
        if (PlayerController.MoveBackward())
        {
            _addDir += -transform.forward;
        }
        _addDir = _addDir.normalized * accelSpeed * Time.fixedDeltaTime;


        //저항력
        if (_addDir == Vector3.zero && !isAir && _velocity.magnitude > breakSpeed * Time.fixedDeltaTime)
        {
            _addDir = -_velocity.normalized * breakSpeed * Time.fixedDeltaTime;
        }

        //공중 이동
        if (isAir)
        {
            _addDir = _addDir / accelSpeed * airWalkSpeed;
            _addDir += Physics.gravity * gravityWeight * Time.fixedDeltaTime;
        }

        //가속도 적용  
        _velocity += _addDir;
        if (_velocity.magnitude > maxSpeed && !isAir)
        {
            _velocity = _velocity.normalized * maxSpeed;
        }
        rigid.velocity = _velocity;
    }

    /// <summary>
    /// 몸 회전 카메라 회전 등
    /// </summary>
    void Turn()
    {
      this.transform.Rotate(new Vector3(0.0f, PlayerController.TurnY() * bodyTurnSpeed * Time.deltaTime, 0.0f));
      headTransform.Rotate(new Vector3(PlayerController.TurnX() * headTurnSpeed * Time.deltaTime * -1, 0.0f, 0.0f));
    }

    /// <summary>
    /// 스킬, 총, 공격등 액션적인 요소들
    /// </summary>
    void Action()
    {
        //점프
        if (PlayerController.Jump() && !isAir && jumpWaitTime <= 0.0f)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpWaitTime = jumpDelay;
        }

        if(PlayerController.Attack())
        {
            if(gunRight != null)
                gunRight.Fire();
            if(gunLeft != null)
                gunLeft.Fire();
            
            //gunRight.GetComponent<Gun>().Fire();
            //gunLeft.GetComponent<Gun>().Fire();
        }

        if(PlayerController.Flash())
        {

        }
    }

    void Update()
    {
        DelayCheck();
        Turn();
    }

    private void FixedUpdate()
    {
        AirCheck();
        Move();
    }
}
