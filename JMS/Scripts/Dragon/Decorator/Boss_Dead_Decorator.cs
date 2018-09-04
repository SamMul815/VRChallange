using DragonController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Dead_Decorator : DecoratorTask
{
    public override bool Run()
    {
        float CurHP = DragonManager.Stat.HP;

        if (CurHP <= 0.0f)
        {
            ActionTask childAction = ChildNode.GetComponent<ActionTask>();

            if (childAction)
                if (NodeState != TASKSTATE.RUNNING || childAction.IsEnd)
                    OnStart();

                else if (NodeState != TASKSTATE.RUNNING)
                    OnStart();

            return ChildNode.Run();
        }

        if (NodeState != TASKSTATE.FAULURE)
            OnEnd();

        return true;
    }

}
