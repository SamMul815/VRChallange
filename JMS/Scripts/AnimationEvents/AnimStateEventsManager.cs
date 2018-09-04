using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct EvnData
{
    public string StateFuncName;
    public int IntParam;
    public float FloatParam;
    public string StringParam;
    public UnityEngine.Object ObjectParam;


    [Range(0.0f, 1.0f)]
    public float RunTime;
    public bool IsLoop;

    public EvnData(string stateFuncName, int intParam, float floatParam, string stringParam, float runTime, UnityEngine.Object objectParam, bool isLoop)
    {
        this.StateFuncName = stateFuncName;
        this.IntParam = intParam;
        this.FloatParam = floatParam;
        this.StringParam = stringParam;
        this.ObjectParam = objectParam;

        this.RunTime = runTime;
        this.IsLoop = isLoop;
    }
}



[RequireComponent(typeof(AnimStateEventCollection))]

public class AnimStateEventsManager : Singleton<AnimStateEventsManager>
{
    private Animator[] animators;

    private Dictionary<string, BaseSMB> animSMB = new Dictionary<string, BaseSMB>();

    private AnimStateEventCollection stateEventCollection;
    public AnimStateEventCollection StateEventCollection { get { return stateEventCollection; } }
  
    private void Awake()
    {
        animators = GetComponents<Animator>();
        stateEventCollection = GetComponent<AnimStateEventCollection>();

    }

    private void Start()
    {
        Dictionary<string, Action<EvnData>> EnterEventFunc = stateEventCollection.AnimEnterEventFunc;
        Dictionary<string, Action<EvnData>> ExitEventFunc = stateEventCollection.AnimExitEventFunc;
        Dictionary<string, List<Action<EvnData>>> TimeEventFunc = stateEventCollection.AnimTimeEventFunc;

        foreach (Animator a in animators)
        {
            BaseSMB[] bsmbs = a.GetBehaviours<BaseSMB>();
            foreach (BaseSMB smb in bsmbs)
            {
                string SMBKey = smb.SMBKeyName;

                animSMB.Add(SMBKey, smb);

                if (EnterEventFunc.ContainsKey(SMBKey))
                    smb.SetStateEnterEvent(EnterEventFunc[SMBKey]);

                if (TimeEventFunc.ContainsKey(SMBKey))
                {
                    foreach (Action<EvnData> action in TimeEventFunc[SMBKey])
                    {
                        stateEventCollection.AddAnimTimeEventFunc(TimeEventFunc, action, SMBKey);
                        stateEventCollection.AddIsAnimTimeEventRun(false, SMBKey);
                    }
                    smb.IsRunning = stateEventCollection.IsAnimTimeEventRun[SMBKey];
                    smb.SetStateTimeEventLListener(stateEventCollection.AnimTimeEventFunc[SMBKey]);
                }

                if (ExitEventFunc.ContainsKey(SMBKey))
                    smb.SetStateExitEvent(ExitEventFunc[SMBKey]);

            }
        }
    }
}
