using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_OverLap_Decorator : DecoratorTask
{
    public override void OnStart()
    {
        base.OnStart();
    }


    public override bool Run()
    {
        bool IsOverLap = BlackBoard.Instance.IsOverLapAttack;

        bool IsGroundAttacking = BlackBoard.Instance.IsGroundAttacking;
        bool IsOverLapAttacking = BlackBoard.Instance.IsOverLapAttacking;

        bool IsRushAttacking = BlackBoard.Instance.IsRushAttacking;
        bool IsSecondAttack = BlackBoard.Instance.IsSecondAttack;

        if ((IsOverLap && !IsGroundAttacking) || 
            ((IsSecondAttack && !IsRushAttacking) || IsOverLapAttacking))
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

        if (NodeState != TASKSTATE.FAULURE)
            OnEnd();

        return true;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }


}
