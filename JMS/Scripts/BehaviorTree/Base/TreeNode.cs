using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TASKSTATE
{
    SUCCESS = 0,
    FAULURE,
    RUNNING
}

public abstract class TreeNode : MonoBehaviour {

    protected TASKSTATE _nodeState;
    public TASKSTATE NodeState { set { _nodeState = value; } get { return _nodeState; } }

    public virtual void ChildAdd(TreeNode node)
    {
    }

    public virtual void OnStart()
    {
        NodeState = TASKSTATE.RUNNING;
    }
    public abstract bool Run();
    public virtual void OnEnd()
    {
        NodeState = TASKSTATE.FAULURE;
    }

}
