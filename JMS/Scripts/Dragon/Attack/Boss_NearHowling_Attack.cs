using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Boss_NearHowling_Attack : ActionTask
{
    public override void OnStart()
    {
        float preTime = BlackBoard.Instance.GetGroundTime().PreNearHowlingTime;
        float runTime = BlackBoard.Instance.GetGroundTime().RunNearHowlingTime;
        float afterTime = BlackBoard.Instance.GetGroundTime().AfterNearHowlingTime;

        BlackBoard.Instance.IsGroundAttacking = true;
        BlackBoard.Instance.IsNearHowlingAttacking = true;

        ActionCor = NearHowlingCor(preTime, runTime, afterTime);

        base.OnStart();
    }

    public override bool Run()
    {
        return false;
    }

    public override void OnEnd()
    {
        ParticleManager.Instance.PoolParticleEffectOff("NearHowling");

        Transform Dragon = UtilityManager.Instance.DragonTransform();
        Transform Player = UtilityManager.Instance.PlayerTransform();

        float WalkDistance = BlackBoard.Instance.RushDistance;

        BlackBoard.Instance.IsWalk =
            !UtilityManager.DistanceCalc(Dragon, Player, WalkDistance);

        BlackBoard.Instance.GetGroundTime().CurWalkTime =
            (BlackBoard.Instance.IsWalk) ? 0.0f : BlackBoard.Instance.GetGroundTime().MaxWalkTime;

        BlackBoard.Instance.IsGroundAttacking = false;
        BlackBoard.Instance.IsNearHowlingAttacking = false;
        BlackBoard.Instance.IsNearHowling = false;

        WeakPointManager.Instance.CurrentPatternCount++;

        base.OnEnd();
    }

    IEnumerator NearHowlingCor(float preTime, float runTime ,float afterTime)
    {
        Transform Dragon = UtilityManager.Instance.DragonTransform();
        Transform Player = UtilityManager.Instance.PlayerTransform();

        float Range = DragonManager.Stat.NearHowlingRange;
        float Damage = DragonManager.Stat.NearHowlingDamage;

        Transform DragonMouth = BlackBoard.Instance.DragonMouth;
        FMODSoundManager.Instance.PlayBossHowling(DragonMouth.position);

        //선딜
        ParticleManager.Instance.PoolParticleEffectOn("NearHowling");
        DragonAniManager.SwicthAnimation("NearHowling_Atk_Pre");
        yield return CoroutineManager.GetWaitForSeconds(preTime);

        //런
        DragonAniManager.SwicthAnimation("NearHowling_Atk_Run");

        UtilityManager.Instance.ShakePlayerHowling();

        if (UtilityManager.DistanceCalc(Dragon, Player, Range))
        {
            UtilityManager.Instance.AttackPlayer(Damage);
        }
        yield return CoroutineManager.GetWaitForSeconds(runTime);

        //후딜
        DragonAniManager.SwicthAnimation("NearHowling_Atk_After");
        yield return CoroutineManager.GetWaitForSeconds(afterTime);


        OnEnd();

    }

}
