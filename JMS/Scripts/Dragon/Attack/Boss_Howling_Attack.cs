using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Boss_Howling_Attack : ActionTask
{
    public override void OnStart()
    {
        Debug.Log(this.gameObject.name + ": OnStart");

        float preTime = BlackBoard.Instance.GetGroundTime().PreHowlingTime;
        float runTime = BlackBoard.Instance.GetGroundTime().RunHowlingTime;
        float afterTime = BlackBoard.Instance.GetGroundTime().AfterHowlingTime;

        ActionCor = HowlingAttackCor(preTime,runTime, afterTime);

        base.OnStart();
    }

    public override bool Run()
    {
        return false;
    }

    public override void OnEnd()
    {
        Debug.Log(this.gameObject.name + ": OnEnd");

        DragonManager.Stat.SaveTakeDamage = 0.0f;
        BlackBoard.Instance.IsHowlingAttacking = false;
        BlackBoard.Instance.IsGroundAttacking = false;

        WeakPointManager.Instance.CurrentPatternCount++;

        base.OnEnd();
    }

    IEnumerator HowlingAttackCor(float preTime, float runTime ,float afterTime)
    {
        Transform Dragon = UtilityManager.Instance.DragonTransform();

        Vector3 DragonPos = UtilityManager.Instance.DragonPosition();
        Vector3 PlayerPos = UtilityManager.Instance.PlayerPosition();

        DragonPos.y = 0.0f;
        PlayerPos.y = 0.0f;

        BlackBoard.Instance.IsGroundAttacking = true;
        BlackBoard.Instance.IsHowlingAttacking = true;
        Transform mouth = BlackBoard.Instance.DragonMouth;

        Vector3 forward = (PlayerPos - DragonPos).normalized;

        while (Vector3.Dot(Dragon.forward, forward) < 0.99f)
        {
            Dragon.rotation =
                Quaternion.Slerp(
                    Dragon.rotation,
                    Quaternion.LookRotation(forward),
                    0.1f
                    );

            yield return CoroutineManager.FiexdUpdate;
        }

        DragonAniManager.SwicthAnimation("Howling_Atk_Pre");
        yield return CoroutineManager.GetWaitForSeconds(preTime);

        //런 타임
        DragonAniManager.SwicthAnimation("Howling_Atk_Run");
        yield return CoroutineManager.GetWaitForSeconds(runTime);

        //후딜 
        DragonAniManager.SwicthAnimation("Howling_Atk_After");
        yield return CoroutineManager.GetWaitForSeconds(afterTime);

        _isEnd = true;

    }
}
