using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Ground_Decorator : DecoratorTask
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        bool IsGround = BlackBoard.Instance.IsGround;
        bool IsFlying = BlackBoard.Instance.IsFlying;

        bool IsTakeOff = BlackBoard.Instance.IsTakeOff;
        bool IsLanding = BlackBoard.Instance.IsLanding;

        if (IsGround && !IsTakeOff && !IsLanding && !IsFlying)
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
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }
}
