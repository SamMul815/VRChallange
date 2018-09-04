using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    [SerializeField]
    private float maxHP;

    [SerializeField]
    private Slider hpSliderUI;

    [SerializeField]
    private Text hpTextUI;

    [SerializeField]
    private GameObject hitEffect;

    private float currentHP;
    private bool isHit;
    bool isDie = false;

    private void Awake()
    {
        currentHP = maxHP;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            UtilityManager.Instance.AttackPlayer(50);
            //HitDamage(Random.Range(5, 15));
        }
    }

    public void HitDamage(float damage)
    {
        if (isHit) return;

        StartCoroutine(corHitEffect());
        currentHP -= damage;
        isHit = true;
        hpSliderUI.value = currentHP / maxHP;
        hpTextUI.text = ((int)currentHP).ToString();
        if (currentHP <= 0)
        {
            hpTextUI.text = "0";
            isDie = true;
        }
    }

    public bool IsDead() { return isDie; }

    IEnumerator corHitEffect()
    {
        if (!hitEffect.activeSelf)
        {
            hitEffect.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            isHit = false;
            yield return null;
        }
    }

}
