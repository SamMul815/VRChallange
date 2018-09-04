using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Idle_Decorator : DecoratorTask
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        float CurTime = BlackBoard.Instance.GetGroundTime().CurIdleTime;
        float MaxTime = BlackBoard.Instance.GetGroundTime().MaxIdleTime;

        bool IsIdle = BlackBoard.Instance.IsIdle;

        if (CurTime < MaxTime)
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
