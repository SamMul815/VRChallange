using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Boss_LeftPow_Attack : ActionTask
{
    public override void OnStart()
    {
        float preTime = BlackBoard.Instance.GetGroundTime().SecondAttackPreTime;
        float runTime = BlackBoard.Instance.GetGroundTime().SecondAttackRunTime;
        float afterTime = BlackBoard.Instance.GetGroundTime().SecondAttackAfterTime;

        BlackBoard.Instance.IsSecondAttacking = true;
        BlackBoard.Instance.IsLeftPowAttacking = true;

        ActionCor = LefrPowAttackCor(preTime, runTime, afterTime);

        base.OnStart();
    }

    public override bool Run()
    {
        return false;
    }

    public override void OnEnd()
    {

        ParticleManager.Instance.PoolParticleEffectOff("LeftPow");
        ParticleManager.Instance.PoolParticleEffectOff("LClaw");

        BlackBoard.Instance.IsSecondAttacking = false;
        BlackBoard.Instance.IsSecondAttack = false;
        BlackBoard.Instance.IsGroundAttacking = false;
        BlackBoard.Instance.IsLeftPowAttacking = false;

        WeakPointManager.Instance.CurrentPatternCount++;
        base.OnEnd();
    }

    IEnumerator LefrPowAttackCor(float preTime, float runTime ,float afterTime)
    {   

        //선딜 애니메이션  
        Transform Dragon = UtilityManager.Instance.DragonTransform();
        Transform Player = UtilityManager.Instance.PlayerTransform();

        Vector3 DragonPos = Dragon.position;
        Vector3 PlayerPos = Player.position;

        DragonPos.y = 0.0f;
        PlayerPos.y = 0.0f;

        Vector3 forward = (PlayerPos - DragonPos).normalized;

        while (Vector3.Dot(Dragon.forward, forward) < 0.99f)
        {
            DragonAniManager.SwicthAnimation("Walk");
            Dragon.rotation = Quaternion.Slerp(Dragon.rotation, Quaternion.LookRotation(forward), 0.05f);
            yield return CoroutineManager.FiexdUpdate;
        }

        DragonAniManager.SwicthAnimation("LeftPow_Atk_Pre");

        //선딜
        ParticleManager.Instance.PoolParticleEffectOn("LeftPow");
        ParticleManager.Instance.PoolParticleEffectOn("LClaw");

        yield return new WaitForSeconds(preTime);

        //런 애니메이션
        DragonAniManager.SwicthAnimation("LeftPow_Atk_Run");
        yield return new WaitForSeconds(runTime);

        //후딜
        DragonAniManager.SwicthAnimation("LeftPow_Atk_After");
        yield return new WaitForSeconds(afterTime);
        OnEnd();
    }
}
