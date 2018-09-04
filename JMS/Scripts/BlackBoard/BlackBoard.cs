using DragonController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    수 정 날 : 2018 - 05 - 05
    작 성 자 : 전민수
    수정내역 : DragonManager를 Singleton화로 인한 소스코드 정리
*/

public class BlackBoard : Singleton<BlackBoard>
{
    [Space]
    [Header("Boss Body Transform")]

    [SerializeField]
    private Transform _dragonMouth;
    public Transform DragonMouth { get { return _dragonMouth; } }

    [SerializeField]
    private Transform _dragonPowAttacksPivot;
    public Transform DragonPowAttacksPivot { get { return _dragonPowAttacksPivot; } }

    [SerializeField]
    private Transform _dragonTailAttacksPivot;
    public Transform DragonTailAttacksPivot { get { return _dragonTailAttacksPivot; } }

    private Clock _clocks;

    [Space]
    [Header("Boss Action Check Distance")]

    [SerializeField]
    private float _rushDistance;
    public float RushDistance { get { return _rushDistance; } }

    [SerializeField]
    private float _fanShapeBreathDistance;
    public float FanShapeBreathDistance { get { return _fanShapeBreathDistance; } }

    [SerializeField]
    private float _nearBreathDistance;
    public float NearBreathDistance { get { return _nearBreathDistance; } }

    //[SerializeField]
    //private float _mortarDistance;
    //public float MortarDistance { get { return _mortarDistance; } }

    /* 보스몹 덮치기 */
    [Space]
    [Header("Boss OverLapAttack Check")]

    [SerializeField]
    private bool _isOverLapAttack;
    public bool IsOverLapAttack { set { _isOverLapAttack = value; } get { return _isOverLapAttack; } }

    [SerializeField]
    private float _secondAttackDistance;
    public float SecondAttackDistance { get { return _secondAttackDistance; } }


    /* 보스몹 이동 거리*/
    [Space]
    [Header("Boss Pattern Move Distance ")]

    [SerializeField]
    private float _overLapMoveDistance;
    public float OverLapMoveDistance { get { return _overLapMoveDistance; } }

    [SerializeField]
    private float _rushMoveDistance;
    public float RushMoveDistance { get { return _rushMoveDistance; } }


    [Space]
    [Header("Boss State")]
    /* 보스몹 해야되는 행동 관련 변수 */
    [SerializeField]
    private bool _isGround;      //땅 상태
    public bool IsGround { set { _isGround = value; } get { return _isGround; } }

    private bool _isLanding;    //착륙 상태
    public bool IsLanding { set { _isLanding = value; } get { return _isLanding; } }

    private bool _isIdle;       //아이들 상태
    public bool IsIdle { set { _isIdle = value; } get { return _isIdle; } }
    
    [SerializeField]
    private bool _isWalk;       //걷기 상태
    public bool IsWalk { set { _isWalk = value; }  get { return _isWalk; } }

    private bool _isTakeOffReady;//이륙 준비 상태
    public bool IsTakeOffReady { set { _isTakeOffReady = value; } get { return _isTakeOffReady; } }

    private bool _isTakeOff;    //이륙 상태
    public bool IsTakeOff { set { _isTakeOff = value; } get { return _isTakeOff; } }

    [SerializeField]
    private bool _isHovering;   //호버링 상태
    public bool IsHovering { set { _isHovering = value; } get { return _isHovering; } }

    private bool _isFlying;     //플라잉 상태
    public bool IsFlying { set { _isFlying = value; } get { return _isFlying; } }

    //private bool _isAttack;     //공격 상태
    //public bool IsAttack { set { _isAttack = value; } get { return _isAttack; } }


    //[Space]
    //[Header("Boss Pattern On")]
    ///* 보스몹 행동 중 관련 변수 */
    //[SerializeField]
    private bool _isGroundAttacking;   //땅에서 패턴을 사용하고 있는지
    public bool IsGroundAttacking { set { _isGroundAttacking = value; } get { return _isGroundAttacking; } }

    //[SerializeField]
    //private bool _isLandingAct;         //착륙 액션을 하고 있는지
    //public bool IsLandingAct { set { _isLandingAct = value; } get { return _isLandingAct; } }

