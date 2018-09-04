using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPow_Attack_Run_SMB : BaseSMB {

    public override void Awake()
    {
        base.Awake();
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (onStateEnterEventListener != null)
        {
            onStateEnterEventListener(StateEnterEvnData);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!beginExit)
        {
            if (animator.IsInTransition(layerIndex))
            {
                if (!waitingToBegin)
                {    
                    /*
                     * This is the first frame that current state is transitioning Away
                     */

                    beginExit = true;
                }
            }
            else if (waitingToBegin)
            {
                waitingToBegin = false;
            }
        }

        if (onStateTimeEventListener != null)
        {
            for (int i = 0; i < onStateTimeEventListener.Count; i++)
            {
                float aniTime = Mathf.Round((stateInfo.normalizedTime) * 1000.0f) / 1000f;

                if (aniTime >= StateTimeEvent[i].RunTime)
                {
                    bool isRun = isRunning[i];

                    if (!isRun)
                    {
                        onStateTimeEventListener[i](StateTimeEvent[i]);
                        isRunning[i] = true;
                    }
                }
            }
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
