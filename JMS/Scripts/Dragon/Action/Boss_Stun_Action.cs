using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Boss_Stun_Action : ActionTask
{

    public override void OnStart()
    {
        float runTime = 1.7f;
        ActionCor = PainfulMotionCor(runTime);

        base.OnStart();
    }

    public override bool Run()
    {
        return false;
    }

    public override void OnEnd()
    {
        BlackBoard.Instance.IsNearHowling = true;
        BlackBoard.Instance.IsWeakPointAttack = false;
        base.OnEnd();
    }

    IEnumerator PainfulMotionCor(float runTime)
    {
        DragonAniManager.SwicthAnimation("Painful_Right");
        yield return CoroutineManager.GetWaitForSeconds(runTime);
        OnEnd();
    }

}