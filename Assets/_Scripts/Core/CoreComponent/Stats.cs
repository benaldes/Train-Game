using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : CoreComponent
{
    public event Action OnHealthZero;
    public event Action OnStunned;

    [SerializeField] private Image healthBarFill;
    
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxStunResistance;
    
    private float currentHealth;
    private float currentStunResistance;

    private bool invulnerable;

    protected override void Awake()
    {
        base.Awake();
        
        currentHealth = maxHealth;
        currentStunResistance = maxStunResistance;
        invulnerable = false;
    }

    public void DecreaseHealth(float amount)
    {
        if(invulnerable) return;
        currentHealth -= amount;
        UpdateHPBar();
        DecreaseStunResistance(amount);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnHealthZero?.Invoke();
        }
    }

    public void DecreaseStunResistance(float amount)
    {
        if(invulnerable) return;
        currentStunResistance -= amount;
        if (currentStunResistance <= 0)
        {
            currentStunResistance = 0;
            OnStunned?.Invoke();
        }
    }

    public void ResetStunResistance()
    {
        currentStunResistance = maxStunResistance;
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }

    public void ToggleInvulnerableMode()
    {
        invulnerable = !invulnerable;
    }

    private void UpdateHPBar()
    {
        if(healthBarFill == null) return;
        float amount = (currentHealth / maxHealth) * 100f;
        healthBarFill.rectTransform.sizeDelta = new Vector2(amount, healthBarFill.rectTransform.sizeDelta.y);
    }
}
