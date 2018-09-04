using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Groggy_Action : ActionTask
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        Debug.Log("Groggy.Action");
        return false;
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }

}
