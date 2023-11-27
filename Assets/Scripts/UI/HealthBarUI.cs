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
        Gnome.Instance.Hp.OnDamaged -= UpdateBarUI;
        Gnome.Instance.Hp.OnHealed -= UpdateBarUI;
    }


    private void Start()
    {
        Gnome.Instance.Hp.OnDamaged += UpdateBarUI;
        Gnome.Instance.Hp.OnHealed += UpdateBarUI;
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
        float normalizedHealth = currentHeatlh / Gnome.Instance.Hp.MaxHealth;
        barImage.fillAmount = normalizedHealth;
    }
}
