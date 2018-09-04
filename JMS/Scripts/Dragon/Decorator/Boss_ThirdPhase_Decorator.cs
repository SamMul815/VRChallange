using DragonController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_ThirdPhase_Decorator : DecoratorTask
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        float CurHP = DragonManager.Stat.HP;
        float MaxHP = DragonManager.Stat.MaxHP;
        float PhasePercent = DragonManager.Stat.ThirdPhaseHpPercent;

        if (CurHP <= MaxHP * PhasePercent)
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
