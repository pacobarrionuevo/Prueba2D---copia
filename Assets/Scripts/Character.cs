using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField, Min(1f)] protected float maxHealth = 100f;

    public float MaxHealth => maxHealth;

    public float CurrentHealth { get; private set; }
    public bool IsDead { get; private set; }
    

    protected virtual void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        if (IsDead || amount <= 0f) return;

        CurrentHealth = Mathf.Max(0f, CurrentHealth - amount);

        Debug.Log(CurrentHealth);

        if (CurrentHealth <= 0f)
        {
            IsDead = true;

            Die();
        }
    }

    protected abstract void Die();
}
