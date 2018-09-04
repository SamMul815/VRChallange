using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStat : MonoBehaviour {

    private List<float> _nodeSpeed = new List<float>();
    public List<float> NodeSpeed { set { _nodeSpeed = value; } get { return _nodeSpeed; } }

    private List<Vector3> _nodeDir = new List<Vector3>();
    public List<Vector3> NodeDir { set { _nodeDir = value; } get { return _nodeDir; } }

    private List<Vector3> _nodeRot = new List<Vector3>();
    public List<Vector3> NodeRot { set { _nodeRot = value; } get { return _nodeRot; } }

}
