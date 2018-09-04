using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    만 든 날 : 2018-03-29
    작 성 자 : 전민수

    노드 총 관리 Manager

    베지어곡선을 이용하여 노드 만들기     
*/

/*
 *  수정한날 : 2018 - 04 - 30
 *  작성자 : 김영민
 *  수정내역: 업벡터 관련 추가 리스트 생성
 */


[RequireComponent(typeof(MoveStat))]
public class NodeManager : MonoBehaviour {

    private MoveStat _stat;
    public MoveStat Stat { get { return _stat; } }

    public List<BezierNode> Nodes = new List<BezierNode>();    //노드들

    private List<Vector3> _nodesDir = new List<Vector3>();
    private List<Vector3> _nodesRot = new List<Vector3>();
    private List<bool> _nodesUp = new List<bool>();         //노드 Up vecter

    private List<float> _nodesSpeed = new List<float>();    //노드 Speed

    //private List<float> _maxNodeDis = new List<float>();
        
    //private Vector3 _curveNodeDir;      //현재노드, 커브노드의 거리 AB
    //private Vector3 _nextNodeDir;       //커브노드, 다음노드의 거리 BC

    private Vector3 _curveNodeCenter;   //E
    private Vector3 _nextNodeCenter;    //F

    private Vector3 _arriveNodePos;      //최종노드위치값

    public float NodeInterval = 0.02f; //dir / speed;

    public List<Vector3> NodesDir { get { return _nodesDir; } }
    public List<Vector3> NodesRot { get { return _nodesRot; } }
    public List<float> NodesSpeed { get { return _nodesSpeed; } }
    public List<bool> NodesDragonUp { get { return _nodesUp; } }

    public bool IsStick;
    public bool IsMoveLoop;
    public bool IsRotation;

    private bool _isMoveReady;
    public bool IsMoveReady { set { _isMoveReady = value; } get { return _isMoveReady; } }

    private bool _isMoveEnd;
    public bool IsMoveEnd { set { _isMoveEnd = value; } get { return _isMoveEnd; } }

    private int _curNodesIndex;
    public int CurNodesIndex { set { _curNodesIndex = value; } get { return _curNodesIndex; } }

    public Transform CenterAxisRot;

    public void Awake()
    {
        _stat = GetComponent<MoveStat>();
    }
    
    public void AllNodesCalc()  //전체 노드 거리 계산
    {
        Vector3 to = Nodes[0].transform.position;//다음좌표
        Vector3 from = Nodes[0].transform.position;//다음좌표

        for (int index = 0; index + 1 < Nodes.Count; index++) //초기 노드부터 최종 노드까지 돌아가
        {
            for (int segment = 0; segment < Nodes[index].NodeSegment; segment++)    //다음좌표가 다음 노드포지션 값이랑 일치하는지
            {
                float t = (1.0f / Nodes[index].NodeSegment) * segment;
                float tt = (1.0f - t);


                float speed = Mathf.Lerp(Nodes[index].NodeSpeed, Nodes[index + 1].NodeSpeed, t);
                
                Vector3 rot = Vector3.Slerp(Nodes[index].GetRotate.eulerAngles, Nodes[index + 1].GetRotate.eulerAngles, t);

                to = CalcNodePos(index, index + 1, tt, t);  //현재 좌표

                _nodesRot.Add(rot);
                _nodesSpeed.Add(speed);
                _nodesDir.Add(to);
                _nodesUp.Add(Nodes[index].DragonUp);

                t += NodeInterval; //시간 0~1
            }
        }

    }
    
    public Vector3 CalcNodePos(int Current, int Next, float LostT, float NextT)   //하나 하나의 노드 거리 계산
    {
        if (Current >= Nodes.Count || Next >= Nodes.Count)
            return Vector3.zero;


        Transform Node = Nodes[Current].transform;                  //A
        Transform CurveNode = Nodes[Current].CurveNode;             //B
        Transform NextNode = Nodes[Next].transform;                 //C


        //_curveNodeDir = CurveNode.position - Node.position;                         //AB
        //_nextNodeDir = NextNode.position - CurveNode.position;                      //BC
        
        _curveNodeCenter = (LostT * Node.position) + (NextT * CurveNode.position);       //E
        _nextNodeCenter = (LostT * CurveNode.position) + (NextT * NextNode.position);    //F

        _arriveNodePos = (LostT * _curveNodeCenter) + (NextT * _nextNodeCenter);          //도착
            
        return _arriveNodePos;


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 to = Nodes[0].transform.position;//다음좌표
        Vector3 from = Nodes[0].transform.position;//이전좌표

        //초기 노드부터 최종 노드까지 돌아가
        for (int index = 0; index + 1 < Nodes.Count; index++)
        {
            for (int segment = 0; segment < Nodes[index].NodeSegment; segment++)
            {
                float t = (1.0f / Nodes[index].NodeSegment) * segment;
                float tt = (1.0f - t);
                
                from = to;  //전 좌표
                to = CalcNodePos(index, index + 1, tt, t);  //현재 좌표

                Gizmos.DrawLine(from, to);  //라인

                t += NodeInterval; //시간 0~1
            }
        }
    }

}
