using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    수 정 날 : 2018 - 05 - 05
    작 성 자 : 전민수
    수정내역 : DragonManager를 Singleton화로 인한 소스코드 정리
*/

namespace DragonController { 

    public class DragonAniManager : Singleton<DragonAniManager>
    {
        
        private DragonManager _manager;
        public DragonManager Manager { get { return _manager; } }

        private static Animator _ani;
        public static Animator Ani { get { return _ani; } }
        
        private static string _aniParamName;
        public static string AniParamName { set { _aniParamName = value; } get { return _aniParamName; } }

        static bool _isInit;

        private void Awake()
        {
            _ani = GetComponent<Animator>();
            _manager = DragonManager.Instance;
        }
        void Start()
        {
            if (Application.isPlaying)
            {
                SwicthAnimation("Idle");
                _isInit = true;
            }

        }

        public static void SwicthAnimation(string _newAniName)
        {
            if (_isInit)
                Ani.ResetTrigger(_aniParamName);

            _aniParamName = _newAniName;
            Ani.SetTrigger(_aniParamName);
        }

        public void TakeOffReadyOn()
        {
            Debug.Log("TakeOffReady");
            BlackBoard.Instance.IsTakeOffReady = true;
        }

    }
}
