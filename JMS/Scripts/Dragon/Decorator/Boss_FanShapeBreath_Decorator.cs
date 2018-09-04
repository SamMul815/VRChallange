using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_FanShapeBreath_Decorator : DecoratorTask
{
    public override void OnStart()
    {
        Debug.Log(this.gameObject.name + " : OnStart");
        base.OnStart();
    }

    public override bool Run()
    {

        Transform Dragon = UtilityManager.Instance.DragonTransform();
        Transform Player = UtilityManager.Instance.PlayerTransform();

        float FanShapeBreathDistance = BlackBoard.Instance.FanShapeBreathDistance;

        bool IsFanShapeBreath = UtilityManager.DistanceCalc(Dragon, Player, FanShapeBreathDistance);

        bool IsGroundAttacking = BlackBoard.Instance.IsGroundAttacking;
        bool IsFanShapeBreathAttacking = BlackBoard.Instance.IsFanShapeBreathAttacking;

        if ((IsFanShapeBreath && !IsGroundAttacking) || IsFanShapeBreathAttacking)
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
        {
            Debug.Log("test");
            OnEnd();
        }

        return true;
    }

    public override void OnEnd()
    {
        Debug.Log(this.gameObject.name + "OnEnd");
        base.OnEnd();
    }


}
