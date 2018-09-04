using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public class Boss_Walk_Action : ActionTask
{
    public override void OnStart()
    {
        base.OnStart();
        DragonAniManager.SwicthAnimation("Walk");
    }

    public override bool Run()
    {
        Transform Dragon = UtilityManager.Instance.DragonTransform();
        Transform Player = UtilityManager.Instance.PlayerTransform();

        Vector3 DragonPos = UtilityManager.Instance.DragonPosition();
        Vector3 PlayerPos = UtilityManager.Instance.PlayerPosition();

        DragonPos.y = 0.0f;
        PlayerPos.y = 0.0f;

        Vector3 forward = (PlayerPos - DragonPos).normalized;

        if (Vector3.Dot(Dragon.forward, forward) < 0.99f)
        {
            Dragon.rotation = Quaternion.Slerp(
                    Dragon.rotation,
                    Quaternion.LookRotation(forward),
                    0.05f);
        }

        Dragon.position = Vector3.MoveTowards(
            Dragon.position,
            Player.position,
            10.0f * Time.deltaTime
            );

        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
