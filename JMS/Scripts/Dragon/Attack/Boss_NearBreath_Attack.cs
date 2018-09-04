using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Boss_NearBreath_Attack : ActionTask
{

    public override void OnStart()
    {
        float preTime = BlackBoard.Instance.GetGroundTime().PreNearBreathTime;
        float runTime = BlackBoard.Instance.GetGroundTime().RunNearBreathTime;
        float afterTime = BlackBoard.Instance.GetGroundTime().AfterNearBreathTime;

        BlackBoard.Instance.IsNearBreathAttacking = true;
        BlackBoard.Instance.IsGroundAttacking = true;

        ActionCor = NearBreathCor(preTime, runTime, afterTime);

        base.OnStart();
    }

    public override bool Run()
    {
        return false;
    }

    public override void OnEnd()
    {
        ParticleManager.Instance.PoolParticleEffectOff("NearBreath");

        Transform Dragon = UtilityManager.Instance.DragonTransform();
        Transform Player = UtilityManager.Instance.PlayerTransform();

        float OverLapDistance = BlackBoard.Instance.RushDistance;

        BlackBoard.Instance.IsGroundAttacking = false;
        BlackBoard.Instance.IsNearBreathAttacking = false;

        BlackBoard.Instance.IsOverLapAttack =
            (UtilityManager.DistanceCalc(Dragon, Player, OverLapDistance)) ? false : true;


        WeakPointManager.Instance.CurrentPatternCount++;

        base.OnEnd();

    }

    IEnumerator NearBreathCor(float preTime, float runTime ,float afterTime)
    {

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


        //선딜
        DragonAniManager.SwicthAnimation("NearBreath_Atk_Pre");
        yield return CoroutineManager.GetWaitForSeconds(preTime);

        //실행
        ParticleManager.Instance.PoolParticleEffectOn("NearBreath");
        DragonAniManager.SwicthAnimation("NearBreath_Atk_Run");
        yield return CoroutineManager.GetWaitForSeconds(runTime);
        
        //후딜
        DragonAniManager.SwicthAnimation("NearBreath_Atk_After");
        yield return CoroutineManager.GetWaitForSeconds(afterTime);

        OnEnd();
    }

}
