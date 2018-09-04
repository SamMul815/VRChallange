using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSMB : StateMachineBehaviour
{

    public string SMBKeyName;

    public EvnData StateEnterEvnData;
    public EvnData StateExitEvnData;
    public List<EvnData> StateTimeEvent;

    protected Action<EvnData> onStateEnterEventListener;
    protected Action<EvnData> onStateExitEventListener;
    protected List<Action<EvnData>> onStateTimeEventListener;
    //protected Action<EvnData> onStateTimeEventListener;

    protected bool beginExit = false;
    protected bool waitingToBegin = false;

    protected List<bool> isRunning;
    public List<bool> IsRunning { set { isRunning = value; } get { return isRunning; } }

    public virtual void Awake()
    {

    }

    public void InitRunning(List<bool> Running)
    {

        if (onStateTimeEventListener != null)
        {
            for (int index = 0; index < Running.Count; index++)
            {
                if (Running[index])
                    Running[index] = false;
            }

        }
    }


    public virtual void SetStateEnterEvent(Action<EvnData> action)
    {
        onStateEnterEventListener += action;
    }

    public virtual void SetStateExitEvent(Action<EvnData> action)
    {
        onStateExitEventListener += action;
    }

    public virtual void SetStateTimeEventLListener(List<Action<EvnData>> actions)
    {
        //Debug.Log(onStateTimeEventListener.Count);
        onStateTimeEventListener = actions;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (animator.IsInTransition(layerIndex))
            waitingToBegin = true;
        else
            waitingToBegin = false;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        InitRunning(isRunning);
    }

    //public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    base.OnStateMove(animator, stateInfo, layerIndex);
    //}

    //public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    base.OnStateUpdate(animator, stateInfo, layerIndex);
    //}
}