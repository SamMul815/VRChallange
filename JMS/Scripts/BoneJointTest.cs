using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneJointTest : MonoBehaviour
{
    Transform parentTransform;
    Rigidbody boneRigidBody;

    Vector3 prevFrameParentPosition = Vector3.zero;

    float weight = 10.0f;

    public float clampDist = 00.3f;

    private void Awake()
    {
        parentTransform = transform.parent;
        prevFrameParentPosition = parentTransform.position;

        boneRigidBody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start () {
		
	}

    private void FixedUpdate()
    {
        Vector3 delta = (prevFrameParentPosition - parentTransform.position);

        boneRigidBody.AddForce(Vector3.ClampMagnitude(delta, clampDist) * weight);
        prevFrameParentPosition = parentTransform.position;
    }
}
