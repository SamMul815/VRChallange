using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FMODSoundManager : Singleton<FMODSoundManager>
{
    [FMODUnity.EventRef]
    public string GunFireSound;
    [FMODUnity.EventRef]
    public string GunReloadSound;
    [FMODUnity.EventRef]
    public string TeleportSound;

    [FMODUnity.EventRef]
    public string BossHowling;

    [FMODUnity.EventRef]
    public string Background;

    [FMODUnity.EventRef]
    public string AmbientWind;

    [FMODUnity.EventRef]
    public string Move;

    FMOD.Studio.EventInstance bgmSound;
    FMOD.Studio.ParameterInstance bgmVolume;
    
    public void Start()
    {
        //PlayBGM();
    }

    public void PlayFireSound(Vector3 pos)
    {
        FMODUnity.RuntimeManager.PlayOneShot(GunFireSound,pos);
    }

    public void PlayReloadSound(Vector3 pos)
    {
        FMODUnity.RuntimeManager.PlayOneShot(GunReloadSound, pos);
    }
    public void PlayTeleportsound(Vector3 pos)
    {
        FMODUnity.RuntimeManager.PlayOneShot(TeleportSound, pos);
    }
    public void PlayBossHowling(Vector3 pos)
    {
        FMODUnity.RuntimeManager.PlayOneShot(BossHowling, pos);
    }
    public void PlayBGM(Vector3 pos)
    {
        FMODUnity.RuntimeManager.PlayOneShot(Background, pos);
    }

    public void PlayFireSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(GunFireSound);
    }

    public void PlayReloadSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(GunReloadSound);
    }
    public void PlayTeleportsound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(TeleportSound);
    }
    public void PlayBossHowling()
    {
        FMODUnity.RuntimeManager.PlayOneShot(BossHowling);
    }
    public void PlayBGM()
    {
        FMODUnity.RuntimeManager.PlayOneShot(Background);
    }
    public void PlayMove(Vector3 pos)
    {
        FMODUnity.RuntimeManager.PlayOneShot(Move);
    }

}
