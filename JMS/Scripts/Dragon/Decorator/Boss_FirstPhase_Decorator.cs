
using DragonController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_FirstPhase_Decorator : DecoratorTask
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        float CurHP = DragonManager.Stat.HP;
        float MaxHP = DragonManager.Stat.MaxHP;
        float PhasePercent = DragonManager.Stat.FirstPhaseHpPercent;

        if (CurHP <= MaxHP * PhasePercent)
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

        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
