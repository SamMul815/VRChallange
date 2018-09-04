using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Flying_Decorator : DecoratorTask
{

    public override bool Run()
    {
        bool IsGround = BlackBoard.Instance.IsGround;
        bool IsFlying = BlackBoard.Instance.IsFlying;

        bool IsTakeOff = BlackBoard.Instance.IsTakeOff;
        bool IsLanding = BlackBoard.Instance.IsLanding;

        if (IsFlying && !IsTakeOff && !IsLanding && !IsGround)
        {
            return ChildNode.Run();
        }
        return false;
    }
}
