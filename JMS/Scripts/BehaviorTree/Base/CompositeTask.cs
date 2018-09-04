using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CompositeTask : TreeNode
{
    private List<TreeNode> _childNodes = new List<TreeNode>();
    public List<TreeNode> ChildNodes { get { return _childNodes; } }

    public override void OnStart()
    {
        NodeState = TASKSTATE.RUNNING;
        base.OnStart();
    }

    public override void OnEnd()
    {
        NodeState = TASKSTATE.FAULURE;
        base.OnEnd();
    }

    public override void ChildAdd(TreeNode node)
    {
        ChildNodes.Add(node);
        base.ChildAdd(node);
    }
}
