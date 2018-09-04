using DragonController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Selector : CompositeTask
{
    public override void OnStart()
    {
        base.OnStart();
    }

    public override bool Run()
    {
        foreach (TreeNode child in ChildNodes)
        {
            if (child.Run())
            {
                if (child.GetComponent<ActionTask>())
                {
                    ActionTask childAction = child.GetComponent<ActionTask>();

                    if (child.NodeState != TASKSTATE.RUNNING || childAction.IsEnd)
                    {
                        if (DragonManager.Instance.PreserveActionTask)
                        {
                            if (DragonManager.Instance.PreserveActionTask != childAction)
                                DragonManager.Instance.PreserveActionTask.OnEnd();
                        }
                        DragonManager.Instance.PreserveActionTask = childAction;

                        child.NodeState = TASKSTATE.RUNNING;
                        child.OnStart();
                    }
                }

                if (NodeState != TASKSTATE.RUNNING)
                    OnStart();

                return true;

            }

        }

        if (NodeState != TASKSTATE.FAULURE)
            OnEnd();

        return false; 
    }

    public override void OnEnd()
    {
        base.OnEnd();
    }


}
