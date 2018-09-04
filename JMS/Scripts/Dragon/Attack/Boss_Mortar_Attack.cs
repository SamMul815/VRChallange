using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Mortar_Attack : ActionTask
{
    public override void OnStart()
    {
        BlackBoard.Instance.IsGroundAttacking = true;
        BlackBoard.Instance.IsMortarAttacking = true;

        float preTime = BlackBoard.Instance.GetGroundTime().PreMortarTime;
        float runTime = BlackBoard.Instance.GetGroundTime().RunMortarTime;
        float afterTime = BlackBoard.Instance.GetGroundTime().AfterMortarTime;

        CoroutineManager.DoCoroutine(MortarAttackCor(preTime, runTime ,afterTime));

        base.OnStart();
    }

    public override bool Run()
    {

        Debug.Log(this.gameObject.name + " : Run");

        return false;
    }

    public override void OnEnd()
    {
        Transform Dragon = UtilityManager.Instance.DragonTransform();
        Transform Player = UtilityManager.Instance.PlayerTransform();

        float OverLapDistance = BlackBoard.Instance.RushDistance;

        BlackBoard.Instance.IsOverLapAttack =
            (UtilityManager.DistanceCalc(Dragon, Player, OverLapDistance)) ? false : true;

        BlackBoard.Instance.IsMortarAttacking = false;
        BlackBoard.Instance.IsGroundAttacking = false;

        WeakPointManager.Instance.CurrentPatternCount++;

        base.OnEnd();
    }


    IEnumerator MortarAttackCor(float preTime, float runTime ,float atfetTime)
    {
        //선딜
        yield return CoroutineManager.GetWaitForSeconds(preTime);
        //실행
        yield return CoroutineManager.GetWaitForSeconds(runTime);
        //후딜
        yield return CoroutineManager.GetWaitForSeconds(atfetTime);
        _isEnd = true;

    }


}
