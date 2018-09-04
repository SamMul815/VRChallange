using DragonController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    만 든 날 : 2018-04-02
    작 성 자 : 전민수

    작성내역 : 베지어곡선을 이용한 노드를 사용하여 오브젝트 이동   

    수 정 날 : 2018 - 05 - 05
    작 성 자 : 전민수
    수정내역 : 소스코드 정리
    
    수 정 날 : 2018 - 05 - 13
    작 성 자 : 전민수
    수정내역 : 기존 List를 Dictionary로 자료구조 바꿈으로써, enum 구조 추가

    수 정 날 : 2018 - 05 - 13
    작 성 자 : 전민수
    수정내역 : 노드가 Object에 붙었을 시 y값 회전 보정

    수정한날 : 2018 - 04 - 30
    작성자 : 김영민
    수정내역: 업벡터 확인후 드래곤 업벡터 , 월드 업벡터  사용
 */
public enum MOVEMENTTYPE
{
    OverLap = 0,
}

public class ObjectMovement : MonoBehaviour {

    Dictionary<MOVEMENTTYPE, NodeManager> _nodesManager = new Dictionary<MOVEMENTTYPE, NodeManager>();
    

    private void Awake()
    {
        MOVEMENTTYPE[] MoveTypesValues = (MOVEMENTTYPE[])System.Enum.GetValues(typeof(MOVEMENTTYPE));

        foreach (MOVEMENTTYPE t in MoveTypesValues)
        {

            GameObject gameObj = GameObject.Find(t.ToString());

            if (gameObj == null)
            {
                //Debug.LogWarning("ErrorLog - Is not NodeManager GameObject ");
                continue;
            }

            NodeManager Movement = gameObj.GetComponent<NodeManager>();
            
            if (Movement == null)
            {
                //Debug.LogWarning("ErrorLog - Is not NodeManager ");
                continue;
            }
            _nodesManager.Add(t, Movement);
        }
    }
    // 노드 찾기 
    /*
    public bool FindNode(int TYPE)
    {
        if (!_nodesManager[TYPE].IsRotation) { 
            if (_nodesManager[TYPE].IsStick)
            { 
                Vector3 forward = transform.position - _nodesManager[TYPE].transform.position;
                _nodesManager[TYPE].transform.rotation = Quaternion.LookRotation(forward);
                _nodesManager[TYPE].transform.position = transform.position;
                return true;
            }
            else
            {
                Vector3 forward = (_nodesManager[TYPE].transform.position - transform.position).normalized;

                float curSpeed = DragonManager.Stat.CurFlySpeed;
                float speed = _nodesManager[TYPE].Nodes[0].NodeSpeed;

                DragonManager.Stat.CurFlySpeed = BlackBoard.Instance.Acceleration(curSpeed, speed, 38.0f);

                transform.position =
                    Vector3.MoveTowards(
                        transform.position,
                        _nodesManager[TYPE].transform.position,
                        DragonManager.Stat.CurFlySpeed * Time.deltaTime);

                transform.rotation =
                    Quaternion.Lerp(
                        transform.rotation,
                        Quaternion.LookRotation(forward),
                        360.0f * Time.deltaTime);

                return BlackBoard.Instance.DistanceCalc(transform, _nodesManager[TYPE].Nodes[0].transform, 0.0f);
            }
        }
        return true;
    } 
    */

    /* Getter */
    public NodeManager GetNodeManager(MOVEMENTTYPE TYPE)
    {
        if (_nodesManager[TYPE] == null)
        {
            Debug.Log("ErrorLog - Is not NodeManager");
            return null;
        }
        return _nodesManager[TYPE];
    }

    /* 중심축으로 회전 */
    public void AxisRotation(MOVEMENTTYPE TYPE)
    {
        if (_nodesManager[TYPE].IsRotation)
        {
            Vector3 forward = (_nodesManager[TYPE].transform.position - transform.position).normalized;
            _nodesManager[TYPE].transform.rotation = Quaternion.LookRotation(forward);
        }
    }

    /* 이동 반복 */
    public void MovementLoop(MOVEMENTTYPE TYPE)
    {
        if (_nodesManager[TYPE].IsMoveLoop)
        {
            if (_nodesManager[TYPE].IsMoveEnd)
            {
                _nodesManager[TYPE].IsMoveEnd = false;
                _nodesManager[TYPE].CurNodesIndex = 0;
            }
        }
    }

