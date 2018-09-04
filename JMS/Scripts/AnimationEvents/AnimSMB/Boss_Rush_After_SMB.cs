using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Rush_After_SMB : BaseSMB {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Transform Boss = UtilityManager.Instance.DragonTransform();
        //Transform Player = UtilityManager.Instance.PlayerTransform();

        //float SecondAttackDistance = BlackBoard.Instance.SecondAttackDistance;

        //BlackBoard.Instance.IsSecondAttack = UtilityManager.DistanceCalc(Boss, Player, SecondAttackDistance);
        //BlackBoard.Instance.IsGroundAttacking = (BlackBoard.Instance.IsSecondAttack) ? true : false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
