using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [System.Serializable]
    public class GroundTimes
    {

        [Space]
        [Header("Idle Time")]

        [SerializeField]
        private float _maxIdleTime;
        public float MaxIdleTime { get { return _maxIdleTime; } }

        private float _curIdleTime;
        public float CurIdleTime { set { _curIdleTime = value; } get { return _curIdleTime; } }

        [Space]
        [Header("Walk Time")]

        [SerializeField]
        private float _maxWalkTime;
        public float MaxWalkTime { get { return _maxWalkTime; } }

        private float _curWalkTime;
        public float CurWalkTime { set { _curWalkTime = value; } get { return _curWalkTime; } }

        [Space]
        [Header("DamageAmount Time")]

        [SerializeField]
        private float _maxDamageAmountTime;
        public float MaxDamageAmountTime { get { return _maxDamageAmountTime; } }

        private float _curDamageAmountTime;
        public float CurDamageAmountTime { set { _curDamageAmountTime = value; } get { return _curDamageAmountTime; } }

        [Space]
        [Header("Rush Attack Time")]

        [SerializeField]
        private float _preRushTime;
        public float PreRushTime { get { return _preRushTime; } }

        [SerializeField]
        private float _runRushTime;
        public float RunRushTime { set { _runRushTime = value; } get { return _runRushTime; } }

        [SerializeField]
        private float _afterRushTime;
        public float AfterRushTime { get { return _afterRushTime; } }

        [Space]
        [Header("NearHowling Attack Time")]
        [SerializeField]
        private float _preNearHowlingTime;
        public float PreNearHowlingTime { get { return _preNearHowlingTime; } }

        [SerializeField]
        private float _runNearHowlingTime;
        public float RunNearHowlingTime { get { return _runNearHowlingTime; } }

        [SerializeField]
        private float _afterNearHowlingTime;
        public float AfterNearHowlingTime { get { return _afterNearHowlingTime; } }

        [Space]
        [Header("Howling Attack Time")]

        [SerializeField]
        private float _preHowlingTime;
        public float PreHowlingTime { get { return _preHowlingTime; } }

        [SerializeField]
        private float _runHowlingTime;
        public float RunHowlingTime { get { return _runHowlingTime; } }

        [SerializeField]
        private float _afterHowlingTime;
        public float AfterHowlingTime { get { return _afterHowlingTime; } }

        [Space]
        [Header("OverLap Attack Time")]

        [SerializeField]
        private float _preOverLapTime;
        public float PreOverLapTime { get { return _preOverLapTime; } }

        [SerializeField]
        private float _runOverLapTime;
        public float RunOverLapTime { set { _runOverLapTime = value; } get { return _runOverLapTime; } }

        [SerializeField]
        private float _afterOverLapTime;
        public float AfterOverLapTime { get { return _afterOverLapTime; } }

        [SerializeField]
        private float _curOverLapCheckTime;
        public float CurOverLapCheckTime { set { _curOverLapCheckTime = value; } get { return _curOverLapCheckTime; } }

        [SerializeField]
        private float _maxOverLapCheckTime;
        public float MaxOverLapCheckTime { get { return _maxOverLapCheckTime; } }

        [Space]
        [Header("FanShapeBreath Attack Time")]

        [SerializeField]
        private float _preFanShapeBreathTime;
        public float PreFanShapeBreathTime { get { return _preFanShapeBreathTime; } }

        [SerializeField]
        private float _runFanShapeBreathTime;
        public float RunFanShapeBreathTime { get { return _runFanShapeBreathTime; } }

        [SerializeField]
        private float _afterFanShapeBreathTime;
        public float AfterFanShapeBreathTime { get { return _afterFanShapeBreathTime; } }

        [Space]
        [Header("BulletBreath Attack Time")]

        [SerializeField]
        private float _preNearBreathTime;
        public float PreNearBreathTime { get { return _preNearBreathTime; } }

        [SerializeField]
        private float _runNearBreathTime;
        public float RunNearBreathTime { get { return _runNearBreathTime; }  }

        [SerializeField]
        private float _afterNearBreathTime;
        public float AfterNearBreathTime { get { return _afterNearBreathTime; } }

        [Space]
        [Header("Mortar Attack Time")]

        [SerializeField]
        private float _preMortarTime;
        public float PreMortarTime { get { return _preMortarTime; } }

        [SerializeField]
        private float _runMortarTime;
        public float RunMortarTime { get { return _runMortarTime; } }

        [SerializeField]
        private float _afterMortarTime;
        public float AfterMortarTime { get { return _afterMortarTime; } }

        [Space]
        [Header("Second Attack Time")]

        [SerializeField]
        private float _secondAttackPreTime;
        public float SecondAttackPreTime { get { return _secondAttackPreTime; } }

        private float _secondAttackCurTime;
        public float SecondAttackCurTime { set { _secondAttackCurTime = value; } get { return _secondAttackCurTime; } }

        [SerializeField]
        private float _secondAttackRunTime;
        public float SecondAttackRunTime { get { return _secondAttackRunTime; } }

        [SerializeField]
        private float _secondAttackAfterTime;
        public float SecondAttackAfterTime { get { return _secondAttackAfterTime; } }

        public void InitTime()
        {
            _curWalkTime = 0.0f;
        }
    }

    [System.Serializable]
    public class FlyingTimes
    {

        /* 호버링 시간 */
        [SerializeField]
        private float _hoveringTime;
        public float HoveringTime { set { _hoveringTime = value; } get { return _hoveringTime; } }

        private float _curHoveringTime;
        public float CurHoveringTime { set { _curHoveringTime = value; } get { return _curHoveringTime; } }

        /* 나는 시간 */
        [SerializeField]
        private float _flyTime;
        public float FlyTime { get { return _flyTime; } }

        private float _curFlyTime;
        public float CurFlyTime { set { _curFlyTime = value; } get { return _curFlyTime; } }

        /* 유도탄 */
        [SerializeField]
        private float _preHommingBulletTime;
        public float PreHommingBulletTime { get { return _preHommingBulletTime; } }

        [SerializeField]
        private float _afterHommingBulletTime;
        public float AfterHommingBulletTime { get { return _afterHommingBulletTime; } }

        /* 브레스 */
        [SerializeField]
        private float _preBreathTime;
        public float PreBreathTime { get { return _preBreathTime; } }

        [SerializeField]
        private float _runBreathTime;
        public float RunBreathTime { set { _runBreathTime = value; } get { return _runBreathTime; } }

        [SerializeField]
        private float _afterBreathTime;
        public float AfterBreathTime { get { return _afterBreathTime; } }


        /* 얼음탄환  */
        [SerializeField]
        private float _preIceBulletTime;
        public float PreIceBulletTime { get { return _preIceBulletTime; } }

        [SerializeField]
        private float _runIceBulletTime;
        public float RunIceBulletTime { set { _runIceBulletTime = value; } get { return _runIceBulletTime; } }

        [SerializeField]
        private float _afterIceBulletTime;
        public float AfterIceBulletIime { get { return _afterIceBulletTime; } }



        public void InitTime()
        {
            _curHoveringTime = 0.0f;
            _curFlyTime = 0.0f;
        }

    }

    [SerializeField]
    private GroundTimes _groundTime;
    public GroundTimes GroundTime { get { return _groundTime; } }

    [SerializeField]
    private FlyingTimes _flyingTime;
    public FlyingTimes Flyingtime { get { return _flyingTime; } }

    [SerializeField]
    private float _fallingTime;
    public float FallingTime { set { _fallingTime = value; } get { return _fallingTime; } }

    public void InitLandTimes()
    {
        _groundTime.InitTime();
    }

    public void InitFlyingTime()
    {
        Flyingtime.InitTime();
    }

}
