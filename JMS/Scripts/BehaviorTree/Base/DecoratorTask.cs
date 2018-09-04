using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public abstract class DecoratorTask : TreeNode
{
    protected TreeNode _childNode;
    public TreeNode ChildNode { get { return _childNode; } }

    public override void OnStart()
    {
        if (ChildNode.GetComponent<ActionTask>())
        {
            ActionTask childAction = ChildNode.GetComponent<ActionTask>();

            if (DragonManager.Instance.PreserveActionTask)
                if (!DragonManager.Instance.PreserveActionTask.IsEnd)
                    DragonManager.Instance.PreserveActionTask.OnEnd();

            DragonManager.Instance.PreserveActionTask = childAction;
            ChildNode.NodeState = TASKSTATE.RUNNING;
            ChildNode.OnStart();
        }
        _nodeState = TASKSTATE.RUNNING;
        base.OnStart();
    }

    public override void OnEnd()
    {
        if (ChildNode.GetComponent<ActionTask>())
        {
            ActionTask childAction = ChildNode.GetComponent<ActionTask>();
            ChildNode.OnEnd();
        }
        _nodeState = TASKSTATE.FAULURE;
        base.OnEnd();
    }

    public override void ChildAdd(TreeNode node)
    {
        _childNode = node;
        base.ChildAdd(node);
    }

}
