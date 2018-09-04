using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerVRState
{
    TeleportNone,
    TeleportSelect,
    Teleporting 
}

public class PlayerVRController : MonoBehaviour
{
    [SerializeField]
    private SteamVR_TrackedObject trackedObjectLeft;
    [SerializeField]
    private SteamVR_TrackedObject trackedObjectRight;
    [SerializeField]
    TeleportPointer pointer;
    [SerializeField]
    private Transform originTransform;
    [SerializeField]
    private Transform headTransform;

    [SerializeField]
    private Gun leftHandGun;
    [SerializeField]
    private Gun rightHandGun;

    PlayerVRState playerState;

    private Vector3 lastClickAngle = Vector3.zero;
    private bool isClicking = false;
    public float HapticClickAngleStep = 10;
    [SerializeField]
    public float teleportMovingTime = 0.4f;

    private bool isSlow = false;
    private float slowTime = 0.0f;

    [SerializeField]
    private float maxSlowTime = 5.0f;
    [SerializeField]
    [Range(0, 1)]
    private float slowValue = 0.2f;

    [SerializeField]
    private Slider slowSliderUI;
    [SerializeField]
    private Image slowGearUI;

    [SerializeField]
    private float freeMovingSpeed = 20.0f;

    private int prevArrow;
    [SerializeField]
    private float dashCooltime;
    [SerializeField]
    private float dashCheckTime;

    [SerializeField]
    private float dashDistance;

    private float calcDashCoolTime;
    private float calcDashCheckTime;


