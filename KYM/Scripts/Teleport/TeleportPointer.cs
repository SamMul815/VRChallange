using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportPointer : MonoBehaviour
{

    public Vector3 initialVelocity = Vector3.forward * 10.0f;
    public Vector3 acceleration = Vector3.up * -9.8f;
    public int pointcount = 10;
    public float pointSpacing = 0.5f;
    public float graphicThickness = 0.2f;
    public Material graphicMaterial;
    public Material graphicNoneMaterial;

    public LayerMask teleportLayer = ~0;
    public LayerMask parabolHitLayer = ~0;

    [SerializeField]
    private GameObject selectionPadPrefab;
    [SerializeField]
    private GameObject invalidPadPrefab;




    public Vector3 SelectedPoint { get; private set; }
    public float CurrentParabolaAngleY { get; private set; }
    public bool CanTeleport { get; private set; }
    public Vector3 CurrentPointVector { get; private set; }

    private GameObject selectionPadObject;
    private GameObject invalidPadObject;

    private Mesh parabolaMesh;
    private List<Vector3> parabolaPoints;

	// Use this for initialization
	void Start ()
    {
        parabolaPoints = new List<Vector3>(pointcount);
        parabolaMesh = new Mesh();
        parabolaMesh.MarkDynamic(); //메쉬가 지속적으로 업데이될때 필요한 최적화 작업
        parabolaMesh.vertices = new Vector3[0];
        parabolaMesh.triangles = new int[0];

        if(selectionPadPrefab != null)
        {
            selectionPadObject = Instantiate<GameObject>(selectionPadPrefab);
            SceneManager.MoveGameObjectToScene(selectionPadObject, SceneManager.GetSceneByName("MainScene"));
            selectionPadObject.SetActive(false);
        }

        if(invalidPadPrefab != null)
        {
            invalidPadObject = Instantiate<GameObject>(invalidPadPrefab);
            SceneManager.MoveGameObjectToScene(invalidPadObject, SceneManager.GetSceneByName("MainScene"));
            invalidPadObject.SetActive(false);
        }
	}

    private void OnDisable()    //비활성화 될때 호출
    {
        if (selectionPadObject != null)
            selectionPadObject.SetActive(false);
        if (invalidPadObject != null)
            invalidPadObject.SetActive(false);
    }

    void Update ()
    {
        Vector3 velocity = transform.TransformDirection(initialVelocity);
        Vector3 velocity_normalized;
        CurrentParabolaAngleY = ClampInitialVelocity(ref velocity, out velocity_normalized);
        CurrentPointVector = velocity_normalized;

        Vector3 normal;
        CanTeleport = CalculateParabolicCurve(
            transform.position,
            velocity,
            acceleration, pointSpacing, pointcount,
            teleportLayer,
            parabolaPoints,
            out normal);

        SelectedPoint = parabolaPoints[parabolaPoints.Count - 1];

        if (selectionPadObject != null)
        {
            selectionPadObject.SetActive(CanTeleport);
            selectionPadObject.transform.position = SelectedPoint + Vector3.one * 0.005f + Vector3.up * 0.5f;
            if (CanTeleport)
            {
                selectionPadObject.transform.rotation = Quaternion.LookRotation(normal);
                selectionPadObject.transform.Rotate(90, 0, 0);
            }
        }
        if (invalidPadObject != null)
        {
            invalidPadObject.SetActive(!CanTeleport);
            invalidPadObject.transform.position = SelectedPoint + Vector3.one * 0.005f + Vector3.up * 0.5f;
            if (!CanTeleport)
            {
                invalidPadObject.transform.rotation = Quaternion.LookRotation(normal);
                invalidPadObject.transform.Rotate(90, 0, 0);
            }
        }

        // Draw parabola (BEFORE the outside faces of the selection pad, to avoid depth issues)
        GenerateMesh(ref parabolaMesh, parabolaPoints, velocity, Time.time % 1);
        if(CanTeleport)
            Graphics.DrawMesh(parabolaMesh, Matrix4x4.identity, graphicMaterial, gameObject.layer);
        else
            Graphics.DrawMesh(parabolaMesh, Matrix4x4.identity, graphicNoneMaterial, gameObject.layer);
    }

    private void GenerateMesh(ref Mesh m, List<Vector3> points, Vector3 fwd, float uvoffset)
    {
        Vector3[] verts = new Vector3[points.Count * 2];
        Vector2[] uv = new Vector2[points.Count * 2];

        Vector3 right = Vector3.Cross(fwd, Vector3.up).normalized;

        for (int x = 0; x < points.Count; x++)
        {
            verts[2 * x] = points[x] - right * graphicThickness / 2;
            verts[2 * x + 1] = points[x] + right * graphicThickness / 2;

            float uvoffset_mod = uvoffset;
            if (x == points.Count - 1 && x > 1)
            {
                float dist_last = (points[x - 2] - points[x - 1]).magnitude;
                float dist_cur = (points[x] - points[x - 1]).magnitude;
                uvoffset_mod += 1 - dist_cur / dist_last;
            }

            uv[2 * x] = new Vector2(0, x - uvoffset_mod);
            uv[2 * x + 1] = new Vector2(1, x - uvoffset_mod);
        }

        int[] indices = new int[2 * 3 * (verts.Length - 2)];
        for (int x = 0; x < verts.Length / 2 - 1; x++)
        {
            int p1 = 2 * x;
            int p2 = 2 * x + 1;
            int p3 = 2 * x + 2;
            int p4 = 2 * x + 3;

            indices[12 * x] = p1;
            indices[12 * x + 1] = p2;
            indices[12 * x + 2] = p3;
            indices[12 * x + 3] = p3;
            indices[12 * x + 4] = p2;
            indices[12 * x + 5] = p4;

            indices[12 * x + 6] = p3;
            indices[12 * x + 7] = p2;
            indices[12 * x + 8] = p1;
            indices[12 * x + 9] = p4;
            indices[12 * x + 10] = p2;
            indices[12 * x + 11] = p3;
        }

        m.Clear();
        m.vertices = verts;
        m.uv = uv;
        m.triangles = indices;
        m.RecalculateBounds();
        m.RecalculateNormals();
    }

    private static Vector3 ProjectVectorOntoPlane(Vector3 planeNormal, Vector3 point)
    {
        Vector3 d = Vector3.Project(point, planeNormal.normalized);
        return point - d;
    }

    public void ForceupdateCurrentAngle()
    {
        Vector3 velocity = transform.TransformDirection(initialVelocity);
        Vector3 d;
        CurrentParabolaAngleY = ClampInitialVelocity(ref velocity, out d);
        CurrentPointVector = d;
    }

    private float ClampInitialVelocity(ref Vector3 velocity, out Vector3 velocity_normalized)
    {
        Vector3 velocity_fwd = ProjectVectorOntoPlane(Vector3.up, velocity);

        float angle = Vector3.Angle(velocity_fwd, velocity);

        Vector3 right = Vector3.Cross(Vector3.up, velocity_fwd);

        if(Vector3.Dot(right, Vector3.Cross(velocity_fwd, velocity)) > 0)
        {
            angle *= -1;
        }
        if(angle > 45)
        {
            velocity = Vector3.Slerp(velocity_fwd, velocity, 45 / angle);
            velocity /= velocity.magnitude;
            velocity_normalized = velocity;
            velocity *= initialVelocity.magnitude;
            angle = 45;
        }
        else
        {
            velocity_normalized = velocity.normalized;
        }

        return angle;
    }

    private static float ParabolicCurve(float p0, float v0, float a, float t)
    {
        return p0 + v0 * t + 0.5f * a * t * t;
    }

    private static Vector3 ParabolicCurve(Vector3 p0, Vector3 v0, Vector3 a, float t)
    {
        Vector3 ret = new Vector3();
        for (int x = 0; x < 3; x++)
            ret[x] = ParabolicCurve(p0[x], v0[x], a[x], t);
        return ret;
    }

    private static float ParabolicCurveDeriv(float v0, float a, float t)
    {
        return v0 + a * t;
    }

    private static Vector3 ParabolicCurveDeriv(Vector3 v0, Vector3 a, float t)
    {
        Vector3 ret = new Vector3();
        for (int x = 0; x < 3; x++)
            ret[x] = ParabolicCurveDeriv(v0[x], a[x], t);
        return ret;
    }

    private bool CalculateParabolicCurve(Vector3 p0, Vector3 v0, Vector3 a, float dist, int points,int teleportLayer, List<Vector3> outPts, out Vector3 normal)
    {
        outPts.Clear();
        outPts.Add(p0);

        Vector3 last = p0;
        float t = 0;

        for(int i = 0; i<points; i++)
        {
            t += dist / ParabolicCurveDeriv(v0, a, t).magnitude;
            Vector3 next = ParabolicCurve(p0, v0, a, t);

            Vector3 dir = next - last;
            float distance = dir.magnitude;
            
            RaycastHit hitInfo;
            Ray ray = new Ray(last, dir.normalized);
            if(Physics.Raycast(ray, out hitInfo, distance, parabolHitLayer))
            {
                outPts.Add(hitInfo.point);
                normal = hitInfo.normal;
                int layer = hitInfo.collider.gameObject.layer;
                //Debug.Log(layer);
                if(teleportLayer == (teleportLayer |(1 << layer)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                outPts.Add(next);
            }

            last = next;
        }
        normal = Vector3.up;
        return false;
    }
}
