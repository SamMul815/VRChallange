using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Dragon_Howling_Attack : ActionTask
{
    public override void OnStart()
    {
        float preTime = BlackBoard.Instance.GetGroundTime().PreHowlingTime;
        float runTime = BlackBoard.Instance.GetGroundTime().RunHowlingTime;
        float afterTime = BlackBoard.Instance.GetGroundTime().AfterHowlingTime;

        ActionCor = HowlingAttackCor(preTime, runTime, afterTime);

        base.OnStart();
    }

    public override bool Run()
    {
        Debug.Log("Test");
        return false;
    }

    public override void OnEnd()
    {
        BlackBoard.Instance.IsHowlingAttacking = false;
        BlackBoard.Instance.IsGroundAttacking = false;

        WeakPointManager.Instance.CurrentPatternCount++;
        base.OnEnd();
    }

    IEnumerator HowlingAttackCor(float preTime, float runTime, float afterTime)
    {
        yield return CoroutineManager.GetWaitForSeconds(preTime);

        yield return CoroutineManager.GetWaitForSeconds(runTime);

        yield return CoroutineManager.GetWaitForSeconds(afterTime);

        DragonManager.Stat.SaveTakeDamage = 0.0f;

    }

}
