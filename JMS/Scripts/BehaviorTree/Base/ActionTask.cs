using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonController;

public abstract class ActionTask : TreeNode
{
    protected bool _isEnd = false;
    public bool IsEnd { set { _isEnd = value; } get { return _isEnd; } }

    protected IEnumerator _actionCor;
    public IEnumerator ActionCor { set { _actionCor = value; } get { return _actionCor; } }

    public override void OnStart()
    {
        NodeState = TASKSTATE.RUNNING;
        _isEnd = false;
        if (_actionCor != null)
        {
            CoroutineManager.DoCoroutine(_actionCor);
        }
        base.OnStart();
    }

    public override void OnEnd()
    {
        if (ActionCor != null)
        {
            CoroutineManager.DontCoroutine(_actionCor);
        }

        base.OnEnd();
        _isEnd = true;
        NodeState = TASKSTATE.FAULURE;
    }

}