    SteamVR_Controller.Device LeftHand
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObjectLeft.index);
        }
    }
    SteamVR_Controller.Device RightHand
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObjectRight.index);
        }
    }

    private void Awake()
    {
        prevArrow = -1;
        calcDashCoolTime = 0.0f;
        calcDashCheckTime = dashCheckTime;
    }

    private void Start()
    {
        playerState = PlayerVRState.TeleportNone;
    }

    private void Update()
    {
        Teleport();
        GunFire();
        Slow();
    }

    private void FixedUpdate()
    {
      // Move();
    }

    void Teleport()
    {
        if (playerState == PlayerVRState.TeleportNone && trackedObjectLeft.isActiveAndEnabled)
        {
            if (LeftHand.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
            {
                pointer.transform.parent = trackedObjectLeft.transform;
                pointer.transform.localPosition = Vector3.zero;
                pointer.transform.localRotation = Quaternion.identity;
                pointer.transform.localScale = Vector3.one;
                pointer.enabled = true;

                playerState = PlayerVRState.TeleportSelect;

                pointer.ForceupdateCurrentAngle();
                //lastClickAngle = pointer.CurrentPointVector;
                //isClicking = pointer.CanTeleport;

            }
        }
        else if (playerState == PlayerVRState.TeleportSelect)
        {
            if (LeftHand.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
            {
                if (pointer.CanTeleport)
                {
                    playerState = PlayerVRState.Teleporting;
                    FMODSoundManager.Instance.PlayTeleportsound(this.transform.position);
                    StartCoroutine("CorTeleport", pointer.SelectedPoint);
                }
                else
                {
                    playerState = PlayerVRState.TeleportNone;
                }
                pointer.enabled = false;
                pointer.transform.parent = null;
                pointer.transform.position = Vector3.zero;
                pointer.transform.rotation = Quaternion.identity;
                pointer.transform.localScale = Vector3.one;
            }
            //else
            //{
            //    Vector3 offset = headTransform.position - originTransform.position;
            //    offset.y = 0;

            //    if (pointer.CurrentParabolaAngleY >= 45)
            //        lastClickAngle = pointer.CurrentPointVector;

            //    float angleClickDiff = Vector3.Angle(lastClickAngle, pointer.CurrentPointVector);
            //    if(isClicking && Mathf.Abs(angleClickDiff) > HapticClickAngleStep)
            //    {
            //        lastClickAngle = pointer.CurrentPointVector;
            //        if (pointer.CanTeleport)
            //            RightHand.TriggerHapticPulse();
            //    }

            //    if (pointer.CanTeleport && !isClicking)
            //    {
            //        isClicking = true;
            //        RightHand.TriggerHapticPulse(750);
            //        lastClickAngle = pointer.CurrentPointVector;
            //    }
            //    else if (!pointer.CanTeleport && isClicking)
            //        isClicking = false;
            //}
        }
        else //playerState == PlayerVRState.Teleporting
        {

        }
    }

    void GunFire()
    {
        float rightHandAxis = 0.0f;
        float leftHandAxis = 0.0f;

        if (trackedObjectRight.isActiveAndEnabled)
            rightHandAxis = RightHand.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;
        if(trackedObjectLeft.isActiveAndEnabled)
            leftHandAxis = LeftHand.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;

        if (rightHandAxis >= 1.0f)
        {
            rightHandGun.Fire();
        }
        if(leftHandAxis >= 1.0f)
        {
            leftHandGun.Fire();
        }
    }

    void Slow()
    {
        if (RightHand.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if(slowTime <= 0)
                isSlow = true;
        }

        if(isSlow)
        {
            if (slowTime >= maxSlowTime)
                isSlow = false;
            else
            {
                Time.timeScale = slowValue; 
                slowTime += Time.unscaledDeltaTime;
                if (slowTime > maxSlowTime) slowTime = maxSlowTime;
            }
        }
        else
        {
            Time.timeScale = 1.0f;
            if (slowTime > 0)
                slowTime -= Time.unscaledDeltaTime / 6;
            else
                slowTime = 0;
        }
        float value = 1 - slowTime / maxSlowTime;

        slowSliderUI.value = value;
        slowGearUI.transform.localRotation = Quaternion.Euler(0, 0, value * -1440);

    }

    private void Move()
    {
        if(LeftHand.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //Debug.Log(prevArrow.ToString() + ArrowValue(8,RightHand.GetAxis()).ToString());
            int currentArrow = ArrowValue(8, LeftHand.GetAxis());
            if (prevArrow == currentArrow &&
                calcDashCheckTime < dashCheckTime &&
                calcDashCoolTime <= 0)
            {
                Dash(8,prevArrow);
                calcDashCoolTime = dashCooltime;
                Debug.Log("Dash!");
            }
        }
        else if (LeftHand.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 _axis = LeftHand.GetAxis();
            int _curretArrow = ArrowValue(8, _axis);
            prevArrow = _curretArrow;

            Vector3 forwardDir = headTransform.forward;
            Vector3 rightDir = headTransform.right;
            forwardDir.y = 0;
            rightDir.y = 0;
            Vector3 moveDir = forwardDir * _axis.y + rightDir * _axis.x;
            moveDir.Normalize();
            originTransform.transform.position += moveDir * freeMovingSpeed * Time.fixedUnscaledDeltaTime;            
        }
        else if(LeftHand.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            calcDashCheckTime = 0.0f;
        }
        else
        {
            if (calcDashCheckTime < dashCheckTime)
                calcDashCheckTime += Time.fixedUnscaledDeltaTime;

            if (calcDashCoolTime >= 0)
                calcDashCoolTime -= Time.fixedUnscaledDeltaTime;
        }
    }

    int ArrowValue(int divCount, Vector2 axis)
    {
        Vector2 baseAxis = new Vector2(0, 1.0f);
        float angle = Vector2.SignedAngle(axis,baseAxis);
        if (angle < 0)
            angle = 360.0f + angle;

 
        float d = 360.0f / divCount;
        angle += (d / 2);

        int retType = (int)(angle / d);

        if (retType >= divCount)
            retType = 0;

        return retType;
    }

    private void Dash(int divCount, int value)
    {
        Vector3 dir = headTransform.forward;
        dir.y = 0;

        dir = Quaternion.AngleAxis(360.0f / divCount * value,Vector3.up) * dir.normalized;
        
        Vector3 dashPosition = originTransform.position + dir * dashDistance;
        FMODSoundManager.Instance.PlayTeleportsound(this.transform.position);

        StartCoroutine(CorTeleport(dashPosition));
    }


    IEnumerator CorTeleport(Vector3 teleportPosition)
    {
        Vector3 difference = originTransform.position - headTransform.position;
        Vector3 startPos = originTransform.position;
        difference.y = 0;
        teleportPosition += difference;

        for(float t = 0; t< teleportMovingTime;t += Time.unscaledDeltaTime)
        {
            originTransform.position = Vector3.Lerp(startPos, teleportPosition, t / teleportMovingTime);
            yield return new WaitForEndOfFrame();
        }
        originTransform.position = teleportPosition;
        playerState = PlayerVRState.TeleportNone;
        yield return null;
    }

}
