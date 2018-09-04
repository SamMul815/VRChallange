using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_HowlingAttack_Decorator : DecoratorTask
{
    public override void OnStart()
    {
        Debug.Log(this.gameObject.name + " : OnStart");
        base.OnStart();
    }

    public override bool Run()
    {
        float Hp = DragonManager.Stat.HP;
        float MaxHP = DragonManager.Stat.MaxHP;

        float DamageReceiveHpPercent = DragonManager.Stat.DamageReceiveHpPercent;
        float SaveTakeDamage = DragonManager.Stat.SaveTakeDamage;

        bool IsHowling = (MaxHP * DamageReceiveHpPercent <= SaveTakeDamage);

        bool IsHowlingAttacking = BlackBoard.Instance.IsHowlingAttacking;
        bool IsGroundAttacking = BlackBoard.Instance.IsGroundAttacking;


        if ((IsHowling && !IsGroundAttacking) || IsHowlingAttacking)
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
        Debug.Log(this.gameObject.name + "OnEnd");
        base.OnEnd();
    }

}
