using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Walk_Decorator : DecoratorTask
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {

        Transform Dragon = UtilityManager.Instance.DragonTransform();
        Transform Player = UtilityManager.Instance.PlayerTransform();

        float WalkDistance = BlackBoard.Instance.NearBreathDistance;

        bool IsWalkDistance = !UtilityManager.DistanceCalc(Dragon, Player, WalkDistance);
        bool IsGroundAttacking = BlackBoard.Instance.IsGroundAttacking;

        if ((!IsGroundAttacking && IsWalkDistance))
        {
            ActionTask childAction = ChildNode.GetComponent<ActionTask>();

            if (childAction)
            {
                if (NodeState != TASKSTATE.RUNNING || childAction.IsEnd)
                    OnStart();
            }
            else if (NodeState != TASKSTATE.RUNNING)
                OnStart();

            return ChildNode.Run();
        }
        return true;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
