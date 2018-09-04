using DragonController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Groggy_Decorator : DecoratorTask
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        float CurHP = DragonManager.Stat.HP;
        float MaxHP = DragonManager.Stat.MaxHP;
        float GroggyHP = MaxHP * DragonManager.Stat.GroggyHpPercent;

        if (CurHP <= GroggyHP)
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

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
