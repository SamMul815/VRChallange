using DragonController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

/*
    수 정 날 : 2018 - 05 - 05
    작 성 자 : 전민수
    수정내역 : 빌드 버그 수정
*/

[CreateAssetMenu(menuName = "BehaviorTree/Tree")]
public class BehaviorTree : ScriptableObject
{

    [SerializeField]
    private CompositeTask _root;
    public CompositeTask Root { get { return _root; } }

    //private List<TreeNode> _preserveTasks = new List<TreeNode>();
    //public List<TreeNode> PreserveTasks { get { return _preserveTasks; } }

    bool _isInit = false;

    public void Awake()
    {
        if (!_isInit) { 
            if (_root != null)
            {
                if (_root.ChildNodes != null)
                {
                    SerializeNodes(_root);
                    _isInit = true;
                }
            }
        }
    }

    public void SerializeNodes(TreeNode node)
    {
        TreeNode[] ChildNodes = node.GetComponentsInChildren<TreeNode>();

        for (int i = 1; i < ChildNodes.Length; i++)
        {
            int n = i;
            i += ChildNodes[i].GetComponentsInChildren<TreeNode>().Length - 1;

            if (!_isInit)
            {
                node.ChildAdd(ChildNodes[n]);
                //PreserveTasks.Add(ChildNodes[n]);
                //Debug.Log("ChildInit : " + ChildNodes[n]);
            }
            ChildNodes[n].NodeState = TASKSTATE.FAULURE;
            SerializeNodes(ChildNodes[n]);
        }
    }

    public void Initialize(TreeNode node)
    {
        TreeNode[] ChildNodes = node.GetComponentsInChildren<TreeNode>();

        for (int i = 1; i< ChildNodes.Length; i++)
        {
            int n = i;
            i += ChildNodes[i].GetComponentsInChildren<TreeNode>().Length - 1;
            ChildNodes[n].NodeState = TASKSTATE.FAULURE;
            Initialize(ChildNodes[n]);
        }

    }

    public void OnEnable()
    {
        Initialize(_root);
    }

#if UNITY_EDITOR
    public static BehaviorTree Create()
    {
        BehaviorTree asset = CreateInstance<BehaviorTree>();

        AssetDatabase.CreateAsset(asset, "Assets/BehaviroTree.asset");
        AssetDatabase.SaveAssets();

        return asset;
    }
#endif
}