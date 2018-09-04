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

    private float currentHP;
    bool isDie = false;

    private void Awake()
    {
        currentHP = maxHP;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            HitDamage(Random.Range(5, 15));
        }
    }

    public void HitDamage(float damage)
    {
        currentHP -= damage;

        hpSliderUI.value = currentHP / maxHP;
        hpTextUI.text = ((int)currentHP).ToString();
        if (currentHP <= 0)
        {
            hpTextUI.text = "0";
            isDie = true;
        }
    }

}
