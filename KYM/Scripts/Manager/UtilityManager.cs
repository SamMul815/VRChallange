using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// 작성일 : 2018 - 05 - 09
/// 직상지 : 김영민
/// 작업내용 : 유틸리티 매니저 제작
/// 다른곳에서 자주 불리는 애들 또는 자주 하는 계산 여기서 가져다 놓고 사용
/// </summary>

public class UtilityManager :  Singleton<UtilityManager>
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Dragon;

    [SerializeField] private TestShakePlayer Shake;

    [SerializeField] private PlayerStat playerStat;

    [SerializeField] private Image blackScreenImg;
    [SerializeField] private Image gameOverImg;
    [SerializeField] private Image victoryImg;

    public Vector3 PlayerPosition() { return Player.transform.position; }
    public Vector3 DragonPosition() { return Dragon.transform.position; }

    public Transform PlayerTransform() { return Player.transform; }
    public Transform DragonTransform() { return Dragon.transform; }

    public void AttackPlayer(float damage)
    {
        playerStat.HitDamage(damage);
        Debug.Log("player Attack damage: " + damage.ToString());
    }

    public void ShakePlayer()
    {
        Shake.PlayerShake();
    }
    public void ShakePlayer(float _playTime, float _radius, float _waitTime)
    {
        Shake.PlayerShake(_playTime, _radius, _waitTime);
    }
    public void ShakePlayerDistance()
    {
        //70
        Vector3 dir = PlayerPosition() - DragonPosition();
        float distane = Mathf.Abs(dir.magnitude);
        float _radius = 0.5f;
        if (distane > 70)
            return;
        else
        {
            _radius = _radius - (distane / 70.0f * 0.3f);
            Shake.PlayerShake(0.3f, _radius + 0.1f, 0.02f);
        }
    }
    public void ShakePlayerHowling()
    {
        Vector3 dir = PlayerPosition() - DragonPosition();
        float distane = Mathf.Abs(dir.magnitude);
        float _radius = 0.7f;
        if (distane > 100)
            return;
        else
        {
            _radius = _radius - (distane / 100.0f * 0.5f);
            Shake.PlayerShake(0.5f, _radius + 0.1f, 0.02f);
        }
    }
    public static bool DistanceCalc(Transform This, Transform Target, float Range)
    {
        if (Vector3.Distance(This.position, Target.position) <= Range)
            return true;

        return false;
    }

    public void GameOver()
    {
        StartCoroutine(GameEndEvent(blackScreenImg, gameOverImg));
    }

    public void GameClear()
    {
        StartCoroutine(GameEndEvent(blackScreenImg, victoryImg));
    }

    IEnumerator GameEndEvent(Image Image1, Image Image2)
    {
        Image1.CrossFadeAlpha(1.0f, 3.0f, false);

        yield return new WaitForSeconds(4.0f);

        SceneManager.LoadScene(0);
    }

}
