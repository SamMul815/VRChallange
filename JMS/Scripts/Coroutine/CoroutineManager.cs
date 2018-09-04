using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : Singleton<CoroutineManager> {
    
    //코루틴 딜레이 설정
	private static WaitForSeconds _delay = new WaitForSeconds(0.02f);
	public static WaitForSeconds Delay {  get { return _delay; } }

    private static WaitForSeconds _longDelay = new WaitForSeconds(0.1f);
	public static WaitForSeconds LongDelay { get { return _longDelay; } }

	private static WaitForSeconds _shortDelay = new WaitForSeconds(0.005f);
	public static WaitForSeconds ShortDelay { get { return _shortDelay; } }

    private static WaitForFixedUpdate _fiexdUpdate = new WaitForFixedUpdate();
	public static WaitForFixedUpdate FiexdUpdate { get { return _fiexdUpdate; } }

	private static WaitForEndOfFrame _endOfFrame = new WaitForEndOfFrame();
	public static WaitForEndOfFrame EndOfFrame { get { return _endOfFrame; } }

    private static WaitForSeconds _seconds;

    private Dictionary<string, IEnumerator> _preserveCoroutines = new Dictionary<string, IEnumerator>();

    public void AddPreserveCoroutine(string coroutineName, IEnumerator coroutine)
    {
        IEnumerator Cor = GetPreserveCoroutine(coroutineName, coroutine);

        if (Cor == null)
            _preserveCoroutines.Add(coroutineName, coroutine);
        else
            _preserveCoroutines[coroutineName] = coroutine;

    }
    
    public IEnumerator GetPreserveCoroutine(string coroutineName, IEnumerator coroutine)
    {
        IEnumerator ReturnCoroutine = null;

        if (_preserveCoroutines.ContainsKey(coroutineName))
        {
            ReturnCoroutine = _preserveCoroutines[coroutineName];
        }
        return ReturnCoroutine;
    }

    public void RemovePreserveCoroutine(string coroutineName)
    {
        if (!_preserveCoroutines.ContainsKey(coroutineName))
            _preserveCoroutines.Remove(coroutineName);

        Debug.Log("NoKey : " + coroutineName);
    }

    public static WaitForSeconds GetWaitForSeconds(float _time)
    {
        _seconds = new WaitForSeconds(_time);
        return _seconds;
    }
         
    
    //코루틴이 종료가 될 때까지...(실제로 코루틴 부분)
    IEnumerator Preform(IEnumerator coroutine)
    {
        yield return StartCoroutine(coroutine);
        Instance.StopCoroutine(coroutine);
    }

    //코루틴 시작
    public static void DoCoroutine(IEnumerator coroutine)
    {
        Instance.StartCoroutine(Instance.Preform(coroutine));
        Instance.AddPreserveCoroutine(coroutine.ToString(), coroutine);
    }

    public static void DontCoroutine(IEnumerator coroutine)
    {
        Instance.StopCoroutine(coroutine);
    }

    //코루틴 죽이기
    public void Die()
    {
        _instance = null;
        Destroy(gameObject);
    }

    //종료가 되면 _instance에 Null값
    public void OnApplicationQuit()
    {
        Die();
    }

}
