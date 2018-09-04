using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_NearBreathAttack_Decorator : DecoratorTask
{
    public override void OnStart()
    {
        base.OnStart();
    }


    public override bool Run()
    {
        Transform Dragon = UtilityManager.Instance.DragonTransform();
        Transform Player = UtilityManager.Instance.PlayerTransform();

        float NearBreathDistance = BlackBoard.Instance.NearBreathDistance;

        bool IsBulletBreath = UtilityManager.DistanceCalc(Dragon, Player, NearBreathDistance);

        bool IsNearBreathAttacking = BlackBoard.Instance.IsNearBreathAttacking;
        bool IsGroundAttacking = BlackBoard.Instance.IsGroundAttacking;

        if ((IsBulletBreath && !IsGroundAttacking) || IsNearBreathAttacking)
        {
            ActionTask childAction = ChildNode.GetComponent<ActionTask>();

            if (childAction)
            {
                if (NodeState != TASKSTATE.RUNNING || childAction.IsEnd)
                {
                    OnStart();
                }
            }
            else if (NodeState != TASKSTATE.RUNNING)
                OnStart();

            return ChildNode.Run();
        }
        if (NodeState != TASKSTATE.FAULURE)
            OnEnd();

        return true;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }


}
