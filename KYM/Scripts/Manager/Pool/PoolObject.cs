using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성일 : 2018 - 05 -22
/// 작성자 : 김영민
/// 작업내용 : 풀 오브젝트 제작
/// poolManager에서 가져다 사용할 tag값과 초기값 함수 지정
/// 초기값 함수는 풀링이 필요한 오브젝트들이 이 컴퍼넌트는 불러와 리셋 함수를 적용해준다.
/// </summary>
public class PoolObject : MonoBehaviour
{
    public delegate void ResetFunction();
    public ResetFunction Reset;

    public delegate void InitFunction();
    public InitFunction Init;

    public string pooltag; 
}
