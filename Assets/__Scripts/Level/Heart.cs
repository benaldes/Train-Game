using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour,Idamageble
{
    [SerializeField] private int Health;
    [SerializeField] private int MaxHealth;
    
    
    [ContextMenu("destroy Heart")]
    public void HeartDestroyed()
    {
        LevelManager.HeartDestroyed.Invoke();
        Destroy(gameObject);
    }


    public void Damage(float damage)
    {
        Health -= (int)damage;
        if (Health < 0)
        {
            Health = 0;
            HeartDestroyed();
        }
    }
}
