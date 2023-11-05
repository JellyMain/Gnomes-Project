using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] float damageBarShrinkTimerMax = 1.5f;
    [SerializeField] float damageBarShrinkSpeed = 1;
    [SerializeField] Image damageBarImage;
    [SerializeField] Image barImage;
    private float damageBarTimer;



    private void OnDisable()
    {
        Player.Instance.Hp.OnDamaged -= UpdateBarUI;
        Player.Instance.Hp.OnHealed -= UpdateBarUI;
    }


    private void Start()
    {
        Player.Instance.Hp.OnDamaged += UpdateBarUI;
        Player.Instance.Hp.OnHealed += UpdateBarUI;
    }


    private void Update()
    {
        damageBarTimer -= Time.deltaTime;
        if (damageBarTimer < 0)
        {
            if (barImage.fillAmount < damageBarImage.fillAmount)
            {
                damageBarImage.fillAmount -= damageBarShrinkSpeed * Time.deltaTime;
            }
        }
    }


    private void UpdateBarUI(float currentHeatlh)
    {
        damageBarTimer = damageBarShrinkTimerMax;
        float normalizedHealth = currentHeatlh / Player.Instance.Hp.MaxHealth;
        barImage.fillAmount = normalizedHealth;
    }
}
