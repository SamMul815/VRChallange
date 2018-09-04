using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Boss_RightPow_Attack : ActionTask
{

    public override void OnStart()
    {
        float preTime = BlackBoard.Instance.GetGroundTime().SecondAttackPreTime;
        float runTime = BlackBoard.Instance.GetGroundTime().SecondAttackRunTime;
        float afterTime = BlackBoard.Instance.GetGroundTime().SecondAttackAfterTime;
        
        BlackBoard.Instance.IsSecondAttacking = true;
        BlackBoard.Instance.IsRightPowAttacking = true;

        ActionCor = RightPowAttackCor(preTime, runTime, afterTime);

        base.OnStart();
    }

    public override bool Run()
    {

        return false;
    }

    public override void OnEnd()
    {


        ParticleManager.Instance.PoolParticleEffectOff("RightPow");
        ParticleManager.Instance.PoolParticleEffectOff("RClaw");

        BlackBoard.Instance.IsSecondAttack = false;
        BlackBoard.Instance.IsSecondAttacking = false;
        BlackBoard.Instance.IsGroundAttacking = false;
        BlackBoard.Instance.IsRightPowAttacking = false;

        WeakPointManager.Instance.CurrentPatternCount++;

        base.OnEnd();
    }


    IEnumerator RightPowAttackCor(float preTime, float runTime, float afterTime)
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

        DragonAniManager.SwicthAnimation("RightPow_Atk_Pre");

        ParticleManager.Instance.PoolParticleEffectOn("RightPow");
        ParticleManager.Instance.PoolParticleEffectOn("RClaw");

        yield return CoroutineManager.GetWaitForSeconds(preTime);

        //런 애니메이션
        DragonAniManager.SwicthAnimation("RightPow_Atk_Run");
        yield return CoroutineManager.GetWaitForSeconds(runTime);

        //후딜 애니메이션
        DragonAniManager.SwicthAnimation("RightRow_Atk_After");
        yield return CoroutineManager.GetWaitForSeconds(afterTime);

        OnEnd();

    }

}
