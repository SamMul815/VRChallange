using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_SecondAttacks_Decorator : DecoratorTask
{
    public override void OnStart()
    {
        Debug.Log(this.gameObject.name + " : OnStart");
        base.OnStart();
    }

    public override bool Run()
    {

        bool IsSecondAttack = BlackBoard.Instance.IsSecondAttack;

        if (IsSecondAttack)
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
        Debug.Log(this.gameObject.name + " : OnEnd");
        base.OnEnd();
    }
}
