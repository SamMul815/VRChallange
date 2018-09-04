using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_NearHowling_Decorator : DecoratorTask
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        Transform Dragon = UtilityManager.Instance.DragonTransform();
        Transform Player = UtilityManager.Instance.PlayerTransform();


        float NearHowlingDistance = BlackBoard.Instance.RushDistance;

        bool IsNearHowlingDistance = UtilityManager.DistanceCalc(Dragon, Player, NearHowlingDistance);


        bool IsNearHowling = BlackBoard.Instance.IsNearHowling;

        bool IsNearHowlingAttacking = BlackBoard.Instance.IsNearHowlingAttacking;
        bool IsGroundAttacking = BlackBoard.Instance.IsGroundAttacking;

        if ((IsNearHowlingDistance && IsNearHowling && !IsGroundAttacking) || IsNearHowlingAttacking)
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
