﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DragonController {

    public class DragonStat : MonoBehaviour {

        [Header("Boss Move Speed")]

        [SerializeField]
        private float _walkSpeed = 10.0f;
        public float WalkSpeed { set { _walkSpeed = value; } get { return _walkSpeed; } }

        [SerializeField]
        private float _turnSpeed = 360.0f;
        public float TurnSpeed { set { _turnSpeed = value; } get { return _turnSpeed; } }

        [Space]
        [Header("Boss HP")]

        [SerializeField]
        private float _maxHP;
        public float MaxHP { set { _maxHP = value; } get { return _maxHP; } }

        [SerializeField]
        private float _hp;
        public float HP { set { _hp = value; } get { return _hp; } }


        [Space]
        [Header("Boss TakeDamage")]

        [SerializeField]
        private float _saveTakeDamage;
        public float SaveTakeDamage { set { _saveTakeDamage = value; } get { return _saveTakeDamage; } }

        [Space]
        [Header("Boss Pattern Damage Stats")]
        [SerializeField]
        private float _rushDamage;
        public float RushDamage { set { _rushDamage = value; } get { return _rushDamage; } }

        [SerializeField]
        private float _overLapDamage;
        public float OverLapDamage{ set { _overLapDamage = value; } get { return _overLapDamage; } }

        [SerializeField]
        private float _leftPowDamage;
        public float LeftPowDamage { set { _leftPowDamage = value; } get { return _leftPowDamage; } }

        [SerializeField] 
        private float _rightPowDamage;
        public float RightPowDamage { set { _rightPowDamage = value; } get { return _rightPowDamage; } }

        [SerializeField]
        private float _nearHowlingDamage;
        public float NearHowlingDamage { set { _nearHowlingDamage = value; } get { return _nearHowlingDamage; } }

        [SerializeField]
        private float _nearBreathDamage;
        public float NearBreathDamage { set { _nearHowlingDamage = value; } get { return _nearBreathDamage; } }

        [Space]
        [Header("Boss Phase HPBar Precent")]

        [Range(0.0f, 1.0f)]
        [SerializeField] private float _firstPhaseHpPercent;
        public float FirstPhaseHpPercent { set { _firstPhaseHpPercent = value; } get { return _firstPhaseHpPercent; } }

        [Range(0.0f, 1.0f)]
        [SerializeField] private float _secondPhaseHpPercent;
        public float SecondPhaseHpPercent { set { _secondPhaseHpPercent = value; } get { return _secondPhaseHpPercent; } }

        [Range(0.0f, 1.0f)]
        [SerializeField] private float _thirdPhaseHpPercent;
        public float ThirdPhaseHpPercent { set { _thirdPhaseHpPercent = value; } get { return _thirdPhaseHpPercent; } }

        [Space]
        [Header("Boss State HPBar Precent")]

        [Range(0.0f, 1.0f)]
        [SerializeField] private float _groggyHpPercent;
        public float GroggyHpPercent { set { _groggyHpPercent = value; } get { return _groggyHpPercent; } }

        [Range(0.0f, 1.0f)]
        [SerializeField] private float _damageReceiveHpPercent;
        public float DamageReceiveHpPercent { get { return _damageReceiveHpPercent; } }


        public void Awake()
        {
        }

    }
}
