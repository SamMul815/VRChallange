using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성일 : 2018 - 05 -02
/// 작성자 : 김영민
/// 작업내용 : 사운드 매니저 제작
/// </summary>


[System.Serializable]
public class AudioData
{
    [SerializeField]
    public string name;
    [SerializeField]
    public AudioClip sound;
}

public enum AudioPlayType
{
    NONE,
    SOUND3D,
    LOOP
}

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private List<AudioData> audioList;

    private Dictionary<string, AudioClip> audioDictionary = new Dictionary<string, AudioClip>();
    private List<GameObject> emptyAudioSourceObjects = new List<GameObject>();

    [SerializeField]
    private GameObject audioSoundObject;

    private void Start()
    {
        for(int i = 0; i<audioList.Count; i++)
        {
            audioDictionary.Add(audioList[i].name, audioList[i].sound);
        }
        audioSoundObject = new GameObject();
        audioSoundObject.AddComponent<AudioSound>();
        audioSoundObject.gameObject.name = "AudioSoundObjectBase";
        audioSoundObject.SetActive(false);
    }

    /// <summary>
    /// 오디오를 재생하고 싶으면 사용하세요
    /// </summary>
    /// <param name="_name">재생오디오 이름</param>
    /// <param name="_pos">재생 위치</param>
    /// <param name="_playType">재생 타입 기본 NONE</param>
    public void PlayAudio(string _name,Vector3 _pos, AudioPlayType _playType = AudioPlayType.NONE)
    {
        GameObject _audioSoundObject;
        if (emptyAudioSourceObjects.Count > 0)
        {
            _audioSoundObject = emptyAudioSourceObjects[emptyAudioSourceObjects.Count-1];
            emptyAudioSourceObjects.Remove(_audioSoundObject);
        }
        else
        {
            _audioSoundObject = Instantiate(audioSoundObject, _pos, Quaternion.identity);
        }
        _audioSoundObject.SetActive(true);
        _audioSoundObject.transform.position = _pos;
        _audioSoundObject.GetComponent<AudioSound>().SetClip(audioDictionary[_name]);
        _audioSoundObject.GetComponent<AudioSound>().Play();

        if(_playType == AudioPlayType.LOOP)
            _audioSoundObject.GetComponent<AudioSound>().LoopOn();

        if (_playType == AudioPlayType.SOUND3D)
            _audioSoundObject.GetComponent<AudioSound>().Sound3DOn();


    }

    /// <summary>
    /// 전부 사용한 사운드 오브젝트는 여기를 통해 돌아옵니다.
    /// 내부에서 리셋해줍니다.
    /// </summary>
    /// <param name="_empyAudioSourceObject">다 사용한 사운드 오브젝트</param>
    public void AddEmptyAudioSourceObject(GameObject _empyAudioSourceObject)
    {
        _empyAudioSourceObject.GetComponent<AudioSound>().Reset();
        _empyAudioSourceObject.SetActive(false);
        emptyAudioSourceObjects.Add(_empyAudioSourceObject);
    }
}
