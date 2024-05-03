using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : CoreComponent
{
    public event Action OnHealthZero;
    public event Action OnStunned;
    
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxStunResistance;
    
    private float currentHealth;
    private float currentStunResistance;

    protected override void Awake()
    {
        base.Awake();
        currentHealth = maxHealth;
        currentStunResistance = maxStunResistance;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
        DecreaseStunResistance(amount);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnHealthZero?.Invoke();
        }
    }

    public void DecreaseStunResistance(float amount)
    {
        Debug.Log("hit stun " + amount);
        currentStunResistance -= amount;
        Debug.Log("hit stun " + currentStunResistance);
        if (currentStunResistance <= 0)
        {
            Debug.Log("stunned");
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
}
