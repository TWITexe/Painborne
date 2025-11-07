using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private float destroyDelay = 1.5f;

    public event Action<int> OnDamaged; // вызывается при уроне, передаём текущее HP
    public event Action OnDeath;        // вызывается при смерти
    public event Action<int> OnHealed;  // вызывается при лечении, передаём текущее HP
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private Animator animator;

    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        

        animator?.SetTrigger("GetDamage");

        OnDamaged?.Invoke(currentHealth); // 🔔 событие урона

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(int amount)
    {
        if (currentHealth > 0)
        {
            currentHealth = Mathf.Min(currenstHealth + amount, maxHealth);
            Debug.Log($"{gameObject.name} Вылечился! У него стало: {currentHealth} здоровья");
            animator?.SetTrigger("Prays");
            OnHealed?.Invoke(currentHealth); // 🔔 событие лечения
        }
        
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} Мёртв!");

        //animator?.SetTrigger("Die");

        OnDeath?.Invoke(); // 🔔 событие смерти
        Destroy(gameObject, destroyDelay);
    }
}