    //노드 포지션 및 로테이션 셋팅
    public void MovementReady(MOVEMENTTYPE TYPE)
    {
        AxisRotation(TYPE);

        //bool isFindNode = FindNode(TYPE);
        //if (!isFindNode)
        //    return;


        _nodesManager[TYPE].AllNodesCalc();

        int NodesCount = _nodesManager[TYPE].NodesSpeed.Count;

        for (int nodeIndex = 0; nodeIndex < NodesCount; nodeIndex++)
        {
            _nodesManager[TYPE].Stat.NodeDir.Add(_nodesManager[TYPE].NodesDir[nodeIndex]);
            _nodesManager[TYPE].Stat.NodeSpeed.Add(_nodesManager[TYPE].NodesSpeed[nodeIndex]);
            _nodesManager[TYPE].Stat.NodeRot.Add(_nodesManager[TYPE].NodesRot[nodeIndex]);
        }
        _nodesManager[TYPE].IsMoveReady = true;

    }

    //노드를 따라서 이동 및 회전
    public void Movement(MOVEMENTTYPE TYPE)
    {
        int NodesCount = _nodesManager[TYPE].NodesSpeed.Count;
        int NodesIndex = _nodesManager[TYPE].CurNodesIndex;


        if (NodesIndex < NodesCount)
        {
            _nodesManager[TYPE].IsMoveEnd = false;

            float moveDistance = _nodesManager[TYPE].Stat.NodeSpeed[NodesIndex] * Time.deltaTime;
            float nextDistance = Vector3.Distance(_nodesManager[TYPE].Stat.NodeDir[NodesIndex], transform.position);
            
            Vector3 dir = (_nodesManager[TYPE].Stat.NodeDir[NodesIndex] - transform.position).normalized;
            
            Vector3 eulerAngle = _nodesManager[TYPE].NodesRot[NodesIndex] + new Vector3(0.0f, transform.rotation.eulerAngles.y, 0.0f);
            bool dragonUp = _nodesManager[TYPE].NodesDragonUp[NodesIndex];

            //if (NodesIndex + 1 < NodesCount)
            //{
            //    eulerAngle = (_nodesManager[TYPE].NodesRot[NodesIndex] - _nodesManager[TYPE].Stat.NodeRot[NodesIndex + 1]);
            //}
            

            for (; moveDistance > nextDistance;)
            {
                transform.position += dir * nextDistance;
                moveDistance -= nextDistance;

                _nodesManager[TYPE].CurNodesIndex++;
                NodesIndex = _nodesManager[TYPE].CurNodesIndex;

                if (NodesIndex >= NodesCount)
                    return;
                dir = (_nodesManager[TYPE].Stat.NodeDir[NodesIndex] - transform.position).normalized;

                eulerAngle = _nodesManager[TYPE].NodesRot[NodesIndex] + new Vector3(0.0f, transform.rotation.eulerAngles.y, 0.0f);
                //(_nodesManager[TYPE].Stat.NodeRot[NodesIndex - 1] - _nodesManager[TYPE].Stat.NodeRot[NodesIndex]);


                nextDistance = Vector3.Distance(_nodesManager[TYPE].Stat.NodeDir[NodesIndex],
                    transform.position);

            }

            if (_nodesManager[TYPE].CenterAxisRot != null)
            {
                Vector3 CentralAxis = (_nodesManager[TYPE].CenterAxisRot.position - transform.position).normalized;

                transform.rotation =
                    Quaternion. Slerp(
                        transform.rotation,
                        Quaternion.LookRotation(dir, CentralAxis),
                        45.0f * Time.fixedDeltaTime);
            }
            else
            {
                Quaternion rot;

                if (dragonUp)
                {
                    rot = Quaternion.Slerp(transform.rotation,
                        Quaternion.LookRotation(dir, transform.up) * Quaternion.Euler(eulerAngle), 0.1f);
                }
                else
                {
                    rot = Quaternion.Slerp(transform.rotation,
                        Quaternion.LookRotation(dir, Vector3.up) * Quaternion.Euler(eulerAngle), 0.1f);
                }
      

                //Quaternion rot = (Quaternion.LookRotation(dir, Vector3.up));
                //Quaternion rot = Quaternion.LookRotation(dir, Dragon.transform.up) 
                //    * Quaternion.Euler(eulerAngle);

                transform.rotation = rot;
            }

            transform.position += dir * moveDistance;

            if (NodesIndex + 1 >= NodesCount)
                return;
            dir = (_nodesManager[TYPE].Stat.NodeDir[NodesIndex] - transform.position).normalized;

        }
        else
        {
            _nodesManager[TYPE].IsMoveEnd = true;
            //MovementLoop(TYPE);
        }
    }



}
