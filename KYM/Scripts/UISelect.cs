using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelect : MonoBehaviour {

    [SerializeField]
    private SteamVR_TrackedObject trackedObject;
    SteamVR_Controller.Device Hand
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObject.index);
        }
    }


    public bool IsSelect = false;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Hand.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            IsSelect = true;
        }
        if (Hand.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            IsSelect = false;
        }
    }

    public void OKSelect()
    {
        IsSelect = false;
    }




}
