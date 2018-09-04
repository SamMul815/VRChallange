using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_MortarAttack_Decorator : DecoratorTask
{

    public override bool Run()
    {
        //Transform Dragon = UtilityManager.Instance.DragonTransform();
        //Transform Player = UtilityManager.Instance.PlayerTransform();

        //float MortarDistanceLimit = BlackBoard.Instance.MortarDistance;

        //bool IsMortar = UtilityManager.DistanceCalc(Dragon, Player, MortarDistanceLimit);

        //bool IsMortarAttacking = BlackBoard.Instance.IsMortarAttacking;
        //bool IsGroundAttacking = BlackBoard.Instance.IsGroundAttacking;

        //if ((IsMortarAttacking && !IsGroundAttacking))
        //{
        //    Debug.Log("Martar_Attack_Decorator");

        //    return ChildNode.Run();
        //}

        return true;
    }

}