    //[SerializeField]
    //private bool _isTakeOffAct;         //이륙 액션을 하고 있는지
    //public bool IsTakeOffAct { set { _isTakeOffAct = value; } get { return _isTakeOffAct; } }

    //[SerializeField]
    //private bool _isHoveringPatternAct;  //호버링 패턴을 사용하고 있는지
    //public bool IsHoveringPatternAct { set { _isHoveringPatternAct = value; } get { return _isHoveringPatternAct; } }

    //[SerializeField]
    //private bool _isFlyingPatternAct;    //플라잉 패턴을 하고 있는지
    //public bool IsFlyingPatternAct { set { _isFlyingPatternAct = value; } get { return _isFlyingPatternAct; } }

    //private bool _isTakeOffEnd;         //이륙 액션을 하고 있는지
    //public bool IsTakeOffEnd { set { _isTakeOffEnd = value; } get { return _isTakeOffEnd; } }

    //[Space]
    //[Header("Boss Pattern Attack On")]

    /* Boss Ground Pattern Attack */
    private bool _isRushAttacking;
    public bool IsRushAttacking { set { _isRushAttacking = value; } get { return _isRushAttacking; } }

    private bool _isNearHowlingAttacking;
    public bool IsNearHowlingAttacking { set { _isNearHowlingAttacking = value; } get { return _isNearHowlingAttacking; } }

    private bool _isOverLapAttacking;
    public bool IsOverLapAttacking { set { _isOverLapAttacking = value; } get { return _isOverLapAttacking; } }

    private bool _isFanShapeBreathAttacking;
    public bool IsFanShapeBreathAttacking { set { _isFanShapeBreathAttacking = value; } get { return _isFanShapeBreathAttacking; } }

    private bool _isNearBreathAttacking;
    public bool IsNearBreathAttacking { set { _isNearBreathAttacking = value; } get { return _isNearBreathAttacking; } }

    private bool _isHowlingAttacking;
    public bool IsHowlingAttacking { set { _isHowlingAttacking = value; } get { return _isHowlingAttacking; } }

    private bool _isMortarAttacking;
    public bool IsMortarAttacking { set { _isMortarAttacking = value; } get { return _isMortarAttacking; } }

    [SerializeField]
    private bool _isNearHowling;
    public bool IsNearHowling { set { _isNearHowling = value; } get { return _isNearHowling; } }


    [Space]
    [Header("Boss Second Attack On")]

    /* 보스 2차 공격 패턴 */
    [SerializeField]
    private bool _isSecondAttack;   //2차 공격을 해야 되는지
    public bool IsSecondAttack { set { _isSecondAttack = value; } get { return _isSecondAttack; } }

    private bool _isSecondAttacking;    //2차 공격 중인지
    public bool IsSecondAttacking { set { _isSecondAttacking = value; } get { return _isSecondAttacking; } }

    private bool _isLeftPowAttacking;  //2차 공격 중 왼발 공격을 하고 있는지
    public bool IsLeftPowAttacking { set { _isLeftPowAttacking = value; } get { return _isLeftPowAttacking; } }

    private bool _isRightPowAttacking; //2차 공격 중 오른발 공격을 하고 있는지
    public bool IsRightPowAttacking { set { _isRightPowAttacking = value; } get { return _isRightPowAttacking; } }

    private bool _isTailAttacking;     //2차 공격 중 꼬리 공격을 하고 있는지
    public bool IsTailAttacking { set { _isTailAttacking = value; } get { return _isTailAttacking; } }

    [Space]
    [Header("IceCrystal")]
    /* 현재 얼음결정 개수 */
    [SerializeField]
    private int _curIceCrystalNum;
    public int CurIceCrystalNum { set { _curIceCrystalNum = value; } get { return _curIceCrystalNum; } }

    /* IceBullet(얼음탄환) 얼음 결정 개수 */
    [SerializeField]
    private int _maxIceBulletCrystalNum;
    public int MaxIceBulletCrystalNum { set { _maxIceBulletCrystalNum = value; } get { return _maxIceBulletCrystalNum; } }

    [SerializeField]
    private int _minIceBulletCrystalNum;
    public int MinIceBulletCrtystalNum { set { _minIceBulletCrystalNum = value; } get { return _minIceBulletCrystalNum; } }

