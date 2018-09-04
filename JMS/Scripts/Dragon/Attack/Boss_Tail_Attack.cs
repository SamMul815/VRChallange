using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Boss_Tail_Attack : ActionTask
{
    public override void OnStart()
    {
        float preTime = BlackBoard.Instance.GetGroundTime().PreNearHowlingTime;
        float runTime = BlackBoard.Instance.GetGroundTime().RunNearHowlingTime;
        float afterTime = BlackBoard.Instance.GetGroundTime().AfterNearHowlingTime;

        BlackBoard.Instance.IsSecondAttacking = true;
        BlackBoard.Instance.IsTailAttacking = true;

        ActionCor = TailAttackCor(preTime, runTime, afterTime);

        base.OnStart();
    }

    public override bool Run()
    {
        return false;
    }

    public override void OnEnd()
    {
        ParticleManager.Instance.PoolParticleEffectOff("NearHowling");

        BlackBoard.Instance.IsTailAttacking = false;
        BlackBoard.Instance.IsSecondAttack = false;
        BlackBoard.Instance.IsSecondAttacking = false;
        BlackBoard.Instance.IsGroundAttacking = false;

        WeakPointManager.Instance.CurrentPatternCount++;

        base.OnEnd();
    }


    IEnumerator TailAttackCor(float preTime, float runTime ,float afterTime)
    {
        Transform DragonMouth = BlackBoard.Instance.DragonMouth;
        FMODSoundManager.Instance.PlayBossHowling(DragonMouth.position);

        ParticleManager.Instance.PoolParticleEffectOn("NearHowling");
        DragonAniManager.SwicthAnimation("NearHowling_Atk_Pre");
        yield return CoroutineManager.GetWaitForSeconds(preTime);

        //런        
        UtilityManager.Instance.ShakePlayerHowling(); 
        DragonAniManager.SwicthAnimation("NearHowling_Atk_Run");
        yield return CoroutineManager.GetWaitForSeconds(runTime);

        //후딜
        DragonAniManager.SwicthAnimation("NearHowling_Atk_After");
        yield return CoroutineManager.GetWaitForSeconds(afterTime);

        OnEnd();

    }

}
