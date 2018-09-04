using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Boss_Idle_Action : ActionTask
{
    public override void OnStart()
    {
        DragonAniManager.SwicthAnimation("Idle");
        base.OnStart();
    }

    public override bool Run()
    {
        BlackBoard.Instance.GetGroundTime().CurIdleTime += Time.deltaTime;
        return true;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
