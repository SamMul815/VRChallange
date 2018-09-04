using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DragonController;

public class BaseAnimStateEventsCollection : MonoBehaviour
{
    protected Dictionary<string, Action<EvnData>> _animEnterEventFunc = new Dictionary<string, Action<EvnData>>();
    public Dictionary<string, Action<EvnData>> AnimEnterEventFunc { get { return _animEnterEventFunc; } }

    protected Dictionary<string, Action<EvnData>> _animExitEventFunc = new Dictionary<string, Action<EvnData>>();
    public Dictionary<string, Action<EvnData>> AnimExitEventFunc { get { return _animExitEventFunc; } }

    protected Dictionary<string, List<Action<EvnData>>> _animTimeEventFunc = new Dictionary<string, List<Action<EvnData>>>();
    public Dictionary<string, List<Action<EvnData>>> AnimTimeEventFunc { get { return _animTimeEventFunc; } }

    protected Dictionary<string, List<bool>> isAnimTimeEventRun = new Dictionary<string, List<bool>>();
    public Dictionary<string, List<bool>> IsAnimTimeEventRun { get { return isAnimTimeEventRun; } }

    protected virtual void Awake()
    {

    }

    protected List<bool> GetIsAnimTimeEventRun(Dictionary<string, List<bool>> Target, string Key)
    {
        List<bool> IsAnimEventRun;
        if (Target.ContainsKey(Key))
        {
            IsAnimEventRun = Target[Key];
        }
        else
        {
            IsAnimEventRun = new List<bool>();
            Target[Key] = IsAnimEventRun;
        }
        return IsAnimEventRun;
    }

    public void AddIsAnimTimeEventRun(bool IsAnimRun, string Key)
    {
        List<bool> IsAnimRunList = GetIsAnimTimeEventRun(isAnimTimeEventRun, Key);
        IsAnimRunList.Add(IsAnimRun);
    }

    protected List<Action<EvnData>> GetAnimTimeEventFunc(Dictionary<string, List<Action<EvnData>>> Target, string Key)
    {
        List<Action<EvnData>> evnDataFunc;
        if (Target.ContainsKey(Key))
        {
            evnDataFunc = Target[Key];
        }
        else
        {
            evnDataFunc = new List<Action<EvnData>>();
            Target[Key] = evnDataFunc;
        }

        return evnDataFunc;
    }

    public void AddAnimTimeEventFunc(Dictionary<string, List<Action<EvnData>>> original, Action<EvnData> evnData, string Key)
    {
        List<Action<EvnData>> evnDataFunc = GetAnimTimeEventFunc(original, Key);

        if (!evnDataFunc.Contains(evnData))
        {
            evnDataFunc.Add(evnData);
        }

    }

    protected Action<EvnData> GetAnimEnterExitEventFunc(Dictionary<string, Action<EvnData>> Target, string Key)
    {
        Action<EvnData> evnDataFunc;
        if (Target.ContainsKey(Key))
        {
            evnDataFunc = Target[Key];
        }
        else
        {
            return null;
        }

        return evnDataFunc;
    }

    public void AddAnimEnterEventFunc(Dictionary<string, Action<EvnData>> original , Action<EvnData> evnData, string Key)
    {
        Action<EvnData> evnDataFunc = GetAnimEnterExitEventFunc(original, Key);

        if (evnDataFunc == null)
        {
            original.Add(Key, evnData);
        }
    }


}