    /* Missile(유도탄) 얼음 결정 개수 */
    [SerializeField]
    private int _maxHommingBulletCrystalNum;
    public int MaxHommingBulletCrystalNum { set { _maxHommingBulletCrystalNum = value; } get{ return _maxHommingBulletCrystalNum; } }

    [SerializeField]
    private int _maxBreathCrystalNum;
    public int MaxBreathCrystalNum { set { _maxBreathCrystalNum = value; }  get { return _maxBreathCrystalNum; } }


    [Header("WeakPoint")]
    //보스몹 약점 공략
    [SerializeField]
    private int _curWeakPointCount;
    public int CurWeakPointCount { set { _curWeakPointCount = value; } get { return _curWeakPointCount; } }

    [SerializeField]
    private int _maxWeakPointCount;
    public int MaxWeakPointCount { set { _maxWeakPointCount = value; } get { return _maxWeakPointCount; } }

    [SerializeField]
    private bool _isWeakPointAttack;
    public bool IsWeakPointAttack { set { _isWeakPointAttack = value; } get { return _isWeakPointAttack; } }
    

    [Header("PlayerHP Dummy")]
    /* 나중에 지워야 됨!!! */
    public float PlayerMaxHP;
    public float CurPlayerHP;

    public void Awake()
    {
        _isGround = true;
        _clocks = GetComponentInChildren<Clock>();
    }

    public Clock.GroundTimes GetGroundTime()
    {
        return _clocks.GroundTime;
    }

    public Clock.FlyingTimes GetFlyingTime()
    {
        return _clocks.Flyingtime;
    }

    public void Move(Transform Target, float MoveSpeed, float TurnSpeed)
    {
        Transform Dragon = DragonManager.Instance.transform;

        if (Dragon.position != Target.position)
        {
            Vector3 forward = Target.position - Dragon.position;

            if (forward != Vector3.zero)
            {
                Dragon.rotation =
                    Quaternion.RotateTowards(
                        Dragon.rotation,
                        Quaternion.LookRotation(forward),
                        TurnSpeed * Time.deltaTime);

                Vector3 nextPos =
                    Vector3.MoveTowards(
                        Dragon.position,
                        Target.position,
                        MoveSpeed * Time.deltaTime);

                Dragon.position = nextPos;

            }
        }


    }

    /*
    public void CircleMove(Vector3 Target, float Radian)
    {
        Transform dragon = DragonManager.Instance.transform;

        Vector3 circlePos = GetCirclePos(Target, Radian);
        Vector3 forward = (Target - dragon.position).normalized;

        dragon.position = circlePos;
        dragon.rotation = Quaternion.LookRotation(forward);

    }
    */

    public float Acceleration(float fCurSpeed, float fMaxSpeed, float fAccSpeed)
    {
        if (fCurSpeed >= fMaxSpeed)
            return fMaxSpeed;
        else
        {
            float dir = Mathf.Sign(fMaxSpeed - fCurSpeed);
            fCurSpeed += fAccSpeed * dir * Time.fixedDeltaTime;
            return (dir == Mathf.Sign(fMaxSpeed - fCurSpeed)) ? fCurSpeed : fMaxSpeed;
        }

    }

    /*
    public Vector3 GetCirclePos(Vector3 Target, float Radian)
    {
        Vector3 retPos;

        Transform dragon = DragonManager.Instance.transform;

        float x = Mathf.Cos(Radian) * Radius + Target.x;
        float z = Mathf.Sin(Radian) * Radius + Target.z;

        retPos = new Vector3(x, dragon.position.y, z);
        return retPos;

    }
    */

    public NodeManager GetNodeManager(MOVEMENTTYPE Type)
    {
        return DragonManager.DragonMovement.GetNodeManager(Type);
    }

    public bool IsMoveReady(MOVEMENTTYPE Type)
    {
        return GetNodeManager(Type).IsMoveReady;
    }

    public void MovementReady(MOVEMENTTYPE Type)
    {
        DragonManager.DragonMovement.MovementReady(Type);
    }

    public void FlyingMovement(MOVEMENTTYPE Type)
    {
        DragonManager.DragonMovement.Movement(Type);
    }
}
